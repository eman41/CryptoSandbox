// Credential.cs,  5/16/2015
// Author: Eric S Policaro

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Crypto.Core
{
    /// <summary>
    /// This class contains a usename/password credential set.
    /// When disposed, it clears the stored credential values.
    /// </summary>
    public class Credential : IDisposable
    {
        /// <summary>
        /// Retrieve an empty credential instance.
        /// </summary>
        public static Credential Empty
        {
            get { return new Credential(string.Empty, string.Empty); }
        }

        /// <summary>
        /// Initalizes a new instance of class <see cref="Credential"/> with the given parameters.
        /// </summary>
        /// <param name="username">Credential username</param>
        /// <param name="password">Password in bytes</param>
        public Credential(string username, string password)
        {
            Username = username;
            _password = Encrypt(GetUTF8Bytes(password));
        }

        /// <summary>
        /// Initalizes a new instance of class <see cref="Credential"/> with the given parameters.
        /// </summary>
        /// <param name="username">Credential username</param>
        /// <param name="password">Password as a SecureString</param>
        public Credential(string username, SecureString password)
        {
            Username = username;
            MarshalSecureString(password);
        }

        private Credential(string username, byte[] password)
        {
            Username = username;
            _password = password;
        }

        private void MarshalSecureString(SecureString password)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(password);
                _password = Encrypt(GetUTF8Bytes(Marshal.PtrToStringUni(ptr)));
            }
            finally
            {
                Marshal.ZeroFreeCoTaskMemUnicode(ptr);
            }
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the password as a string.
        /// </summary>
        public string GetPasswordString()
        {
            byte[] decrypted = ProtectedData.Unprotect(_password, Secret, ProtectionScope);
            return Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// Gets the encoded version of this credential.
        /// </summary>
        /// <returns>Encoded and encrypted credentials.</returns>
        public string Encoded()
        {
            byte[] result = GetUTF8Bytes(Username + _separator + ToBase64(_password));
            return ToBase64(result);
        }

        /// <summary>
        /// Decode an encoded credential string into a new Credential instance.
        /// </summary>
        /// <param name="encoded">Encoded credential data</param>
        /// <returns>Credential instance (empty credential if decoding fails)</returns>
        public static Credential Decode(string encoded)
        { 
            string decoded = Encoding.UTF8.GetString(FromBase64(encoded));
            string[] parts = decoded.Split(_separator);

            return (parts.Length == 2) ? new Credential(parts[0], FromBase64(parts[1]))
                                       : Credential.Empty;
        }

        private byte[] Encrypt(byte[] data)
        {
            return ProtectedData.Protect(data, Secret, ProtectionScope);
        }

        private string ToBase64(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        private static byte[] FromBase64(string line)
        {
            return Convert.FromBase64String(line);
        }

        private byte[] GetUTF8Bytes(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        private static byte[] Secret
        {
            get { return Encoding.UTF8.GetBytes(secretVal); }
        }

        private static DataProtectionScope ProtectionScope
        {
            get { return DataProtectionScope.CurrentUser; }
        }

        public void Dispose()
        {
            try { _password = new byte[0]; }
            catch (Exception) { }
        }

        private byte[] _password;
        private const char _separator = '|';

        // Some example encryption entropy. Entropy with respect to DPAPI is only
        // useful for protecting encrypted data from other applications. To make
        // entropy useful for symmetric encryption, it needs to be stored somewhere.
        // More info: http://security.stackexchange.com/questions/20358/what-is-the-purpose-of-the-entropy-parameter-for-dpapi-protect
        private static string secretVal = "5f0263987d2a450487da877d6b52e99e3d52e30ace544e20a597eeebff9f1b99";
    }
}
// Credential.cs,  5/16/2015
// Author: Eric S Policaro

using System;
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
        /// Retrieve an instance of an empty credential.
        /// </summary>
        public static Credential Empty
        {
            get { return new Credential(); }
        }

        /// <summary>
        /// Initalizes a new instance of class <see cref="Credential"/>.
        /// </summary>
        public Credential() : this(string.Empty, string.Empty) { }

        /// <summary>
        /// Initalizes a new instance of class <see cref="Credential"/> with the given parameters.
        /// </summary>
        /// <param name="username">Credential username</param>
        /// <param name="password">Credential password</param>
        public Credential(string username, byte[] password)
            : this(username, Encoding.UTF8.GetString(password)) { }

        /// <summary>
        /// Initalizes a new instance of class <see cref="Credential"/> with the given parameters.
        /// </summary>
        /// <param name="username">Credential username</param>
        /// <param name="password">Password in bytes</param>
        public Credential(string username, string password)
        {
            Username = username;
            Password = Encoding.UTF8.GetBytes(password);
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public byte[] Password { get; private set; }

        /// <summary>
        /// Gets the password as a string.
        /// </summary>
        public string GetPasswordString()
        {
            return Encoding.UTF8.GetString(Password);
        }

        /// <summary>
        /// Encode this credential for storage.
        /// </summary>
        /// <returns></returns>
        public string Encode()
        {
            byte[] encryptedPassword = Encrypt(Password);
            string encodedPassword = ToBase64(encryptedPassword);

            byte[] result = GetUTF8Bytes(Username + _separator + encodedPassword);
            encryptedPassword = new byte[0];

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

            if (parts.Length == 2)
                return new Credential(parts[0], Decrypt(parts[1]));

            return Credential.Empty;
        }

        private byte[] Encrypt(byte[] data)
        {
            return ProtectedData.Protect(data, Secret, _protectionScope);
        }

        private static byte[] Decrypt(string encrypted)
        {
            byte[] decodedData = FromBase64(encrypted);
            return ProtectedData.Unprotect(decodedData, Secret, _protectionScope);
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
            try { Password = new byte[0]; }
            catch (Exception) { }
        }

        private const char _separator = '|';
        private static DataProtectionScope _protectionScope = DataProtectionScope.CurrentUser;

        // Some example encryption entropy. Entropy with respect to DPAPI is only
        // useful for protecting encrypted data from other applications. To make
        // entropy useful for symmetric encryption, it needs to be stored somewhere.
        // More info: http://security.stackexchange.com/questions/20358/what-is-the-purpose-of-the-entropy-parameter-for-dpapi-protect
        private static string secretVal = "5f0263987d2a450487da877d6b52e99e3d52e30ace544e20a597eeebff9f1b99";
    }
}
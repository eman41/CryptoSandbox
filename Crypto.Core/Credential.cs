// Credential.cs,  5/16/2015
// Author: Eric S Policaro

using System;
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
        /// Dispose the credential.
        /// </summary>
        public void Dispose()
        {
            try
            {   
                Password = new byte[0];
            }
            catch (Exception) { }
        }
    }
}

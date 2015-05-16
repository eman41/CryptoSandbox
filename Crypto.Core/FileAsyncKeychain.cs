// FileAsyncKeychain.cs,  5/16/2015
// Author: Eric S Policaro

using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Crypto.Core
{
    /// <summary>
    /// This class is a keychain that stores credentials in the filesytem.
    /// </summary>
    public class FileAsyncKeychain : IAsyncKeychain
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="FileAsyncKeychain"/>
        /// with the given folder where keys are located.
        /// </summary>
        /// <param name="keyFolder">Folder containing keys</param>
        public FileAsyncKeychain(string keyFolder)
        {
            _keyFolder = keyFolder;
        }

        public bool KeyExists(string key)
        {
            return File.Exists(GetKeyPath(key));
        }

        public async Task<Credential> RetrieveAsync(string key)
        {
            if (!KeyExists(key))
                return Credential.Empty;

            using(var stream = File.Open(GetKeyPath(key), FileMode.Open))
            using(var reader = new StreamReader(stream))
            {
                string stored = await reader.ReadLineAsync();
                string[] parts = stored.Split(_credSeparator);

                string username = parts[0];
                string encryptedPassword = parts[1];

                return new Credential(username, Decrypt(encryptedPassword));
            }
        }

        public async Task StoreAsync(string key, Credential creds)
        {
            using(var stream = File.Open(GetKeyPath(key), FileMode.Create))
            using(var writer = new StreamWriter(stream))
            {
                byte[] encryptedPassword = Encrypt(creds.Password);
                string toStore = creds.Username + _credSeparator + ToBase64(encryptedPassword);

                await writer.WriteLineAsync(toStore);

                encryptedPassword = new byte[0];
            }
        }

        private byte[] Encrypt(byte[] data)
        {
            return ProtectedData.Protect(data, null, _protectionScope);
        }

        private byte[] Decrypt(string encrypted)
        {
            byte[] decodedData = FromBase64(encrypted);
            return ProtectedData.Unprotect(decodedData, null, _protectionScope);
        }

        private byte[] FromBase64(string line)
        {
            return Convert.FromBase64String(line);
        }

        private string ToBase64(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        private string GetKeyPath(string key)
        {
            return Path.Combine(_keyFolder, key);
        }

        private char _credSeparator = '|';
        private string _keyFolder;
        private DataProtectionScope _protectionScope = DataProtectionScope.CurrentUser;
    }
}

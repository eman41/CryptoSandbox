// FileAsyncKeychain.cs,  5/16/2015
// Author: Eric S Policaro

using System.IO;
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
                return Credential.Decode(stored);
            }
        }

        public async Task StoreAsync(string key, Credential creds)
        {
            using(var stream = File.Open(GetKeyPath(key), FileMode.Create))
            using(var writer = new StreamWriter(stream))
            {
                string encoded = creds.Encode();

                await writer.WriteLineAsync(encoded);
            }
        }

        private string GetKeyPath(string key)
        {
            return Path.Combine(_keyFolder, key);
        }

        private char _separator = '|';
        private string _keyFolder;
    }
}

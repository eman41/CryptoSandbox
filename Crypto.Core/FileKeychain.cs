// FileAsyncKeychain.cs,  5/16/2015
// Author: Eric S Policaro

using System.IO;
using System.Threading.Tasks;

namespace Crypto.Core
{
    /// <summary>
    /// This class is a keychain that stores each credential as an 
    /// individual file (named for the key) in a folder on the local file system.
    /// 
    /// For better test integration in larger apps, you could inject System.IO.Abstractions:
    /// https://github.com/tathamoddie/System.IO.Abstractions
    /// </summary>
    public class FileAsyncKeychain : IKeychain
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

        public Credential Retrieve(string key)
        {
            if (!KeyExists(key))
                return Credential.Empty;

            string stored = File.ReadAllText(GetKeyPath(key));
            return Credential.Decode(stored);
        }

        public async Task<Credential> RetrieveAsync(string key)
        {
            if (!KeyExists(key))
                return Credential.Empty;

            using(var stream = File.Open(GetKeyPath(key), FileMode.Open))
            using(var reader = new StreamReader(stream))
            {
                string stored = await reader.ReadLineAsync().ConfigureAwait(false);
                return Credential.Decode(stored);
            }
        }

        public void Store(string key, Credential creds)
        {
            CreateKeyFolder(key);

            File.WriteAllText(GetKeyPath(key), creds.Encoded());
        }

        public async Task StoreAsync(string key, Credential creds)
        {
            CreateKeyFolder(key);

            using(var stream = File.Open(GetKeyPath(key), FileMode.Create))
            using(var writer = new StreamWriter(stream))
            {
                string encoded = creds.Encoded();
                await writer.WriteLineAsync(encoded).ConfigureAwait(false);
            }
        }

        private void CreateKeyFolder(string key)
        {
            new FileInfo(GetKeyPath(key)).Directory.Create();
        }

        private string GetKeyPath(string key)
        {
            return Path.Combine(_keyFolder, key);
        }

        private string _keyFolder;
    }
}

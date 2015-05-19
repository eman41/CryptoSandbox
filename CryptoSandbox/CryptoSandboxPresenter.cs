// CryptoSandboxPresenter.cs,  5/16/2015
// Author: Eric S Policaro

using Crypto.Core;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSandbox
{
    /// <summary>
    /// A presenter class for testing credential encryption.
    /// </summary>
    public class CryptoSandboxPresenter
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="CryptoSandboxPresenter"/>
        /// with the given keychain.
        /// </summary>
        /// <param name="keychain">Keychain to test</param>
        public CryptoSandboxPresenter(IKeychain keychain)
        {
            _keychain = keychain;
            Clear();
        }

        /// <summary>
        /// Gets or Sets the username for the credentials to be encrypted.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets the password for the credentials to be encrypted.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets the last decrypted username.
        /// </summary>
        public string RetrievedUsername { get; private set; }

        /// <summary>
        /// Gets the last decrypted password.
        /// </summary>
        public string RetrievedPassword  { get; private set; }

        /// <summary>
        /// Gets or Sets the unique identifier for credentials to be saved or retrieved.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Clear all stored and retrieved credentials.
        /// </summary>
        public void Clear()
        {
            Username = string.Empty;
            RetrievedUsername = string.Empty;
            Password = string.Empty;
        }

        /// <summary>
        /// Gets if the there if a key exists and can be decrypted.
        /// </summary>
        public bool CanDecrypt()
        {
            return !_working && IsSiteKeyValid && _keychain.KeyExists(Key);
        }

        /// <summary>
        /// Gets if the username and password are valid for encryption.
        /// </summary>
        public bool CanEncrypt()
        {
            return !_working && IsSiteKeyValid && IsUsernameValid && IsPasswordValid;
        }

        /// <summary>
        /// Encrypt the entered username and password for the specified key.
        /// </summary>
        /// <returns>Awaitable task</returns>
        public async Task EncryptAsync()
        {
            if (!CanEncrypt())
                return;

            using(var cred = new Credential(Username, Password))
            {
                _working = true;
                await _keychain.StoreAsync(Key, cred);
                _working = false;

                Clear();
            }
        }

        /// <summary>
        /// Retrieve the credentials for the selected Key.
        /// </summary>
        /// <returns>Awaitable task</returns>
        public async Task RetrieveAsync()
        {
            if (!CanDecrypt())
                return;

            _working = true;
            using(Credential cred = await _keychain.RetrieveAsync(Key))
            {
                RetrievedUsername = cred.Username;
                RetrievedPassword = cred.GetPasswordString();
            } // This tester is the final credential client, responsible for disposing any creds
            _working = false;
        }

        private bool IsUsernameValid
        {
            get { return !string.IsNullOrEmpty(Username); }
        }

        private bool IsPasswordValid
        {
            get { return !string.IsNullOrEmpty(Password); }
        }

        private bool IsSiteKeyValid
        {
            get { return !string.IsNullOrEmpty(Key); }
        }

        private bool _working;
        private IKeychain _keychain;
    }
}

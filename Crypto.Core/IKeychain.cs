// IAsyncKeychain.cs,  5/16/2015
// Author: Eric S Policaro

using System.Threading.Tasks;

namespace Crypto.Core
{
    /// <summary>
    /// This interfaces defines a credential store that retrieves
    /// and stores credentials asyncronously.
    /// </summary>
    public interface IKeychain
    {
        /// <summary>
        /// Verifies that a credential exists for the given key.
        /// </summary>
        /// <param name="key">Keychain key name</param>
        /// <returns>True if the credential exists</returns>
        bool KeyExists(string key);

        /// <summary>
        /// Retrieve the credential for the given key.
        /// </summary>
        /// <param name="key">Keychain key name</param>
        /// <returns>
        /// Credential stored for the given key.
        /// An empty credential is returned if a credential could not be retrieved.
        /// </returns>
        Task<Credential> RetrieveAsync(string key);

        /// <summary>
        /// Retrieve the credential for the given key.
        /// </summary>
        /// <param name="key">Keychain key name</param>
        /// <returns>
        /// Credential stored for the given key.
        /// An empty credential is returned if a credential could not be retrieved.
        /// </returns>
        Credential Retrieve(string key);

        /// <summary>
        /// Stores the credential for the given key name. If the key
        /// already has a stored credential, it will be replaced.
        /// </summary>
        /// <param name="key">Keychain key name</param>
        /// <param name="creds">Credentials to store</param>
        void Store(string key, Credential creds);

        /// <summary>
        /// Stores the credential for the given key name. If the key
        /// already has a stored credential, it will be replaced.
        /// </summary>
        /// <param name="key">Keychain key name</param>
        /// <param name="creds">Credentials to store</param>
        /// <returns>Awaitable task</returns>
        Task StoreAsync(string key, Credential creds);
    }
}

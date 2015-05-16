// MainForm.cs,  5/16/2015
// Author: Eric S Policaro

using Crypto.Core;
using System;
using System.Windows.Forms;

namespace CryptoSandbox
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            encryptedPasswordTextBox.PasswordChar = _dot;

            var keychain = new FileAsyncKeychain(KeychainFolder);
            _cryptoTester = new CryptoSandboxPresenter(keychain);
        }

        private async void encryptButton_Click(object sender, EventArgs e)
        {
            SetWorkingState();

            await _cryptoTester.EncryptAsync();

            SetIdleState(FillEncryptionTextBoxes);
        }

        private async void decryptButton_Click(object sender, EventArgs e)
        {
            SetWorkingState();

            await _cryptoTester.RetrieveAsync();

            SetIdleState(FillDecryptionTextBoxes);
        }

        private void SiteKeyTextT_OnChanged(object sender, EventArgs e)
        {
            _cryptoTester.Key = siteKeyTextBox.Text;
            UpdateButtonStatuses();
        }

        private void Username_OnChanged(object sender, EventArgs e)
        {
            _cryptoTester.Username = encryptUsernameTextBox.Text;
            UpdateButtonStatuses();
        }

        private void Password_OnChanged(object sender, EventArgs e)
        {
            _cryptoTester.Password = encryptedPasswordTextBox.Text;
            UpdateButtonStatuses();
        }

        private void SetWorkingState()
        {
            statusLabel.Text = WorkingText;
            UpdateButtonStatuses();
        }

        private void SetIdleState(Action idleCallback)
        {
            statusLabel.Text = IdleText;
            UpdateButtonStatuses();
            idleCallback();
        }

        private void UpdateButtonStatuses()
        {
            encryptButton.Enabled = _cryptoTester.CanEncrypt();
            decryptButton.Enabled = _cryptoTester.CanDecrypt();
        }

        private void FillDecryptionTextBoxes()
        {
            decryptUsernameTextBox.Text = _cryptoTester.RetrievedUsername;
            decryptedPasswordTextBox.Text = _cryptoTester.RetrievedPassword;
        }

        private void FillEncryptionTextBoxes()
        {
            encryptUsernameTextBox.Text = _cryptoTester.Username;
            encryptedPasswordTextBox.Text = _cryptoTester.Password;
        }

        private CryptoSandboxPresenter _cryptoTester;
        private char _dot = '\u25CF';

        private readonly string KeychainFolder = @"C:\keychain-testing";
        private readonly string IdleText = "Idle";
        private readonly string WorkingText = "Working...";
    }
}

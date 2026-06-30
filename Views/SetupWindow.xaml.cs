using System;
using System.Configuration;
using System.IO;
using System.Windows;

namespace DataIntegration.Views
{
    public partial class SetupWindow : Window
    {
        public SetupWindow()
        {
            InitializeComponent();
            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            try
            {
                txtStoreId.Text = ConfigurationManager.AppSettings["StoreId"] ?? "230";
                txtStoreName.Text = ConfigurationManager.AppSettings["StoreName"] ?? "Store-230";
                txtDays.Text = ConfigurationManager.AppSettings["Days"] ?? "1";
                txtDataSource.Text = ConfigurationManager.AppSettings["DataSource"] ?? "cc.eoi.wecreatives.agency:1525/SCAR";
                txtUserId.Text = ConfigurationManager.AppSettings["UserID"] ?? "PJSALE";
                txtPassword.Text = ConfigurationManager.AppSettings["Password"] ?? "PJSALE";

                string waEnabled = ConfigurationManager.AppSettings["WhatsAppEnabled"];
                chkWhatsApp.IsChecked = !string.IsNullOrWhiteSpace(waEnabled) && bool.TryParse(waEnabled, out bool enabled) && enabled;

                txtWhatsAppPhone.Text = ConfigurationManager.AppSettings["WhatsAppToPhone"] ?? "+923021656678";
                txtWhatsAppToken.Text = ConfigurationManager.AppSettings["WhatsAppToken"] ?? "hlinq7ipyknr5mu5";
                txtWhatsAppUrl.Text = ConfigurationManager.AppSettings["WhatsAppApiUrl"] ?? "https://api.ultramsg.com/instance183090/messages/chat";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading current settings: {ex.Message}", "Setup Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtStoreId.Text) || 
                    string.IsNullOrWhiteSpace(txtStoreName.Text) || 
                    string.IsNullOrWhiteSpace(txtDays.Text) || 
                    string.IsNullOrWhiteSpace(txtDataSource.Text) || 
                    string.IsNullOrWhiteSpace(txtUserId.Text) || 
                    string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please fill out all store and database fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (chkWhatsApp.IsChecked == true && 
                    (string.IsNullOrWhiteSpace(txtWhatsAppPhone.Text) || 
                     string.IsNullOrWhiteSpace(txtWhatsAppToken.Text) || 
                     string.IsNullOrWhiteSpace(txtWhatsAppUrl.Text)))
                {
                    MessageBox.Show("Please fill out all WhatsApp settings if notifications are enabled.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Open App.config and update appSettings
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                
                config.AppSettings.Settings["StoreId"].Value = txtStoreId.Text.Trim();
                config.AppSettings.Settings["StoreName"].Value = txtStoreName.Text.Trim();
                config.AppSettings.Settings["Days"].Value = txtDays.Text.Trim();
                config.AppSettings.Settings["DataSource"].Value = txtDataSource.Text.Trim();
                config.AppSettings.Settings["UserID"].Value = txtUserId.Text.Trim();
                config.AppSettings.Settings["Password"].Value = txtPassword.Text.Trim();
                config.AppSettings.Settings["WhatsAppEnabled"].Value = chkWhatsApp.IsChecked == true ? "true" : "false";
                config.AppSettings.Settings["WhatsAppToPhone"].Value = txtWhatsAppPhone.Text.Trim();
                config.AppSettings.Settings["WhatsAppToken"].Value = txtWhatsAppToken.Text.Trim();
                config.AppSettings.Settings["WhatsAppApiUrl"].Value = txtWhatsAppUrl.Text.Trim();

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                // Write the setup_done.txt file to mark setup as complete
                File.WriteAllText("setup_done.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings to App.config: {ex.Message}\nMake sure you have write permissions to the application folder.", "Setup Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace cSharpPassAttack
{
    public partial class PasswordCreation : Window
    {
        public string _username { get; set; } = string.Empty;
        private string _password { get; set; } = string.Empty;
        private static readonly Random _random = new Random();
        private static readonly string _charset = "abcdefghijklmnopqrstuvwxyz0123456789";


        public PasswordCreation()
        {
            InitializeComponent();
            Left = 500;
            Top = 300;
        }
        public static string Generate3CharString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                int index = _random.Next(_charset.Length);
                sb.Append(_charset[index]);
            }

            return sb.ToString();
        }
        private void handlePasswordFieldPlaceHodler(object sender, EventArgs e)
        {
            if (x_createPassword.Text == "")
                x_createPassword.Text = "Enter Password";
            else if (x_createPassword.Text == "Enter Password")
                x_createPassword.Text = "";
        }
        private void handleUsernameFieldPlaceHolder(object sender, EventArgs e)
        {
            if (x_userName.Text == "")
                x_userName.Text = "Enter Username";
            else if (x_userName.Text == "Enter Username")
                x_userName.Text = "";
        }
        private void handlePasswordFieldInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            _password = textBox.Text + e.Text;

        }
        private void handleUsernameFieldInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            _username = textBox.Text + e.Text;
        }
        private void handleSaveBtn(object sender, EventArgs e)
        {
            string path = "passwords.csv";

            string encryptedPassword = AESEncryption.Encrypt(_password, Generate3CharString());
            string[] row1 = { $"{_username}", $"{encryptedPassword}", $"{_password}" };

            if (!File.Exists(path) || new FileInfo(path).Length == 0)
            {
                File.WriteAllText(path, string.Join(",", row1));
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(string.Join(",", row1));
                }
            }

            MessageBox.Show($"Password Saved");
        }
    }
}

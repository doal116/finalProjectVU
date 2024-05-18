using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.IO;
using System;

namespace cSharpPassAttack
{
    public partial class BreakPasswordSelect : Window
    {
        public struct Elem
        {
            public string Username { get; }
            public string EncryptPass { get; }
            public string Expected { get; }
            public Elem(string username, string encryptPass, string expected)
            {
                Username = username;
                EncryptPass = encryptPass;
                Expected = expected;
            }
        } 
        public ObservableCollection<Elem> Users { get; set; }
        public BreakPasswordSelect()
        {

            Users = new ObservableCollection<Elem> { };
            string filePath = "passwords.csv";
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {

                        string line = reader.ReadLine() ?? string.Empty;
                        string[] fields = line.Split(',');
                        Users.Add(new Elem($"{fields[0]}", $"{fields[1]}", $"{fields[2]}"));

                    }
                }
            }
            catch (Exception) { }



            InitializeComponent();

            Left = 500;
            Top = 300;
        }
        private void btnBreakSelectedUser(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Elem user = (Elem)button.Tag;
            BruteForce breakingProcess = new BruteForce(user.Username, user.EncryptPass, user.Expected);
            this.Close();
            breakingProcess.Show();
        }
    }
}


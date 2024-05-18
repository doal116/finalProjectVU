using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;
using System.Diagnostics;

namespace cSharpPassAttack
{
    public partial class BruteForce : Window
    {
        public string _username { get; }
        public string _password { get; }
        public string _expectedPass { get; }
        public int _numOfCores { get; }
        public int _selectedThreads { get; set; }
        public string _decrytpedWithBruteForce { get; set; } = string.Empty;
        public BruteForce(string username, string encryptedPassword, string expectedPassword)
        {
            _username = username;
            _password = encryptedPassword;
            _expectedPass = expectedPassword;
            _numOfCores = Environment.ProcessorCount;
            _selectedThreads = 4;
            Left = 500;
            Top = 300;
            InitializeComponent();
            DataContext = this;

        }

        private void handleThreadNumInputnum(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string updatedText = textBox.Text + e.Text;
            int coreNum;
            bool isNum = int.TryParse(updatedText, out coreNum);
            e.Handled = isNum ? !(coreNum < _numOfCores * 2) : true;
            _selectedThreads = !e.Handled ? coreNum : _selectedThreads;
        }

        private void handleBruteForceBtnPress(object sender, EventArgs e)
        {
            char[] elems = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int n = elems.Length;
            int r = 3;
            int maxThreads = _selectedThreads;
            var stopwatch = Stopwatch.StartNew();
            List<List<char>> allPermutations = Permutation.PermutationWithRepetition(n, r, elems, maxThreads, _password, _expectedPass);
            stopwatch.Stop();
            double seconds = stopwatch.Elapsed.TotalSeconds;

            x_TimeElapsed.Text = seconds.ToString() + " ( seconds ) ";

            foreach (var permutation in allPermutations)
            {
                string permutationStr = new string(permutation.ToArray());

                if (permutationStr == Permutation._saltFound)
                {
                    _decrytpedWithBruteForce = AESEncryption.Decrypt(_password, permutationStr);
                    x_decryptedPassBruteFoce.Text = _decrytpedWithBruteForce;
                    var item = new ListBoxItem
                    {
                        Content = permutationStr,
                        Background = Brushes.Gray,
                        Foreground = Brushes.White
                    };
                    x_resultListBox.Items.Add(item);

                    x_resultListBox.ScrollIntoView(item);
                }
                else
                    x_resultListBox.Items.Add(permutationStr);
            }
            x_resultListBox.Visibility = Visibility.Visible;
        }
    }
}
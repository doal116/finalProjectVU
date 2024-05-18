using System.Windows;

namespace cSharpPassAttack
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Left = 500;
            Top = 300;
        }
        private void btnCreatePassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordCreation passwordCreationWindow = new PasswordCreation();
           
            passwordCreationWindow.Show();
        }
        private void btnBruteForceList_Click(object sender, RoutedEventArgs e)
        {
            BreakPasswordSelect breakPasswordSelectWindow = new BreakPasswordSelect();
           
            breakPasswordSelectWindow.Show();
        }
    }
}



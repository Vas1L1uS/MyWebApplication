using MyWpfApp.AuthClientApp;
using MyWpfApp.Data;
using MyWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user;
        ClientDataApi clientDataApi;
        public MainWindow()
        {
            InitializeComponent();
            clientDataApi = new ClientDataApi();

            UserLogin userLogin = new UserLogin()
            {
                LoginProp = "Admin",
                Password = "Admin1!"
            };

            UserLogin userLogin1 = new UserLogin()
            {
                LoginProp = "User2",
                Password = "User2!"
            };

            A(userLogin1);
            var r = 32;
        }

        async void A(UserLogin userLogin)
        {
            user = await clientDataApi.Login(userLogin);
            var b = clientDataApi.GetUserRole(user.Id);
        }
    }
}

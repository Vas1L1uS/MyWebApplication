using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MyWpfApp.AuthClientApp;
using MyWpfApp.Data;
using MyWpfApp.Entities;
using MyWpfApp.Infrastructure.Commands;

namespace MyWpfApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            _clientDataApi = new ClientDataApi();

            _gridMain = new Grid();
            _gridMain.Visibility = Visibility.Visible;
            _gridRegister = new Grid();
            _gridRegister.Visibility = Visibility.Collapsed;
            _gridLogin = new Grid();
            _gridLogin.Visibility = Visibility.Collapsed;
            _gridClient = new Grid();
            _gridClient.Visibility = Visibility.Collapsed;
            _gridUsers = new Grid();
            _gridUsers.Visibility = Visibility.Collapsed;
            _editButton = new Grid();
            _addClientButton = new Grid();

            _vNotAuth = Visibility.Visible;
            _vDefaultUser = Visibility.Collapsed;
            _vAdmin = Visibility.Collapsed;

            _selectedClient = new Client();
            _viewSelectedClient = new Client();
            _clientsList = new ObservableCollection<Client>(_clientDataApi.GetAll());

            _selectedUser = new User();
            _usersList = new ObservableCollection<User>();
            _myAccount = new User();
            _isNotAdmin = true;
            _regAccount = new UserRegistration();
            _accountLogin = new UserLogin();

            #region Команды 

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            GetAllCommand = new LambdaCommand(OnGetAllCommandExecuted, CanGetAllCommandExecute);
            GetAllUsersCommand = new LambdaCommand(OnGetAllUsersCommandExecuted, CanGetAllUsersCommandExecute);
            ToViewRegisterCommand = new LambdaCommand(OnToViewRegisterCommandExecuted, CanToViewRegisterCommandExecute);
            ToViewLoginCommand = new LambdaCommand(OnToViewLoginCommandExecuted, CanToViewLoginCommandExecute);
            ToViewMainCommand = new LambdaCommand(OnToViewMainCommandExecuted, CanToViewMainCommandExecute);
            ToViewClientCommand = new LambdaCommand(OnToViewClientCommandExecuted, CanToViewClientCommandExecute);
            ToViewEditClientCommand = new LambdaCommand(OnToViewEditClientCommandExecuted, CanToViewEditClientCommandExecute);
            RegisterCommand = new LambdaCommand(OnRegisterCommandExecuted, CanRegisterCommandExecute);
            LoginCommand = new LambdaCommand(OnLoginCommandExecuted, CanLoginCommandExecute);
            LogoutCommand = new LambdaCommand(OnLogoutCommandExecuted, CanLogoutCommandExecute);
            ToViewUsersCommand = new LambdaCommand(OnToViewUsersCommandExecuted, CanToViewUsersCommandExecute);
            EditClientCommand = new LambdaCommand(OnEditClientCommandExecuted, CanEditClientCommandExecute);
            DeleteClientCommand = new LambdaCommand(OnDeleteClientCommandExecuted, CanDeletetClientCommandExecute);
            DeleteUserCommand = new LambdaCommand(OnDeleteUserCommandExecuted, CanDeleteUserCommandExecute);
            AddClientCommand = new LambdaCommand(OnAddClientCommandExecuted, CanAddClientCommandExecute);
            ToViewAddClientCommand = new LambdaCommand(OnToViewAddClientCommandExecuted, CanToViewAddClientCommandExecute);

            #endregion
        }

        private ClientDataApi _clientDataApi;

        private bool _isNotAdmin;
        public bool IsNotAdmin { get => _isNotAdmin; set => Set(ref _isNotAdmin, value); }

        private bool _isNotEditClient;
        public bool IsNotEditClient { get => _isNotEditClient; set => Set(ref _isNotEditClient, value); }

        private Visibility _vAdmin;
        public Visibility VAdmin { get => _vAdmin; set => Set(ref _vAdmin, value); }

        private Visibility _vDefaultUser;
        public Visibility VDefaultUser { get => _vDefaultUser; set => Set(ref _vDefaultUser, value); }

        private Visibility _vNotAuth;
        public Visibility VNotAuth { get => _vNotAuth; set => Set(ref _vNotAuth, value); }

        private Grid _gridMain;
        public Grid GridMain
        {
            get => _gridMain;
            set => Set(ref _gridMain, value);
        }

        private Grid _gridRegister;
        public Grid GridRegister
        {
            get => _gridRegister;
            set => Set(ref _gridRegister, value);
        }

        private Grid _gridLogin;
        public Grid GridLogin
        {
            get => _gridLogin;
            set => Set(ref _gridLogin, value);
        }

        private Grid _gridClient;
        public Grid GridClient
        {
            get => _gridClient;
            set => Set(ref _gridClient, value);
        }

        private Grid _gridUsers;
        public Grid GridUsers
        {
            get => _gridUsers;
            set => Set(ref _gridUsers, value);
        }

        private UIElement _editButton;
        public UIElement EditButton
        {
            get => _editButton;
            set => Set(ref _editButton, value);
        }

        private UIElement _addClientButton;
        public UIElement AddClientButton
        {
            get => _addClientButton;
            set => Set(ref _addClientButton, value);
        }

        private Client _viewSelectedClient;
        public Client ViewSelectedClient { get => _viewSelectedClient; set => Set(ref _viewSelectedClient, value); }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                Set(ref _selectedClient, value);
                if(ViewSelectedClient != null && SelectedClient != null)
                {
                    ViewSelectedClient = SelectedClient.Clone() as Client;
                }
            }
        }

        private ObservableCollection<Client> _clientsList;
        public ObservableCollection<Client> ClientsList { get => _clientsList; set => Set(ref _clientsList, value); }

        private User _viewSelectedUser;
        public User ViewSelectedUser { get => _viewSelectedUser; set => Set(ref _viewSelectedUser, value); }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                Set(ref _selectedUser, value);
                if (ViewSelectedUser != null && SelectedUser != null)
                {
                    ViewSelectedUser = SelectedUser.Clone() as User;
                }
            }
        }

        private ObservableCollection<User> _usersList;
        public ObservableCollection<User> UsersList { get => _usersList; set => Set(ref _usersList, value); }

        private User _myAccount;
        public User MyAccount { get => _myAccount; set => Set(ref _myAccount, value); }

        private UserLogin _accountLogin;
        public UserLogin AccountLogin { get => _accountLogin; set => Set(ref _accountLogin, value); }

        private UserRegistration _regAccount;
        public UserRegistration RegAccount { get => _regAccount; set => Set(ref _regAccount, value); }

        #region Команды

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        public ICommand RegisterCommand { get; }

        private bool CanRegisterCommandExecute(object p) => true;
        private async void OnRegisterCommandExecuted(object p)
        {
            if (RegAccount.Password != RegAccount.ConfirmPassword)
            {
                return;
            }
            bool result = _clientDataApi.AddUser(_regAccount);
            if (result)
            {
                _accountLogin.LoginProp = _regAccount.LoginProp;
                _accountLogin.Password = _regAccount.Password;
                _myAccount = await _clientDataApi.Login(_accountLogin);
                if (_myAccount == null)
                {
                    return;
                }
                _myAccount.Role = _clientDataApi.GetUserRole(_myAccount.Id);
                if (_myAccount.Role == "1")
                {
                    IsNotAdmin = false;
                    VAdmin = Visibility.Visible;
                }
                else
                {
                    IsNotAdmin = true;
                    VAdmin = Visibility.Collapsed;
                }
                VNotAuth = Visibility.Collapsed;
                VDefaultUser = Visibility.Visible;

                _gridLogin.Visibility = Visibility.Collapsed;
                _gridRegister.Visibility = Visibility.Collapsed;
                _gridClient.Visibility = Visibility.Collapsed;
                _gridUsers.Visibility = Visibility.Collapsed;
                _gridMain.Visibility = Visibility.Visible;
            }
        }

        public ICommand ToViewRegisterCommand { get; }

        private bool CanToViewRegisterCommandExecute(object p) => true;
        private void OnToViewRegisterCommandExecuted(object p)
        {
            _gridMain.Visibility = Visibility.Collapsed;
            _gridLogin.Visibility = Visibility.Collapsed;
            _gridClient.Visibility = Visibility.Collapsed;
            _gridUsers.Visibility = Visibility.Collapsed;
            _gridRegister.Visibility = Visibility.Visible;
        }

        public ICommand LogoutCommand { get; }
        private bool CanLogoutCommandExecute(object p) => true;
        private void OnLogoutCommandExecuted(object p)
        {
            _myAccount = null;
            VAdmin = Visibility.Collapsed;
            VDefaultUser = Visibility.Collapsed;
            VNotAuth = Visibility.Visible;
            _isNotAdmin = true;

            _gridMain.Visibility = Visibility.Visible;
            _gridLogin.Visibility = Visibility.Collapsed;
            _gridClient.Visibility = Visibility.Collapsed;
            _gridUsers.Visibility = Visibility.Collapsed;
            _gridRegister.Visibility = Visibility.Collapsed;
        }

        public ICommand LoginCommand { get; }

        private bool CanLoginCommandExecute(object p) => true;
        private async void OnLoginCommandExecuted(object p)
        {
            var account = await _clientDataApi.Login(_accountLogin);
            if (account != null)
            {
                _myAccount = await _clientDataApi.Login(_accountLogin);
                _myAccount.Role = _clientDataApi.GetUserRole(_myAccount.Id);
                if (_myAccount.Role == "1")
                {
                    IsNotAdmin = false;
                    VAdmin = Visibility.Visible;
                }
                else
                {
                    IsNotAdmin = true;
                    VAdmin = Visibility.Collapsed;
                }
                VNotAuth = Visibility.Collapsed;
                VDefaultUser = Visibility.Visible;

                _gridLogin.Visibility = Visibility.Collapsed;
                _gridRegister.Visibility = Visibility.Collapsed;
                _gridClient.Visibility = Visibility.Collapsed;
                _gridUsers.Visibility = Visibility.Collapsed;
                _gridMain.Visibility = Visibility.Visible;
            }
        }

        public ICommand ToViewLoginCommand { get; }

        private bool CanToViewLoginCommandExecute(object p) => true;
        private void OnToViewLoginCommandExecuted(object p)
        {
            _gridMain.Visibility = Visibility.Collapsed;
            _gridRegister.Visibility = Visibility.Collapsed;
            _gridClient.Visibility = Visibility.Collapsed;
            _gridUsers.Visibility = Visibility.Collapsed;
            _gridLogin.Visibility = Visibility.Visible;
        }

        public ICommand ToViewMainCommand { get; }

        private bool CanToViewMainCommandExecute(object p) => true;
        private void OnToViewMainCommandExecuted(object p)
        {
            _gridLogin.Visibility = Visibility.Collapsed;
            _gridRegister.Visibility = Visibility.Collapsed;
            _gridClient.Visibility = Visibility.Collapsed;
            _gridUsers.Visibility = Visibility.Collapsed;
            _gridMain.Visibility = Visibility.Visible;
        }

        public ICommand ToViewClientCommand { get; }
        private bool CanToViewClientCommandExecute(object p) => true;
        private void OnToViewClientCommandExecuted(object p)
        {
            _gridLogin.Visibility = Visibility.Collapsed;
            _gridRegister.Visibility = Visibility.Collapsed;
            _gridMain.Visibility = Visibility.Collapsed;
            _gridUsers.Visibility = Visibility.Collapsed;
            IsNotEditClient = true;
            _editButton.Visibility = Visibility.Collapsed;
            _addClientButton.Visibility = Visibility.Collapsed;
            _gridClient.Visibility = Visibility.Visible;
        }

        public ICommand ToViewEditClientCommand { get; }
        private bool CanToViewEditClientCommandExecute(object p) => true;
        private void OnToViewEditClientCommandExecuted(object p)
        {
            _gridLogin.Visibility = Visibility.Collapsed;
            _gridRegister.Visibility = Visibility.Collapsed;
            _gridMain.Visibility = Visibility.Collapsed;
            _gridUsers.Visibility = Visibility.Collapsed;
            _addClientButton.Visibility = Visibility.Collapsed;
            IsNotEditClient = false;
            _editButton.Visibility = Visibility.Visible;
            _gridClient.Visibility = Visibility.Visible;
        }

        public ICommand ToViewAddClientCommand { get; }
        private bool CanToViewAddClientCommandExecute(object p) => true;
        private void OnToViewAddClientCommandExecuted(object p)
        {
            _selectedClient = new Client();
            _gridLogin.Visibility = Visibility.Collapsed;
            _gridRegister.Visibility = Visibility.Collapsed;
            _gridMain.Visibility = Visibility.Collapsed;
            _gridUsers.Visibility = Visibility.Collapsed;
            _editButton.Visibility = Visibility.Collapsed;
            IsNotEditClient = false;
            _addClientButton.Visibility = Visibility.Visible;
            _gridClient.Visibility = Visibility.Visible;
        }

        public ICommand ToViewUsersCommand { get; }

        private bool CanToViewUsersCommandExecute(object p) => true;
        private void OnToViewUsersCommandExecuted(object p)
        {
            _gridLogin.Visibility = Visibility.Collapsed;
            _gridRegister.Visibility = Visibility.Collapsed;
            _gridMain.Visibility = Visibility.Collapsed;
            _editButton.Visibility = Visibility.Collapsed;
            _gridClient.Visibility = Visibility.Collapsed;
            _gridUsers.Visibility = Visibility.Visible;
            UsersList = new ObservableCollection<User>(_clientDataApi.GetAllUsers());
        }

        public ICommand GetAllCommand { get; }

        private bool CanGetAllCommandExecute(object p) => true;

        private void OnGetAllCommandExecuted(object p)
        {
            ClientsList = new ObservableCollection<Client>(_clientDataApi.GetAll());
        }

        public ICommand AddClientCommand { get; }
        private bool CanAddClientCommandExecute(object p) => true;

        private void OnAddClientCommandExecuted(object p)
        {
            _clientDataApi.AddClient(_viewSelectedClient);
            ClientsList = new ObservableCollection<Client>(_clientDataApi.GetAll());
            _gridLogin.Visibility = Visibility.Collapsed;
            _gridRegister.Visibility = Visibility.Collapsed;
            _gridClient.Visibility = Visibility.Collapsed;
            _gridUsers.Visibility = Visibility.Collapsed;
            _gridMain.Visibility = Visibility.Visible;
        }

        public ICommand EditClientCommand { get; }
        private bool CanEditClientCommandExecute(object p) => true;

        private void OnEditClientCommandExecuted(object p)
        {
            _clientDataApi.Edit(_viewSelectedClient);
            ClientsList = new ObservableCollection<Client>(_clientDataApi.GetAll());
        }

        public ICommand DeleteClientCommand { get; }
        private bool CanDeletetClientCommandExecute(object p) => true;

        private void OnDeleteClientCommandExecuted(object p)
        {
            _clientDataApi.Delete(_selectedClient.Id);
            _selectedClient = new Client();
            ClientsList = new ObservableCollection<Client>(_clientDataApi.GetAll());
        }

        public ICommand GetAllUsersCommand { get; }

        private bool CanGetAllUsersCommandExecute(object p) => true;

        private void OnGetAllUsersCommandExecuted(object p)
        {
            UsersList = (ObservableCollection<User>)_clientDataApi.GetAllUsers();
        }

        public ICommand DeleteUserCommand { get; }

        private bool CanDeleteUserCommandExecute(object p) => true;

        private void OnDeleteUserCommandExecuted(object p)
        {
            _clientDataApi.DeleteUser(_selectedUser.Id);
            UsersList = new ObservableCollection<User>(_clientDataApi.GetAllUsers());
        }

        #endregion

        #region Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _title = "Телефонная книга";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion
    }
}

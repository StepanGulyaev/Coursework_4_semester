using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Coursework_client.DB;

namespace Coursework_client
    {
    public partial class LoginWindow : Window
        {
        #region fields
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        #endregion

        public LoginWindow() => InitializeComponent();

        private async void login(object sender, RoutedEventArgs e)
            {
            var uname = inputUsername.Text;
            var pwd = inputPassword.Password;
            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(pwd))
                {
                MessageBox.Show("Заполните все поля!", "Ошибка");
                return;
                }

            if (await DB.User.checkConnection(uname, pwd))
                {
                var mw = new MainWindow(uname, pwd);
                mw.Show();
                Close();
                }
            else
                {
                MessageBox.Show("Введены неправильные логин или пароль!", "Ошибка");
                }
            }

        #region utils
        private void toggleTheme(object sender, RoutedEventArgs e)
            {
            var theme = paletteHelper.GetTheme();

            if (IsDarkTheme)
                {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Dark);
                }
            else
                {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
                }
            paletteHelper.SetTheme(theme);
            }

        private void exitApp(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
            {
            base.OnMouseLeftButtonDown(e);
            DragMove();
            }
        #endregion
        }
    }

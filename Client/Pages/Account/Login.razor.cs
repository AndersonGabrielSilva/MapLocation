using BlazorGPS.Client.Utils;
using MapLocationShared.Model.Account;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGPS.Client.Pages.Account
{
    public class LoginBase : Base
    {
        #region Inject

        #endregion

        #region Properties
        public UserLogin User { get; set; }
        public bool isLogin { get; set; }
        public InputType PasswordInput = InputType.Password;
        #endregion

        #region Constructor
        public LoginBase()
        {
            User = new UserLogin();

            isLogin = false;
        }
        #endregion

        #region Events
        protected override void OnInitialized()
        {
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
        }

        public async void Login()
        {
            if (ValidateLogin())
            {
                isLogin = true;
                StateHasChanged();
                
                await Task.Delay(3000);

                isLogin = false;

            }
        }

        private bool ValidateLogin()
        {
            var result = true;

            if (!Equals(User, null))
            {
                Console.WriteLine(User.Username);
                Console.WriteLine(User.Password);

                if (string.IsNullOrEmpty(User.Username))
                {
                    Snackbar.Add($"Ops! O Login é obrigatório ! ", Severity.Error);
                    result = false;
                }

                if (string.IsNullOrEmpty(User.Password))
                {
                    Snackbar.Add($"Ops! A Senha é obrigatória ! ", Severity.Error);
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
        #endregion

        #region Antigo
        //public bool PasswordVisibility;
        //public InputType PasswordInput = InputType.Password;
        //public string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        //public void TogglePasswordVisibility()
        //{
        //    if (PasswordVisibility)
        //    {
        //        PasswordVisibility = false;
        //        PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        //        PasswordInput = InputType.Password;
        //    }
        //    else
        //    {
        //        PasswordVisibility = true;
        //        PasswordInputIcon = Icons.Material.Filled.Visibility;
        //        PasswordInput = InputType.Text;
        //    }
        //}
        #endregion


    }
}

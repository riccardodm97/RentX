﻿
using RentIT.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsUserAuthenticated { get; }

        Task<Utente> IscriviUtenteAsync(Utente user);
        
        Task<AuthenticationResponse> LoginAsync(string email, string password);

        Task<bool> LogoutAsync();
    }
}

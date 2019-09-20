﻿using RentIT.Services.Annuncio;
using RentIT.Models;
using RentIT.Models.Annuncio;
using RentIT.Services;
using RentIT.Services.Foto;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentIT.ViewModels
{
    public class GestioneAnnuncioViewModel : BaseViewModel<Ad>
    {
        Ad _annuncio;
        public Ad Annuncio
        {
            get
            {
                return _annuncio;
            }
            set
            {
                _annuncio = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<Image> _immagini;
        public ObservableCollection<Image> Immagini
        {
            get
            {
                return _immagini;
            }
            set
            {
                _immagini = value;
                OnPropertyChanged();
            }
        }

        bool _isPrenotato;
        public bool IsPrenotato
        {
            get
            {
                return _isPrenotato;
            }

            set
            {
                _isPrenotato = value;
                OnPropertyChanged();
            }
        }

        bool _isNotPrenotato;
        public bool IsNotPrenotato
        {
            get
            {
                return _isNotPrenotato;
            }

            set
            {
                _isNotPrenotato = value;
                OnPropertyChanged();
            }
        }
        readonly FotoService _fotoService;
        readonly IAnnuncioService _annuncioService;
        public GestioneAnnuncioViewModel(INavService navService, AnnuncioService annuncioService, FotoService fotoService) : base(navService)
        {
            _annuncioService = annuncioService;
            _fotoService = fotoService;
        }

        public async override Task Init(Ad annuncio)
        {
            Annuncio = annuncio;
            /* Da decommentare
            * var imagesModel = await _fotoService.GetAdImagesAsync(Annuncio.id);
            * Immagini = _fotoService.creaImmagini(imagesModel);
            
            IsPrenotato = _annuncioService.isPrenotato(Annuncio.id);
            IsNotPrenotato = !IsPrenotato;
            */
        }

    /*Comando per eliminare l'annuncio*/
    Command _eliminaAnnuncioCommand;
        public Command EliminaAnnuncioCommand
        {
            get
            {
                return _eliminaAnnuncioCommand
                    ?? (_eliminaAnnuncioCommand = new Command(async () => await ExecuteEliminaAnnuncioCommand()));
            }
        }

        async Task ExecuteEliminaAnnuncioCommand()
        {
            IsBusy = true;
            
            var response = await _annuncioService.DeleteAdAsync(Annuncio.id);
            if (response.hasSucceded)
            {
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", response.responseMessage, "Ok");
            }

            IsBusy = false;
        }

        bool EmptyFields()
        {
            var empty = string.IsNullOrWhiteSpace(Annuncio.nomeOggetto) ||
                        string.IsNullOrWhiteSpace(Annuncio.descrizione) ||
                        (Annuncio.prezzo == 0);
            return empty;
        }

        /*Comando per modificare l'annuncio*/
        Command _modificaAnnuncioCommand;
        public Command ModificaAnnuncioCommand
        {
            get
            {
                return _modificaAnnuncioCommand
                    ?? (_modificaAnnuncioCommand = new Command(async () => await ExecuteModificaAnnuncioCommand()));
            }
        }

        async Task ExecuteModificaAnnuncioCommand()
        {
            IsBusy = true;

            if (!EmptyFields())
            {
                await App.Current.MainPage.DisplayAlert("Errore", "Non hai riempito uno o più campi", "Ok");
                return;
            }
            
            var response = await _annuncioService.ModifyAdDataAsync(Annuncio);
            if (response.hasSucceded)
            {
                StringBuilder successo = new StringBuilder();
                successo.Append("Annuncio modificato con successo!");
                await App.Current.MainPage.DisplayAlert("RentIT", successo.ToString(), "ok");
                await NavService.NavigateToMainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Errore", response.responseMessage, "Ok");
            }
            
            IsBusy = false;
        }

        /*Comando per prenotare l'annuncio*/
        Command _prenotaAnnuncioCommand;
        public Command PrenotaAnnuncioCommand
        {
            get
            {
                return _prenotaAnnuncioCommand
                    ?? (_prenotaAnnuncioCommand = new Command(async () => await ExecutePrenotaAnnuncioCommand()));
            }
        }

        async Task ExecutePrenotaAnnuncioCommand()
        {
            IsBusy = true;

            await _annuncioService.prenotaAd(Annuncio.id);
            StringBuilder successo = new StringBuilder();
            successo.Append("Annuncio prenotato con successo!");
            await App.Current.MainPage.DisplayAlert("RentIT", successo.ToString(), "ok");
            await NavService.NavigateToMainPage();

            IsBusy = false;
        }

        /*Comando per liberare l'annuncio prenotato*/
        Command _liberaAnnuncioCommand;
        public Command LiberaAnnuncioCommand
        {
            get
            {
                return _liberaAnnuncioCommand
                    ?? (_liberaAnnuncioCommand = new Command(async () => await ExecuteLiberaAnnuncioCommand()));
            }
        }

        async Task ExecuteLiberaAnnuncioCommand()
        {
            IsBusy = true;

            await _annuncioService.prenotaAd(Annuncio.id);
            StringBuilder successo = new StringBuilder();
            successo.Append("Annuncio liberato con successo!");
            await App.Current.MainPage.DisplayAlert("RentIT", successo.ToString(), "ok");
            await NavService.NavigateToMainPage();

            IsBusy = false;
        }
    }
}

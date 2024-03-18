using Microsoft.EntityFrameworkCore;
using OLIMP.Entities;
using OLIMP.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace OLIMP.ViewModels
{
    public class ManagerViewModel : ReactiveObject
    {
		private ObservableCollection<Client> _client;
        private string _firstNamel;
        private string _lastName;
        private string _middleName;
        private string _passportSeries;
        private string _passportNumber;
        private string _passportAddress;
        private string _searchClien;
        private User _user;
        private Client _selectedClient;
        private Guid _searchContract;
        private ObservableCollection<Contract> _contracts;
        private DateTime _actualReturnDate;
        private decimal _penalty;
        private decimal _returnDeposit;

        public ObservableCollection<Client> Clients { get => _client; set => this.RaiseAndSetIfChanged(ref _client, value); }
        public string FirstName { get => _firstNamel; set=> this.RaiseAndSetIfChanged(ref _firstNamel, value); }
        public string LastName { get => _lastName; set => this.RaiseAndSetIfChanged(ref _lastName, value); }
        public string MiddleName { get => _middleName; set => this.RaiseAndSetIfChanged(ref _middleName, value); }
        public string PassportSeries { get => _passportSeries; set => this.RaiseAndSetIfChanged(ref _passportSeries, value); }
        public string PassportNumber { get => _passportNumber; set => this.RaiseAndSetIfChanged(ref _passportNumber, value); }
        public string PassportAddress { get => _passportAddress; set => this.RaiseAndSetIfChanged(ref _passportAddress, value); }
        public string SearchClient { get => _searchClien; set => this.RaiseAndSetIfChanged(ref _searchClien, value); }
        public Client SelectedClient { get => _selectedClient; set => this.RaiseAndSetIfChanged(ref _selectedClient, value); }
        public User User { get => _user; set => this.RaiseAndSetIfChanged(ref _user, value); }
        public Guid SearchContract { get => _searchContract; set=> this.RaiseAndSetIfChanged(ref _searchContract, value); }
        public ObservableCollection<Contract> Contracts { get => _contracts; set => this.RaiseAndSetIfChanged(ref _contracts, value); }
        public DateTime ActualReturnDate { get => _actualReturnDate; set => this.RaiseAndSetIfChanged(ref _actualReturnDate, value); }
        public decimal Penalty { get => _penalty; set => this.RaiseAndSetIfChanged(ref _penalty, value); }
        public decimal ReturnDeposit { get => _returnDeposit; set => this.RaiseAndSetIfChanged(ref _returnDeposit, value); }

        public ManagerViewModel()
        {
            
        }
        public ManagerViewModel(User user)
        {
            List<Client> clients;
            User = user;
            using(ApplicationDbContext contex = new ApplicationDbContext())
            {
                clients = contex.Client.ToList();
            }
            Clients = new ObservableCollection<Client>(clients);
            Contracts = new ObservableCollection<Contract>();
        }

        public ReactiveCommand<string, Unit> Search => ReactiveCommand.Create<string>(searchText =>
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var searchResult = context.Client.Where(cl => cl.LastName.Contains(searchText)).ToList();
                

                Clients = new ObservableCollection<Client>(searchResult);
            }
        });

        public ReactiveCommand<Guid, Unit> SearchContractId => ReactiveCommand.Create<Guid>(searchText =>
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var searchResult = context.Contract
                .Where(cl => cl.Id == searchText)
                .Include(c => c.User)
                .Include(c => c.Client)
                .ToList();

               foreach(var item in searchResult)
               {
                    ActualReturnDate = item.PlannedDateReturn;
                   
               }

 
                Contracts = new ObservableCollection<Contract>(searchResult);
            }
        });

        public ReactiveCommand<Object, Unit> ReturtDeposit => ReactiveCommand.Create<Object>(e =>
        {
            foreach(var item in Contracts)
            {
                ReturnDeposit = item.Deposit - item.Cost - Penalty;
            }
        });

        public ReactiveCommand<object, Unit> GetAll => ReactiveCommand.Create<object>(e =>
        {
            List<Client> clients;

            using (ApplicationDbContext contex = new ApplicationDbContext())
            {
                clients = contex.Client.ToList();
            }
            Clients = new ObservableCollection<Client>(clients);
        });

        public ReactiveCommand<Object, Unit> CreateClient => ReactiveCommand.Create<object>(o =>
        {
            
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(MiddleName) || string.IsNullOrEmpty(PassportNumber) || string.IsNullOrEmpty(PassportSeries) || string.IsNullOrEmpty(PassportAddress) )
            {
                MessageBox.Show("Форма была заполнена не полностью!", "ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var client = new Client(FirstName, LastName, MiddleName, PassportAddress, PassportNumber, PassportSeries);
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Client.Add(client);
                context.SaveChanges();
            }
            Clients.Add(client);
        });

        public ReactiveCommand<Object, Unit> SelectedClientContract => ReactiveCommand.Create<object>(sc =>
        {
            var contractModel = new ContractWindow(SelectedClient, User);
            contractModel.Show();
        });
    }
}

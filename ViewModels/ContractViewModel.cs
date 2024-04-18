using Microsoft.EntityFrameworkCore;
using OLIMP.Entities;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;

namespace OLIMP.ViewModels
{
    public class ContractViewModel : ReactiveObject
    {
        private Client _selectedClientContract;
        private ObservableCollection<Inventory> _inventories;
        private ObservableCollection<Inventory> _shopInventories;
        private Inventory _selectedInventory;
        private DateTime _plannedDateReturn;
        private DateTime _issueDate;
        private decimal _cost;
        private decimal _deposit;
        private User _user;


        public Client SelectedClientContract { get => _selectedClientContract; set => this.RaiseAndSetIfChanged(ref _selectedClientContract, value); }
        public ObservableCollection<Inventory> Inventories { get => _inventories; set => this.RaiseAndSetIfChanged(ref _inventories, value); }
        public ObservableCollection<Inventory> ShopInventories { get => _shopInventories; set => this.RaiseAndSetIfChanged(ref _shopInventories, value); }
        public Inventory SelectedInventory { get => _selectedInventory; set => this.RaiseAndSetIfChanged(ref _selectedInventory, value); }
        public DateTime PlannedDateReturn { get => _plannedDateReturn; set => this.RaiseAndSetIfChanged(ref _plannedDateReturn, value); }
        public DateTime IssueDate { get => _issueDate; set => this.RaiseAndSetIfChanged(ref _issueDate, value); }
        public decimal Cost { get => _cost; set => this.RaiseAndSetIfChanged(ref _cost, value); }
        public decimal Deposit { get => _deposit; set => this.RaiseAndSetIfChanged(ref _deposit, value); }
        public User User { get => _user; set => this.RaiseAndSetIfChanged(ref _user, value); }

        public ContractViewModel()
        {

        }
        public ContractViewModel(Client selectedClient, User user)
        {
            User = user;

            ShopInventories = new ObservableCollection<Inventory>();

            SelectedClientContract = selectedClient;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                List<Inventory> inventories = context.Inventories
                    .Include(inv => inv.Category)
                    .Include(inv => inv.Level)
                    .ToList();

                Inventories = new ObservableCollection<Inventory>(inventories);

                FilterValues = context.Categories.Select(c => c.Name).ToList();
                FilterValues.Insert(0, "Нет");
            }
        }

        public ReactiveCommand<object, Unit> Payment => ReactiveCommand.Create<object>(e =>
        {
            decimal pricePerDay = 0;
            foreach (var item in ShopInventories)
            {
                pricePerDay += item.Category.PriceDay;
            }

            // Расчет срока аренды
            TimeSpan rentalPeriod = PlannedDateReturn - IssueDate;

            Cost = pricePerDay;
            if (rentalPeriod.Days > 1)
            {
                Cost = (rentalPeriod.Days - 1) * 0.8m * pricePerDay;
            }

            Deposit = 0;
            foreach (var item in ShopInventories)
            {
                Deposit += item.Category.SumDeposit;
            }


        });
        public ReactiveCommand<Object, Unit> CreateContract => ReactiveCommand.Create<object>(o =>
        {
            var contract = new Contract(DateTime.UtcNow, IssueDate, PlannedDateReturn, Deposit, Cost, PlannedDateReturn, 0, 0, SelectedClientContract.Id, User.Id);
            if (contract.Deposit != 0.0M || contract.Cost != 0.0M)
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Contract.Add(contract);
                    context.SaveChanges();
                }
                MessageBox.Show("Договор успешно создан!", "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            else
            {
                MessageBox.Show("НЕ выбран инвентарь!", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        });

        public ReactiveCommand<object, Unit> AddInventoryCommand => ReactiveCommand.Create<object>(e =>
        {
            foreach(var item in ShopInventories)
            {
                if (item.Id == SelectedInventory.Id)
                {
                    MessageBox.Show("Данный товар уже добавлен!", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            ShopInventories.Add(SelectedInventory);
        });

        private string _searchValue;

        public string SearchValue
        {
            get { return _searchValue; }
            set { this.RaiseAndSetIfChanged(ref _searchValue, value); UpdateListView(); }
        }


        public List<string> FilterValues { get; set; } = new List<string>();
        private string _filterValue;

        public string FilterValue
        {
            get { return _filterValue; }
            set { this.RaiseAndSetIfChanged(ref _filterValue, value); UpdateListView(); }
        }

        public List<string> SortValues => new List<string> { "Нет", "Стоим. возр.", "Стоим. уб." };

        private string _sortValue;

        public string SortValue
        {
            get { return _sortValue; }
            set { this.RaiseAndSetIfChanged(ref _sortValue, value); UpdateListView(); }
        }

        private void UpdateListView()
        {

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IQueryable<Inventory> query = context.Inventories
                    .Include(i => i.Category);

                if(!string.IsNullOrEmpty(FilterValue) && FilterValue != "Нет")
                {
                    query = query.Where(i => i.Category.Name == FilterValue);
                }

                if (!string.IsNullOrEmpty(SearchValue))
                {
                    query = query.Where(i => i.Name.Contains(SearchValue));
                }

                switch(SortValue)
                {
                    case "Стоим. возр.":
                        query = query.OrderBy(i => i.Cost);
                        break;
                    case "Стоим. уб.":
                        query = query.OrderByDescending(i => i.Cost);
                        break;
                    case "Нет":
                        break;
                    default: 
                        break;
                }
                Inventories = new ObservableCollection<Inventory>(query.ToList());
            }
        }
    }
}

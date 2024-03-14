using Microsoft.EntityFrameworkCore;
using OLIMP.Entities;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;

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
        public Client SelectedClientContract{get => _selectedClientContract; set => this.RaiseAndSetIfChanged(ref _selectedClientContract, value);}
        public ObservableCollection<Inventory> Inventories { get => _inventories; set => this.RaiseAndSetIfChanged(ref _inventories, value); }
        public ObservableCollection<Inventory> ShopInventories { get => _shopInventories; set => this.RaiseAndSetIfChanged(ref _shopInventories, value); }
        public Inventory SelectedInventory { get => _selectedInventory; set => this.RaiseAndSetIfChanged(ref _selectedInventory, value); }
        public DateTime PlannedDateReturn { get => _plannedDateReturn; set => this.RaiseAndSetIfChanged(ref _plannedDateReturn, value); }
        public DateTime IssueDate { get => _issueDate; set => this.RaiseAndSetIfChanged(ref _issueDate, value); }
        public decimal Cost { get => _cost; set => this.RaiseAndSetIfChanged(ref _cost, value); }
        //я остановился на том, что создававал поля для биндинга в view и расчет договора
        public ContractViewModel()
        {
           
        }
        public ContractViewModel(Client selectedClient)
        {
            ShopInventories = new ObservableCollection<Inventory>();
            SelectedClientContract = selectedClient;
            List<Inventory> inventories = new List<Inventory>();
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                inventories = context.Inventories
                    .Include(inv => inv.Category)
                    .Include(inv => inv.Level)
                    .ToList();

                Inventories = new ObservableCollection<Inventory>(inventories);
            }
        }

        public ReactiveCommand<object, Unit> ShopInv => ReactiveCommand.Create<object>(e =>
        {
           ShopInventories.Add(SelectedInventory);
        });
    }
}

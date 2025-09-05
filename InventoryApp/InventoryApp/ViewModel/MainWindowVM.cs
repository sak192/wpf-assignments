using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InventoryApp.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _productName {  get; set; }
        private int? _quantity {  get; set; }

        private bool _addInventoryVisibility { get; set; } = false;
        private string _statusMessage { get; set; }

        private Dictionary<string, int> productsInventory = new Dictionary<string, int>();
        public DelegateCommand ResetCommand { get; set; }
        public DelegateCommand AddInventoryCommand { get; set; }
        public MainWindowVM()
        {
            ResetCommand = new DelegateCommand(ClearFields);
            AddInventoryCommand = new DelegateCommand(AddInventory);
        }

        private void AddInventory()
        {
            if(!string.IsNullOrEmpty(ProductName) && Quantity!=null && Quantity>0)
            {
                if (productsInventory.ContainsKey(ProductName))
                {
                    int newQuantity = (int)(Quantity + productsInventory[ProductName]);
                    productsInventory[ProductName] = newQuantity;
                }
                else
                {
                    productsInventory.Add(ProductName, (int)Quantity);
                }
                StatusMessage = "Product added to inventory";
            }

        }

        private void ClearFields()
        {
            ProductName = "";
            Quantity=null;
            StatusMessage = "Fields Cleared";
        }

        private void updateAddInventoryVisibility()
        {
            if(!string.IsNullOrEmpty(ProductName) && Quantity.HasValue && Quantity > 0)
            {
                AddInventoryVisibility = true;
                return;
            }
            AddInventoryVisibility = false;
        }
        public bool AddInventoryVisibility
        {
            get
            {
                return _addInventoryVisibility;
            }
            set
            {
                if (_addInventoryVisibility != value)
                {
                    _addInventoryVisibility = value;
                    OnPropertyChanged(nameof(AddInventoryVisibility));
                }
            }
        }
        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                _productName = value;
                StatusMessage = ""; //after reset is clicked
                OnPropertyChanged(nameof(ProductName));
                updateAddInventoryVisibility();
            }
        }

        public int? Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                StatusMessage = "";//after reset is clicked
                OnPropertyChanged(nameof(Quantity));
                updateAddInventoryVisibility();
            }
        }

        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public DelegateCommand(Action action, Func<bool> canExecute=null)
        {
            this._execute = action;
            this._canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
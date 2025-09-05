using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace TempConverter.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private string _celsius;
        private string _farenheit;
        public event PropertyChangedEventHandler PropertyChanged;
        //flag for preventing infinite loop of property updation
        private bool isUpdating = false; //default it to false so for 1st time we can update
        public ICommand LogCommand { get; }
        public ObservableCollection<string> ConversionLogs { get; } = new ObservableCollection<string>();
        public MainWindowVM()
        {
            LogCommand = new DelegateCommand(LogConversion);
        }

        private void LogConversion()
        {
            if (double.TryParse(Celsius, out double c) && double.TryParse(Farenheit, out double f))
            {
                string logEntry = $"{Celsius}°C = {Farenheit}°F";

                if (!ConversionLogs.Contains(logEntry))
                {
                    ConversionLogs.Add(logEntry);
                }
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public string Celsius
        {
            get { return _celsius; }
            set
            {
                _celsius = value;
                OnPropertyChanged(nameof(Celsius));
                if (!isUpdating)
                {
                    isUpdating = true;
                    //extract the celsius value in double 
                    if (double.TryParse(_celsius, out double celsiusVal))
                    {
                        double fahrenheitVal = (celsiusVal * 9 / 5) + 32;
                        Farenheit = Math.Round(fahrenheitVal,2).ToString();
                    }
                    isUpdating = false;
                }
            }
        }
        public string Farenheit
        {
            get { return _farenheit; }
            set
            {
                _farenheit = value;
                OnPropertyChanged(nameof(Farenheit));
                //convert to Celcius and update
                if (!isUpdating)
                {
                    isUpdating = true; // to set the Celsius value
                   
                    if (double.TryParse(_farenheit, out double fahrenheitVal))
                    {
                        double celsiusVal = ((fahrenheitVal - 32) * 5) / 9;
                        Celsius = Math.Round(celsiusVal, 2).ToString();
                    }
                    isUpdating = false; // set back to false to enable updating be other property
                }
            }
        }
        
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action action, Func<bool> canExecute = null)
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

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}

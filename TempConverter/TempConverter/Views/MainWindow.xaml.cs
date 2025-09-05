using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TempConverter.ViewModel;

namespace TempConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext is required to know the object to which the UI binds to
            //so the UI(XAML) whenever it wants to check for any binding it will go to the MainWindowVM to update the UI
            //so when we make any binding in the XAML, e.g. "{Binding Result}" then the Result property will be searched on the DataContext object 
            //so now DataContext.Result will be reflected

            DataContext = new MainWindowVM();
        }
    }
}

using KatyaRyrs.Class;
using KatyaRyrs.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace KatyaRyrs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private IEnumerable<Product> _ProductList;
        public IEnumerable<Product> ProductList
        {
            get
            {
                var Result = _ProductList;
                if (Poisk != "")               
                    Result = Result.Where(p => p.Name.IndexOf(Poisk, StringComparison.OrdinalIgnoreCase)>=0);
                  
                
                return Result;
            }
            set
            {
                _ProductList = value;
                Invalidate();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Globals.dataProvider = new MySqlDataProvider();
            ProductList = Globals.dataProvider.GetProduct();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Invalidate(string ComponentName = "ProductList")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(ComponentName));
        }
        //поиск
        private string Poisk = "";
        private void SearchFilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            Poisk = PoiskTextBox.Text;
            Invalidate();
        }
    }
}

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
        private int SortType = 0;
        public List<ProductType> ProductTypeList { get; set; }

        private IEnumerable<Product> _ProductList;
        public IEnumerable<Product> ProductList
        {
            get
            {
                var Result = _ProductList;
               

                if (ProductTypeFilterId > 0)
                    Result = Result.Where(p => p.CurrentProductType.ID == ProductTypeFilterId);

                switch (SortType)
                {
                    // сортировка по названию продукции
                    case 1:
                        Result = Result.OrderByDescending(p => p.Name);
                        break;
                    case 2:
                        Result = Result.OrderBy(p => p.Name);
                        break;
                    case 3:
                        Result = Result.OrderByDescending(p => p.Number);
                        break;
                    case 4:
                        Result = Result.OrderBy(p => p.Number);
                        break;
                    case 5:
                        Result = Result.OrderByDescending(p => p.Price);
                        break;
                    case 6:
                        Result = Result.OrderBy(p => p.Price);
                        break;


                }
                if (Poisk != "")
                    Result = Result.Where(p => p.Name.IndexOf(Poisk, StringComparison.OrdinalIgnoreCase) >= 0);

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
            ProductTypeList = Globals.dataProvider.GetProductTypes().ToList();
            ProductTypeList.Insert(0, new ProductType { Title = "Все типы" });
            var NewEditWindow = new EditWindow(new Product());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Invalidate(string ComponentName = "ProductList")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(ComponentName));
        }
      
       
        private void SortTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortType = SortTypeComboBox.SelectedIndex;
            Invalidate();
        }
        public string[] SortList { get; set; } = {
            "Без сортировки",
            "название по убыванию",
            "название по возрастанию",
            "номер продукта по убыванию",
            "номер продукта по возрастанию",
            "цена по убыванию",
            "цена по возрастанию" };
        public int CurrentPage { get; private set; }
        public string SearchFilter { get; private set; }


        //поиск
        private string Poisk = "";
      

        private void PoiskTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            Poisk = PoiskTextBox.Text;
            Invalidate();
        }

      
        private int ProductTypeFilterId = 0;
        private void ProductTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductTypeFilterId = (ProductTypeFilter.SelectedItem as ProductType).ID;
            Invalidate();
        }

      //      private List<ProductType> ProductTypes = null;
   
        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var NewEditWindow = new EditWindow(ProductListView.SelectedItem as Product);
            if ((bool)NewEditWindow.ShowDialog())
            {
                // при успешном сохранении продукта перечитываем список продукции
                ProductList = Globals.dataProvider.GetProduct();
            }
        }
    }
}

using KatyaRyrs.Class;
using KatyaRyrs.Model;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace KatyaRyrs
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window, INotifyPropertyChanged
    {
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public Product CurrentProduct { get; set; }
        public string WindowName
        {
            get
            {
                return CurrentProduct.ID ==  0 ? "Новый продукт" : "Редактирование продукта";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Invalidate()
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("CatList"));
        }
        public EditWindow(Product EditProduct)
        {
            InitializeComponent();
            DataContext = this;
            CurrentProduct = EditProduct;
            ProductTypes = Globals.dataProvider.GetProductTypes();
        }

      

        private void ChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog GetImageDialog = new OpenFileDialog();
            // задаем фильтр для выбираемых файлов
            // до символа "|" идет произвольный текст, а после него шаблоны файлов разделенные точкой с запятой
            GetImageDialog.Filter = "Файлы изображений: (*.png, *.jpg)|*.png;*.jpg";
            // чтобы не искать по всему диску задаем начальный каталог
            GetImageDialog.InitialDirectory = Environment.CurrentDirectory;
            if (GetImageDialog.ShowDialog() == true)
            {
                // перед присвоением пути к картинке обрезаем начало строки, т.к. диалог возвращает полный путь
                CurrentProduct.Image = GetImageDialog.FileName.Substring(Environment.CurrentDirectory.Length);
                // обратите внимание, это другое окно и другой Invalidate, который реализуйте сами
                Invalidate();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            

                Globals.dataProvider.SaveProduct(CurrentProduct);
                DialogResult = true;
                Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

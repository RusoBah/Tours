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

namespace ToursApp
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Hotel _currentHotel = new Hotel();

        // В этот параметр мы будем добовлять экзеплятор выбранного проекта
        public AddEditPage(Hotel selectedHotel)
        {
            InitializeComponent();

            // Если он не пустой
            if (selectedHotel != null)
                _currentHotel = selectedHotel;

            DataContext = _currentHotel;
            ComboCountries.ItemsSource = toursEntities.GetContext().Countries.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Проверка
            // Переменная для хранения ошибок
            StringBuilder errors = new StringBuilder();

            // Если название текущего отеля будт пустым
            if (string.IsNullOrWhiteSpace(_currentHotel.Name))
                // То выведиться строка
                errors.AppendLine("Укажите название отеля");
            if (_currentHotel.CountOfStars < 1 || _currentHotel.CountOfStars > 5)
                errors.AppendLine("Кол-во звёзд - число от 1 до 5");
            if (_currentHotel.Country == null)
                errors.AppendLine("Выберети страну");

            // Чтобы узнать возникли ли ошибки нужно:
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            // Если всё хорошо и происходит операция добавления, то будет происходить создание собственного id
            if (_currentHotel.id == 0)
                toursEntities.GetContext().Hotels.Add(_currentHotel);

            try
            {
                // сохранение изменений
                toursEntities.GetContext().SaveChanges();
                MessageBox.Show("Сохранено!");
                // Возращение на прошлую страницу
                Maneger.MainFrame.GoBack();
            }
               
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
              
                  
             


               
           
        }

        private void ComboCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

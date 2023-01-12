
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            //Отображает первую страницу
            MainFrame.Navigate(new ToursPage());
            // Присваивание значение к странице Manager
            Maneger.MainFrame = MainFrame;

            // Пример импорта
            ImportTours();
        }


        // Пример импорта
        private void ImportTours()
        {
            // НАДО СОЗДАТЬ ФАЙЛ С ТУРАМИ
            // В скобках указывается путь для файла
            var fileData = File.ReadAllLines(@"C:\Users\1ы\Documents\Tours\tours.txt");
            // для изображения 
            var Images = Directory.GetFiles(@"C:\Users\1ы\Documents\Tours\img");

            foreach (var line in fileData)
            {
                var data = line.Split('\t');

                Tour tempTour;
                if ((data[4] == "0"))
                {
                    tempTour = new Tour
                {
                    Name = data[0].Replace("\'", ""),
                    TicketCount = int.Parse(data[2]),
                    Price = decimal.Parse(data[3]),
                    isActual = false
                };
                }
                else
                {
                    tempTour = new Tour
                {
                    Name = data[0].Replace("\'", ""),
                    TicketCount = int.Parse(data[2]),
                    Price = decimal.Parse(data[3]),
                    isActual = true
                };
                }

                foreach (var tourType in data[5].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var currentType = toursEntities.GetContext().Types.ToList().FirstOrDefault(p => p.Name == tourType);
                    if (currentType != null)
                        tempTour.Types.Add(currentType);
                }
                try
                {
                    tempTour.ImagePreview = File.ReadAllBytes(Images.FirstOrDefault(p => p.Contains(tempTour.Name)));
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

                toursEntities.GetContext().Tours.Add(tempTour);
                toursEntities.GetContext().SaveChanges();
            }

        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Maneger.MainFrame.GoBack();
        }
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}

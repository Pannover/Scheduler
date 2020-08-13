using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scheduler
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\ScDataList.json";
        private BindingList<SchedulerModel> _schedulerDataList;
        private FileIOService _fileIOService;
        public DateTime curDate { get; set; } = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();

            var cultureInfo = new CultureInfo("ru-RU");
            //Thread.CurrentThread.CurrentCulture = cultureInfo;
            //Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata
                (XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) // Обработчик загруски окна
        {
            _fileIOService = new FileIOService(PATH);

            try
            {
                _schedulerDataList = _fileIOService.LoadData(); // Чтение данных из файла и запись в коллекцию BindingList объектов класса SchedulerModel
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            SchedulerList.ItemsSource = _schedulerDataList;     // Добавление данных в DataGrid из коллекции BindingList объектов класса SchedulerModel
            
            _schedulerDataList.ListChanged += _schedulerDataList_ListChanged; // Подпись на событие ListChanged
        }

        private void _schedulerDataList_ListChanged(Object sender, ListChangedEventArgs e) // Обработчик события ListChanged
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted ||
                e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }           
        }              
    }
}
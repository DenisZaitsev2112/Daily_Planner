using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

namespace DailyPlanner
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> entries;
        public DateTime SelectedDate { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            entries = new ObservableCollection<string>();
            lbEntries.ItemsSource = entries;
            SelectedDate = DateTime.Today;
            DataContext = this;

            // Загружаем сохраненные записи при запуске программы
            LoadEntries();

            // Заполняем ComboBox для выбора часов и минут
            for (int i = 0; i < 24; i++)
            {
                cbHours.Items.Add(i.ToString("D2")); // Добавляем часы в формате 00, 01, ..., 23
            }

            for (int i = 0; i < 60; i++)
            {
                cbMinutes.Items.Add(i.ToString("D2")); // Добавляем минуты в формате 00, 01, ..., 59
            }
        }

        private void BtnAddEntry_Click(object sender, RoutedEventArgs e)
        {
            string newEntry = $"{txtTitle.Text} - {txtDescription.Text}";
            if (!string.IsNullOrWhiteSpace(newEntry) && cbHours.SelectedItem != null && cbMinutes.SelectedItem != null)
            {
                string time = $"{cbHours.SelectedItem}:{cbMinutes.SelectedItem}";

                entries.Add($"{SelectedDate.ToShortDateString()} {time} - {newEntry}");
                txtTitle.Clear();
                txtDescription.Clear();
                cbHours.SelectedItem = null;
                cbMinutes.SelectedItem = null;

                // Сохраняем записи при добавлении новой записи
                SaveEntries();
            }
        }

        private void LoadEntries()
        {
            if (File.Exists("C:\\ПЛЕШКА и все что с ней связано\\DailyPlanner.txt"))
            {
                string[] lines = File.ReadAllLines("C:\\ПЛЕШКА и все что с ней связано\\DailyPlanner.txt", Encoding.UTF8);
                entries = new ObservableCollection<string>(lines);
                lbEntries.ItemsSource = entries;
            }
        }

        private void SaveEntries()
        {
            File.WriteAllLines("C:\\ПЛЕШКА и все что с ней связано\\DailyPlanner.txt", entries, Encoding.UTF8);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Сохраняем записи при закрытии окна
            SaveEntries();
        }
    }
}

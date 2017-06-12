using System;
using System.Linq;
using System.Windows;
using System.IO;
using System.Xml.Linq;
using System.Windows.Controls;

namespace ZwiftWorldSelectorPC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, "Zwift\\prefs.xml");
            if (!File.Exists(filePath))
            {
                FileMissing.Visibility = Visibility.Visible;
                return;
            }
            FileMissing.Visibility = Visibility.Hidden;
            var xmlDoc = XDocument.Load(filePath);
            var root = xmlDoc.Descendants().First();
            if (!root.Descendants("WORLD").Any())
            {
                XElement worldNode = new XElement("WORLD", "1");
                root.Add(worldNode);
            }
            var selectedWorld = ((ComboBoxItem)SelectedWorld.SelectedItem).Tag.ToString();
            root.Descendants("WORLD").Single().Value = selectedWorld;
            if (selectedWorld == "0")
            {
                root.Descendants("WORLD").Remove();
            }

            xmlDoc.Save(filePath);
            Application.Current.Shutdown();
        }
    }
}

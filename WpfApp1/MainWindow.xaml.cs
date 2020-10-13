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
using System.Windows.Media.Animation;
using WpfApp1.View;
using System.Runtime.CompilerServices;

namespace WpfApp1{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        public MainWindow() {
            InitializeComponent();

            //Настройка размеров всего приложения
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;

            //ояистка шлавного грида
            //mainContentGrid.Children.RemoveRange(0, mainContentGrid.Children.Count);
        }

        //=======================mainLayout=======================

        //-----LoadedEvent-----
        private void windowApp_Loaded(object sender, RoutedEventArgs e) {
            contentControl.Content = new mainPage(this, currentRecord);
        }
        private void navBarButton3_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = new aboutProgramm(contentControl);
        }
        private void navBarButton2_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = new spravkaPage(contentControl);
        }
        //-----LoadedEvent-----

        //-----ClickEvent-----
        private void mainLayoutExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
        //-----ClickEvent-----

        //=======================mainLayout=======================
    }
}

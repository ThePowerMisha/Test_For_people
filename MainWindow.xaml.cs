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

            mPage = new mainPage(this, currentRecord, contentControl);
            aProgramm = new aboutProgramm(headerControl);
            sPage = new spravkaPage(headerControl);
            tPage = new TheoryPage(headerControl);
            //ояистка шлавного грида
            //mainContentGrid.Children.RemoveRange(0, mainContentGrid.Children.Count);
        }

        //=======================mainLayout=======================

        //-----LoadedEvent-----
        //основная страница и страницы шапки
        private static mainPage mPage;
        private static aboutProgramm aProgramm;
        private static spravkaPage sPage;
        private static TheoryPage tPage;

        //возврат страниц
        public static mainPage getMainPage() {
            return mPage;
        }

        //обработчики событий переводов
        private void windowApp_Loaded(object sender, RoutedEventArgs e) {
            contentControl.Content = mPage;
        }
        private void navBarButton3_Click(object sender, RoutedEventArgs e) {
            swipeHeader(aProgramm);
        }
        private void navBarButton2_Click(object sender, RoutedEventArgs e) {
            swipeHeader(sPage);
        }
        private void navBarButton1_Click(object sender, RoutedEventArgs e) {
            swipeHeader(tPage);
        }

        //смена окна шапки
        private void swipeHeader(UserControl page) {
            headerControl.Content = page;
            headerControl.IsHitTestVisible = true;
            if (!headerControl.IsEnabled) {
                headerControl.IsEnabled = true;
                headerControl.Opacity = 1;
            }
            if (mPage.getPopupStatus())
                mPage.setPopupStatus();
        }
        //-----LoadedEvent-----

        //=======================mainLayout=======================
    }
}

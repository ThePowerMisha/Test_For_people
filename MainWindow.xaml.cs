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

            hControl = headerControl;
            nVB1 = navBarButton1;
            nVB2 = navBarButton2;
            nVB3 = navBarButton3;
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

        private static ContentControl hControl;
        private static Button nVB1;
        private static Button nVB2;
        private static Button nVB3;
        public static void headersBlock() {
            hControl.IsEnabled = false;
            hControl.Opacity = 0;
            hControl.IsHitTestVisible = false;
            nVB1.IsEnabled = false;
            nVB2.IsEnabled = false;
            nVB3.IsEnabled = false;
            nVB1.Opacity = 0.3;
            nVB2.Opacity = 0.3;
            nVB3.Opacity = 0.3;
        }
        public static void headersUnlock() {
            nVB1.IsEnabled = true;
            nVB2.IsEnabled = true;
            nVB3.IsEnabled = true;
            nVB1.Opacity = 1;
            nVB2.Opacity = 1;
            nVB3.Opacity = 1;
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
            if (WpfApp1.View.choiceBlock.getChoiceNextPage()!=null && WpfApp1.View.choiceBlock.getChoiceNextPage().getPopupStatus())
                WpfApp1.View.choiceBlock.getChoiceNextPage().setPopupStatus();
            if (WpfApp1.View.choiceNextPage.getTestPage() != null && WpfApp1.View.choiceNextPage.getTestPage().getPopupStatus())
                WpfApp1.View.choiceNextPage.getTestPage().setPopupStatus();
        }
        //-----LoadedEvent-----

        //=======================mainLayout=======================
    }
}

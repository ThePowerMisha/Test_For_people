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

namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для resultsPage.xaml
    /// </summary>
    public partial class resultsPage : UserControl {
        public resultsPage(Window win, Label currec, ContentControl cC) {
            InitializeComponent();

            mainWin = win;
            curRec = currec;
            contentControl = cC;
        }

        private static Window mainWin;
        private static Label curRec;
        private static ContentControl contentControl;
        //возвращает на главную страницу
        private void onMainPage_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = new mainPage(mainWin, curRec, contentControl);
        }
    }
}

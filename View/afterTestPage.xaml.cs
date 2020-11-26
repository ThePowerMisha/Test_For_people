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
    /// Логика взаимодействия для afterTestPage.xaml
    /// </summary>
    public partial class afterTestPage : UserControl {
        public afterTestPage(ContentControl cC) {
            InitializeComponent();

            contentControl = cC;
        }
        private static ContentControl contentControl;
        private void onPreviousPage_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = WpfApp1.MainWindow.getMainPage();
        }
    }
}

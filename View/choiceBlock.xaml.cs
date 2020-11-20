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
    /// Логика взаимодействия для choiceBlock.xaml
    /// </summary>
    public partial class choiceBlock : UserControl {
        public choiceBlock(ContentControl cC) {
            InitializeComponent();

            contentControl = cC;

            loadDataThemesCB.ItemsSource = themes;


        }
        private static ContentControl contentControl;
        private static List<string> themes = new List<string> {
            "Фермы"
        };

        //возвращает на главную страницу
        private void onMainPage_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = WpfApp1.MainWindow.getMainPage();
        }
    }
    
}

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
        public afterTestPage(ContentControl cC, string timeWaste, string score) {
            InitializeComponent();

            contentControl = cC;

            result1.Content = choiceBlock.dataList[0];
            result2.Content = choiceBlock.dataList[1];
            result3.Content = choiceNextPage.dataList[0];
            result4.Content = choiceNextPage.dataList[1];
            result5.Content = timeWaste;
            result6.Content = choiceNextPage.dataList[2];
            result7.Content = score;
            result8.Content = testPage.correctAnswersCount.ToString();
            result9.Content = DateTime.Today.ToString("D");

            resultsMass.Clear();
            resultsMass.Add("theme1", choiceBlock.dataList[0]);
            resultsMass.Add("theme2", choiceBlock.dataList[1]);
            resultsMass.Add("theme3", choiceNextPage.dataList[0]);
            resultsMass.Add("theme4", choiceNextPage.dataList[1]);
            resultsMass.Add("theme5", timeWaste);
            resultsMass.Add("theme6", choiceNextPage.dataList[2]);
            resultsMass.Add("theme7", score);
            resultsMass.Add("theme8", testPage.correctAnswersCount.ToString());
            resultsMass.Add("theme9", DateTime.Today.ToString("D"));
        }
        private static ContentControl contentControl;

        /// <summary>
        /// Возвращает словарь результатов теста
        /// </summary>
        /// <returns>словарь результатов теста</returns>
        public static Dictionary<string, string> getResultsMass() => resultsMass;

        //массив данных для записи в бд
        private static Dictionary<string, string> resultsMass = new Dictionary<string, string>() {};

        //переход на главную страницу
        private void onPreviousPage_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = WpfApp1.MainWindow.getMainPage();
        }
    }
}

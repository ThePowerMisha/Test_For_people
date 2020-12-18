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

            string gradate="";
            if (Int32.Parse(score) <= 23)
                gradate = "Неудовлетворительно";
            else if(Int32.Parse(score) > 23 && Int32.Parse(score)<=31)
                gradate = "Удовлетворительно";
            else if (Int32.Parse(score) > 31 && Int32.Parse(score) <= 37)
                gradate = "Хорошо";
            else
                gradate = "Отлично";

            result1.Content = choiceBlock.dataList[0];
            result2.Content = choiceBlock.dataList[1];
            result3.Content = choiceNextPage.dataList[0];
            result4.Content = choiceNextPage.dataList[1];
            result5.Content = timeWaste;
            result6.Content = choiceNextPage.dataList[2];
            result7.Content = score +" / "+gradate;
            result8.Content = testPage.correctAnswersCount.ToString();
            result9.Content = DateTime.Today.ToString("D");

            if (gradate == "Неудовлетворительно")
                gradate = "Неуд.";
            else if (gradate == "Удовлетворительно")
                gradate = "Удов.";

            mainPage.data.theme.Add(choiceBlock.dataList[0]);
            mainPage.data.block.Add(choiceBlock.dataList[1]);
            mainPage.data.load.Add(choiceNextPage.dataList[0]);
            mainPage.data.variant.Add(choiceNextPage.dataList[1]);
            mainPage.data.timeSpent.Add(timeWaste);
            mainPage.data.scoreResult.Add(score + " / " + gradate);
            mainPage.data.correctAnswers.Add(testPage.correctAnswersCount.ToString());
            mainPage.data.testDate.Add(DateTime.Today.ToString("D"));
            mainPage.data.tips.Add(choiceNextPage.dataList[2]);
            dBController.results.saveData(mainPage.data);

        }
        private static ContentControl contentControl;

        //переход на главную страницу
        private void onPreviousPage_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = WpfApp1.MainWindow.getMainPage();
        }
    }
}

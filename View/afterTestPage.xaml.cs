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
using dBController;

namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для afterTestPage.xaml
    /// </summary>

    public partial class afterTestPage : UserControl {
        public static results data;
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
            result8.Content = testPage.correctAnswersCount.ToString()+ " / "+ questions.getQuestionCount(choiceBlock.dataList[0], choiceBlock.blockID, choiceNextPage.loadID, choiceNextPage.variantID).ToString();
            result9.Content = DateTime.Today.ToString("D");

            if (gradate == "Неудовлетворительно")
                gradate = "Неуд.";
            else if (gradate == "Удовлетворительно")
                gradate = "Удов.";

             
            data = results.createEntery();
            data.lastName.Add(mainPage.lastName_data);
            data.firstName.Add(mainPage.firstName_data);
            data.secondName.Add(mainPage.secondName_data);
            data.group.Add(mainPage.group_data);
            data.ID.Add(results.idGeneration(data));
            data.theme.Add(choiceBlock.dataList[0]);
            data.block.Add(choiceBlock.dataList[1]);
            data.load.Add(choiceNextPage.dataList[0]);
            data.variant.Add(choiceNextPage.dataList[1]);
            data.timeSpent.Add(timeWaste);
            data.scoreResult.Add(score + " / " + gradate);
            data.tips.Add(choiceNextPage.dataList[2]);
            data.correctAnswers.Add(testPage.correctAnswersCount.ToString() + " / " + questions.getQuestionCount(choiceBlock.dataList[0], choiceBlock.blockID, choiceNextPage.loadID, choiceNextPage.variantID).ToString());
            data.testDate.Add(DateTime.Today.ToString("D"));
            results.saveData(data);


            string pr1, pr2, pr3, pr4;
            pr1 = pr2 = pr3 = pr4 = "НЕТ ДАННЫХ";
            List<string> prevDataMass = results.getLastResults(System.Convert.ToInt32(data.ID[data.ID.Count() - 1]), choiceBlock.dataList[0], choiceBlock.dataList[1], choiceNextPage.dataList[0]);
            Console.WriteLine(prevDataMass);
            if (prevDataMass.Count!=0) {
                pr1 = prevDataMass[0];
                pr2 = prevDataMass[1];
                pr3 = prevDataMass[2];
                pr4 = prevDataMass[3];
            }

            previusResult1.Content = pr1;
            previusResult2.Content = pr2;
            previusResult3.Content = pr3;
            previusResult4.Content = pr4;

        }
        private static ContentControl contentControl;

        //переход на главную страницу
        private void onPreviousPage_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = WpfApp1.MainWindow.getMainPage();
        }
    }
}

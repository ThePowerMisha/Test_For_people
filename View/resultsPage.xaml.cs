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
using System.Data;


namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для resultsPage.xaml
    /// </summary>
    public partial class resultsPage : UserControl {
        public resultsPage(Window win, Label currec, ContentControl cC) {
            InitializeComponent();

            //массив значений для loadDataSubTypeCB
            loadDataSubTypeCB.ItemsSource = loadDataSubType;
            Placeholder.add(searchData, "Введите ключевое слово");

            //массив результатов людей для resultsPeople
            loadResultsPeople.Add(new Person("Девственно", "Чистый", "И", "Не", new List<PersonData> { new PersonData("hello World! what the fuck is going on", "1", "1", "1", "1", "1", "1", "1", "31 января 2020 г."), new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }));
            loadResultsPeople.Add(new Person("Тронутый", "Рот", "Этого", "Казино", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }));
            resultsPeople.ItemsSource = loadResultsPeople;

            mainWin = win;
            curRec = currec;
            contentControl = cC;
        }
        //массив для поиска (это тестовые значения, потом этот массив будет заполняться из бд)
        private static List<string> loadDataSubType = new List<string>{
            "Something1","Something2","Something3","Something4","Something5"
        };

        //массив для таблицы данных (это тестовые значения, потом этот массив будет заполняться из бд)
        private static List<Person> loadResultsPeople = new List<Person> {
            new Person("lastName", "firstName", "secondName", "group", new List<PersonData> { new PersonData("hello World! what the fuck is going on? oh my god! what are you two doing?","1", "1", "1", "1", "1", "1", "1", "31 января 2020 г.") }),
            new Person("lastName", "firstName", "secondName", "group", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "31 января 2020 г.") }),
            new Person("lastName", "firstName", "secondName", "group", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }),
            new Person("lastName", "firstName", "secondName", "group", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }),
            new Person("lastName", "firstName", "secondName", "group", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }),
            new Person("lastName", "firstName", "secondName", "group", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }),
            new Person("lastName", "firstName", "secondName", "group", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }),
            new Person("lastName", "firstName", "secondName", "group", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") })
        };

        private static Window mainWin;
        private static Label curRec;
        private static ContentControl contentControl;
        //возвращает на главную страницу
        private void onMainPage_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = WpfApp1.MainWindow.getMainPage();//new mainPage(mainWin, curRec, contentControl);
        }

        //закрытие подробной информации по повторному клику
        private static int prevSelectedIndex = -1;
        private void Row_Click(object sender, MouseButtonEventArgs e) {
            if (resultsPeople.SelectedIndex == prevSelectedIndex && e.OriginalSource.ToString() != "System.Windows.Controls.Primitives.DataGridDetailsPresenter") {
                resultsPeople.UnselectAllCells();
                prevSelectedIndex = -1;
            } else {
                prevSelectedIndex = resultsPeople.SelectedIndex;
            }

            //CheckAnswer checkF = new CheckAnswer();
            //checkF.Formula = "sqrt(25)+(F1)";
            //searchData.Text = checkF.Check();

            //if ((e.OriginalSource as DataGridCell).IsSelected)
            //    resultsPeople.UnselectAllCells(); 

            //if (resultsPeople.SelectedItem as Person != e.OriginalSource as Person) 
            //    resultsPeople.UnselectAllCells(); 

            //if (resultsPeople.CurrentCell.Column.DisplayIndex != 7) {
            //    searchData.Text = resultsPeople.CurrentCell.Column.DisplayIndex.ToString() + " " + e.OriginalSource.ToString();
            //    //searchData.Text = resultsPeople.SelectedIndex.ToString(); 
            //}

            //if (resultsPeople.SelectedItem == e.OriginalSource as Person)
            //    resultsPeople.UnselectAllCells();

        }
    }
}

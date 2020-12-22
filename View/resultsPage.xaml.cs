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
using dBController;


namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для resultsPage.xaml
    /// </summary>
    public partial class resultsPage : UserControl {
        public resultsPage(Window win, Label currec, ContentControl cC) {
            InitializeComponent();

            //массив значений для loadDataSubTypeCB
            //loadDataSubTypeCB.ItemsSource = loadDataSubType;
            Placeholder.add(searchData, "Введите ключевое слово");

            //массив результатов людей для resultsPeople
            //loadResultsPeople.Add(new Person("Девственно", "Чистый", "И", "Не", new List<PersonData> { new PersonData("hello World! what the fuck is going on", "1", "1", "1", "1", "1", "1", "1", "31 января 2020 г."), new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }));
            //loadResultsPeople.Add(new Person("Тронутый", "Рот", "Этого", "Казино", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }));
            resultsPeople.ItemsSource = loadResultsPeople;

            //обработчики событий
            loadDataTypeCB.SelectionChanged += loadDataTypeCB_SelectionChanged;
            searchDataTypeCB.SelectionChanged += searchDataTypeCB_SelectionChanged;
            searchData.TextChanged += searchData_TextChanged;

            //стартовая сортировка по группе
            loadDataTypeCB.SelectedIndex = 1;
            loadDataTypeCB.SelectedIndex = 0;

            mainWin = win;
            curRec = currec;
            contentControl = cC;
        }
        //массив для поиска (это тестовые значения, потом этот массив будет заполняться из бд)
        //private static List<string> loadDataSubType = new List<string>{
        //    "Something1","Something2","Something3","Something4","Something5"
        //};

        //массив для таблицы данных (это тестовые значения, потом этот массив будет заполняться из бд)
        private static List<Person> loadResultsPeople = new List<Person> ()/*{
            new Person("Карпенко", "Александр", "Павлович", "ИСТ 1-19", new List<PersonData> { new PersonData("hello World! what the fuck is going on? oh my god! what are you two doing?","1", "1", "1", "1", "1", "1", "1", "31 января 2020 г.") }),
            new Person("Путин", "Владимир", "Владимирович", "ИВТ 1-19", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "31 января 2020 г.") }),
            new Person("Капитан", "Джек", "Воробей", "ИСТ 1-19", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }),
            new Person("Дональд", "Трамп", "Младший", "ИВТ 1-19", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }),
            new Person("Синицын", "Иван", "Сергеевич", "ИСТ 1-19", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }),
            new Person("Рекунов", "Сергей", "Сергеевич", "ХЗ 1-10", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }),
            new Person("Доберман", "Серый", "Павлинов", "ХЗ 1-10", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") }),
            new Person("Крико", "Пупа", "Лупа", "ИВТ 1-19", new List<PersonData> { new PersonData("1", "1", "1", "1", "1", "1", "1", "1", "1") })
        }*/;

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


        //массив который будет хранить временные данные
        private static List<Person> newMass = new List<Person>();
        /// <summary>
        ///  ВАНЯ ПИШИ ТУТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadDataTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //newMass.Clear();

            if (newSearchMass.Count != 0) {
                newMass = newSearchMass;
            } else {
                newMass = loadResultsPeople;
            }

            //loadDataTypeCB.Text это текущее содержимое выпадающего списка
            if (loadDataTypeCB.SelectedIndex == 0) {
                for (int this_person = 0; this_person < newMass.Count; this_person++) {
                    for (int other_person = 0; other_person < newMass.Count; other_person++) {
                        if (String.Compare(newMass[this_person].group, newMass[other_person].group) < 0) {
                            (newMass[this_person], newMass[other_person]) = (newMass[other_person], newMass[this_person]);
                        }
                    }
                }
            } else if (loadDataTypeCB.SelectedIndex == 1) {
                for (int this_person = 0; this_person < newMass.Count; this_person++) {
                    for (int other_person = 0; other_person < newMass.Count; other_person++) {
                        if (String.Compare(newMass[this_person].lastName, newMass[other_person].lastName) < 0) {
                            (newMass[this_person], newMass[other_person]) = (newMass[other_person], newMass[this_person]);
                        }
                    }
                }
            } else if (loadDataTypeCB.SelectedIndex == 2) {
                for (int this_person = 0; this_person < newMass.Count; this_person++) {
                    for (int other_person = 0; other_person < newMass.Count; other_person++) {
                        if (String.Compare(newMass[this_person].firstName, newMass[other_person].firstName) < 0) {
                            (newMass[this_person], newMass[other_person]) = (newMass[other_person], newMass[this_person]);
                        }
                    }
                }
            } else if (loadDataTypeCB.SelectedIndex == 3) {
                for (int this_person = 0; this_person < newMass.Count; this_person++) {
                    for (int other_person = 0; other_person < newMass.Count; other_person++) {
                        if (String.Compare(newMass[this_person].secondName, newMass[other_person].secondName) < 0) {
                            (newMass[this_person], newMass[other_person]) = (newMass[other_person], newMass[this_person]);
                        }
                    }
                }
            }


            //загрузка в таблицу данных из того массива
            resultsPeople.ItemsSource = null;
            resultsPeople.ItemsSource = newMass;
        }

        //поиск людей 
        private void searchDataTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            searchData.Clear();
            searchData.Text = "Введите ключевое слово";
            searchData.Foreground = SpecialColor.placeholder();
            resultsPeople.ItemsSource = null;
            resultsPeople.ItemsSource = loadResultsPeople;
        }
        private static List<Person> newSearchMass = new List<Person>();
        private void searchData_TextChanged(object sender, TextChangedEventArgs e) {
            newSearchMass.Clear();
            if (searchData.Text != "" && searchData.Text != "Введите ключевое слово" ) {
                if (searchDataTypeCB.Text == "всем полям") {
                    foreach (Person person in loadResultsPeople) {
                        if (person.lastName.ToLower().Contains(searchData.Text.ToLower()) || person.firstName.ToLower().Contains(searchData.Text.ToLower()) || person.secondName.ToLower().Contains(searchData.Text.ToLower()) || person.group.ToLower().Contains(searchData.Text.ToLower()))
                            newSearchMass.Add(person);
                    }
                } else if (searchDataTypeCB.Text == "фамилии") {
                    foreach (Person person in loadResultsPeople) {
                        if (person.lastName.ToLower().Contains(searchData.Text.ToLower()))
                            newSearchMass.Add(person);
                    }
                } else if (searchDataTypeCB.Text == "имени") {
                    foreach (Person person in loadResultsPeople) {
                        if (person.firstName.ToLower().Contains(searchData.Text.ToLower()))
                            newSearchMass.Add(person);
                    }
                } else if (searchDataTypeCB.Text == "отчеству") {
                    foreach (Person person in loadResultsPeople) {
                        if (person.secondName.ToLower().Contains(searchData.Text.ToLower()))
                            newSearchMass.Add(person);
                    }
                } else {
                    foreach (Person person in loadResultsPeople) {
                        if (person.group.ToLower().Contains(searchData.Text.ToLower()))
                            newSearchMass.Add(person);
                    }
                }
                resultsPeople.ItemsSource = null;
                resultsPeople.ItemsSource = newSearchMass;
            } else {
                if (searchData.Text != "Введите ключевое слово" || searchData.Foreground == SpecialColor.placeholder()) { 
                    resultsPeople.ItemsSource = null;
                    resultsPeople.ItemsSource = loadResultsPeople;
                } 
            }
            //if (resultsPeople.Items.Count!=0)
            //    loadDataTypeCB_SelectionChanged(null, null);
        }

        //загрузка и конвертация данных из 
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {

            Dictionary<Tuple<string, string, string, string>, List<List<string>>> dict = dBController.results.getAllResults();
            List<Tuple<string, string, string, string>> massKeys = new List<Tuple<string, string, string, string>>(dict.Keys);
            loadResultsPeople.Clear();
            foreach (Tuple<string, string, string, string> man in massKeys) {
                Person person = new Person();
                person.lastName = man.Item1;
                person.firstName = man.Item2;
                person.secondName = man.Item3;
                person.group = man.Item4;

                List<PersonData> personDataMass = new List<PersonData>();
                foreach (List<string> manData in dict[man]) {
                    PersonData personData = new PersonData();
                    personData.theme = manData[0];
                    personData.part = manData[1];
                    personData.loads = manData[2];
                    personData.variant = manData[3];
                    personData.time = manData[4];
                    personData.tips = manData[5];
                    personData.score = manData[6];
                    personData.answers = manData[7];
                    personData.date = manData[8];

                    personDataMass.Add(personData);
                }
                person.data = personDataMass;

                loadResultsPeople.Add(person);
            }

            resultsPeople.ItemsSource = null;
            resultsPeople.ItemsSource = loadResultsPeople;
        }
    }
}

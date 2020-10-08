using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WpfApp1;

namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для mainWindow.xaml
    /// </summary>
    public partial class mainPage : UserControl {
        public mainPage(Window win, Label currec) {
            InitializeComponent();

            mainWin = win;
            curRec = currec;

            //=============mainStyle================
            //placeholders
            Placeholder.add(lastName, "Фамилия");
            Placeholder.add(firstName, "Имя");
            Placeholder.add(secondName, "Отчество");
            Placeholder.add(group, "Группа");
            //=============mainStyle================
        }
        //КОНСТРУКТОР ПО УМОЛЧАНИЮ
        public mainPage() {
            InitializeComponent();

            //=============mainStyle================
            //placeholders
            Placeholder.add(lastName, "Фамилия");
            Placeholder.add(firstName, "Имя");
            Placeholder.add(secondName, "Отчество");
            Placeholder.add(group, "Группа");
            //=============mainStyle================
        }

        private static Window mainWin;
        private static Label curRec;
        //=======================mainLayout=======================

        //ВЫХОД
        private void mainLayoutExit_Click(object sender, RoutedEventArgs e) {
            mainWin.Close();
        }

        //ПРОВЕРКА ВВЕДЕННОСТИ ДАННЫХ
        private void saveData_Click(object sender, RoutedEventArgs e) {
            if (lastName.Text!="" && lastName.Text!="Фамилия" &&
                firstName.Text != "" && firstName.Text != "Имя" &&
                secondName.Text != "" && secondName.Text != "Отчество" &&
                group.Text != "" && group.Text != "Группа") {
                Regex regFIO = new Regex("^[А-Я][а-я]+$");
                Regex regGroup = new Regex("^[А-Я]{2,}(\\-| )?\\d{1,2}\\-\\d{2}$");
                if(regFIO.IsMatch(lastName.Text) &&
                    regFIO.IsMatch(firstName.Text) &&
                    regFIO.IsMatch(secondName.Text) &&
                    regGroup.IsMatch(group.Text))
                    curRec.Content = lastName.Text + " " + firstName.Text + " " + secondName.Text + " " + group.Text;
            }
        }

        //ВАЛИДАТОР ДЛЯ ПОЛКЙ ВВОДА
        private void textBox_TextChanged(object sender, TextChangedEventArgs e) {
            string input = (sender as TextBox).Text;
            if (!Regex.IsMatch(input, "^[А-Я]{1}([а-я]{1,})?$")) {
                if (input.Length != 0)
                    (sender as TextBox).Text = textBeforeChange;
            }
        }
        private void textBox_TextChanged2(object sender, TextChangedEventArgs e) {
            string input = (sender as TextBox).Text;
            if (!Regex.IsMatch(input, "^([А-Я]+(\\-| )?((\\d){1,2})?(\\-)?((\\d)?){2}$|Группа)")) {
                if (input.Length != 0)
                    (sender as TextBox).Text = textBeforeChange;
            }

        }
        private string textBeforeChange;
        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            textBeforeChange = (sender as TextBox).Text;
        }

        //=======================mainLayout=======================
    }

}

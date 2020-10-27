﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1;
using System.Diagnostics;
using System.Threading;
using System.Diagnostics.Tracing;
using System.Data;

namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для mainWindow.xaml
    /// </summary>
    public partial class mainPage : UserControl {
        public mainPage(Window win, Label currec, ContentControl cC) {
            InitializeComponent();

            mainWin = win;
            curRec = currec;
            contentControl = cC;

            //ширина попапа
            DataPopup.Width = SystemParameters.VirtualScreenWidth / 2;
            
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

            //ширина попапа
            DataPopup.Width = SystemParameters.VirtualScreenWidth / 2;

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
        private static ContentControl contentControl;
        //=======================mainLayout=======================

        //ВЫХОД
        private void mainLayoutExit_Click(object sender, RoutedEventArgs e) {
            mainWin.Close();
        }

        private static bool saveDataFlag=true;
        //ПРОВЕРКА ВВЕДЕННОСТИ ДАННЫХ
        private async void saveData_Click(object sender, RoutedEventArgs e) {
            if(saveDataFlag == true) {
                saveDataFlag = false;

                bool isCurrentData=false;
                if (lastName.Text!="" && lastName.Text!="Фамилия" &&
                    firstName.Text != "" && firstName.Text != "Имя" &&
                    secondName.Text != "" && secondName.Text != "Отчество" &&
                    group.Text != "" && group.Text != "Группа") {
                    Regex regFIO = new Regex("^([А-Я]|[A-Z])([а-я]|[a-z])+$");
                    Regex regGroup = new Regex("^([А-Я]|[A-Z]){2,}(\\-| )?\\d{1,2}\\-\\d{2}$");
                    if(regFIO.IsMatch(lastName.Text) &&
                        regFIO.IsMatch(firstName.Text) &&
                        regFIO.IsMatch(secondName.Text) &&
                        regGroup.IsMatch(group.Text)) {
                        curRec.Content = lastName.Text + " " + firstName.Text + " " + secondName.Text + " " + group.Text;
                        isCurrentData = true;
                    }  
                }

                DataPopupText.Content = isCurrentData == true ? "ДАННЫЕ СОХРАНЕНЫ" : "ДАННЫЕ НЕ СОХРАНЕННЫ";
                DataPopupText.Background = isCurrentData == true ? SpecialColor.green() : SpecialColor.red();
                DataPopup.IsOpen = !DataPopup.IsOpen;
                await Task.Delay(2000);
                DataPopup.IsOpen = !DataPopup.IsOpen;

                saveDataFlag = true;
            }
        }

        //ВАЛИДАТОР ДЛЯ ПОЛКЙ ВВОДА
        private void textBox_TextChanged(object sender, TextChangedEventArgs e) {
            string input = (sender as TextBox).Text;
            if (!Regex.IsMatch(input, "^([А-Я]|[A-Z]){1}(([а-я]|[a-z]){1,})?$")) {
                if (input.Length != 0)
                    (sender as TextBox).Text = textBeforeChange;
            }
        }
        private void textBox_TextChanged2(object sender, TextChangedEventArgs e) {
            string input = (sender as TextBox).Text;
            if (!Regex.IsMatch(input, "^(([А-Я]|[A-Z])+(\\-| )?((\\d){1,2})?(\\-)?((\\d)?){2}$|Группа)")) {
                if (input.Length != 0)
                    (sender as TextBox).Text = textBeforeChange;
            }

        }
        private string textBeforeChange;
        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            textBeforeChange = (sender as TextBox).Text;
        }

        private void mainLayoutResults_Click(object sender, RoutedEventArgs e) {
            resultPopup.IsOpen = !resultPopup.IsOpen;
            if (resultPopup.IsOpen) {
                mainContentGrid.Opacity = 0.3;
                mainContentGrid.IsEnabled = false;
                popupTextBox.Clear();
            }
            else {
                mainContentGrid.Opacity = 1;
                mainContentGrid.IsEnabled = true;
            }
        }

        //ПОПАП
        //ВЫХОД ИЗ ПОПАПА
        private void popupExit_Click(object sender, RoutedEventArgs e) {
            resultPopup.IsOpen = !resultPopup.IsOpen;
            if (resultPopup.IsOpen) {
                mainContentGrid.Opacity = 0.3;
                mainContentGrid.IsEnabled = false;
                popupTextBox.Clear();
            } else {
                mainContentGrid.Opacity = 1;
                mainContentGrid.IsEnabled = true;
            }
        }

        private void popupExit_MouseEnter(object sender, MouseEventArgs e) {
            line1.Stroke = SpecialColor.white();
            line2.Stroke = SpecialColor.white();
        }
        private void popupExit_MouseLeave(object sender, MouseEventArgs e) {
            line1.Stroke = SpecialColor.mainBlue();
            line2.Stroke = SpecialColor.mainBlue();
        }

        //КНОПКА ПОДТВЕРДИТЬ
        private static bool popupButtonFlag = true;
        private static string passwordKey = "12345";
        private async void popupButton_Click(object sender, RoutedEventArgs e) {
            if (popupButtonFlag == true) {
                popupButtonFlag = false;

                if (popupTextBox.Password == passwordKey) {
                    popupButtonFlag = true;
                    contentControl.Content = new resultsPage(mainWin, curRec, contentControl);
                } else {
                    popupButtonConfirm.IsOpen = true;
                    popupTextBox.Clear();
                    await Task.Delay(2000);
                    popupButtonConfirm.IsOpen = false;
                    popupButtonFlag = true;
                }
            }
        }
        //ПОПАП
        //=======================mainLayout=======================
    }

}

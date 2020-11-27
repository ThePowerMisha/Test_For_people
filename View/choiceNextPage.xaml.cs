﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для choiceNextPage.xaml
    /// </summary>
    public partial class choiceNextPage : UserControl {
        public choiceNextPage(ContentControl cC, string title) {
            InitializeComponent();

            contentControl = cC;

            Placeholder.add(timeCount, "Время в минутах");
            Placeholder.add(tipCount, "Подсказки");

            titleLabel.Content = title;
        }
        private static ContentControl contentControl;

        private void onPreviousPage_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = WpfApp1.View.mainPage.getChoiceBlock();
        }

        private string textBeforeChange;
        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            textBeforeChange = (sender as TextBox).Text;
        }
        private void timeCount_TextChanged(object sender, TextChangedEventArgs e) {
            string input = (sender as TextBox).Text;            
            if (!Regex.IsMatch(input, "(^([0-9]){1,3}$|^Время в минутах$)")) {
                if (input.Length != 0) {
                    (sender as TextBox).Text = textBeforeChange;
                    input = textBeforeChange;
                }
            }
            if (input != "Время в минутах" && input.Length != 0) {
                if (Int32.Parse(input) > 180)
                    input = "180";
                else if (Int32.Parse(input) == 0)
                    input = "10";
                (sender as TextBox).Text = Int32.Parse(input).ToString();
            }
        }
        private void timeCount_LostFocus(object sender, RoutedEventArgs e) {
            string input = (sender as TextBox).Text;
            if (input != "Время в минутах" && input.Length != 0) {
                if (Int32.Parse(input) < 10)
                    input = "10";
                (sender as TextBox).Text = Int32.Parse(input).ToString();
            }
        }

        private void tipCount_TextChanged(object sender, TextChangedEventArgs e) {
            string input = (sender as TextBox).Text;
            if (!Regex.IsMatch(input, "(^([0-9]){1,1}$|^Подсказки$)")) {
                if (input.Length != 0) {
                    (sender as TextBox).Text = textBeforeChange;
                    input = textBeforeChange;
                }
            }
            if (input != "Подсказки" && input.Length != 0) {
                if (Int32.Parse(input) > 3)
                    input = "3";
                (sender as TextBox).Text = Int32.Parse(input).ToString();
            }
        }

        //Первый массив карточек
        private static List<Card> loadPositionСardMass = new List<Card> {
            new Card("I.","View/Karpenko.jpeg"),
            new Card("II.","View/Sinitsin.jpg"),
            new Card("III.","View/Jagaev.jpg"),
            new Card("IV.","View/Smirnov.jpg")
        };
        //Второй массив карточек
        private static List<Card> variantCardMass = new List<Card> {
            new Card("I.","View/Karpenko.jpeg"),
            new Card("II.","View/Sinitsin.jpg"),
            new Card("III.","View/Jagaev.jpg"),
            new Card("IV.","View/Smirnov.jpg")
        };

        //загрузка всех карточек
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {

            foreach (Card card in loadPositionСardMass) {
                Button button = new Button();
                button.Style = this.Resources["buttonCard"] as Style;
                button.Content = card.title;
                button.Tag = card.path;
                button.Width = (loadPosition.ActualHeight)/5*8;
                button.Height = (loadPosition.ActualHeight);
                button.Foreground = SpecialColor.mainBlue();
                button.Click += card_Click;
                loadPosition.Children.Add(button);
            }

            foreach (Card card in variantCardMass) {
                Button button = new Button();
                button.Style = this.Resources["buttonCard"] as Style;
                button.Content = card.title;
                button.Tag = card.path;
                button.Width = (variant.ActualHeight) / 5 * 8;
                button.Height = (variant.ActualHeight);
                button.Foreground = SpecialColor.mainBlue();
                button.Click += card_Click2;
                variant.Children.Add(button);
            }
        }

        //выделение для массива с выбором нагрузок
        private static Button loadPositioСurrentButton = null;
        private void card_Click(object sender, RoutedEventArgs e) {
            if ((sender as Button) != loadPositioСurrentButton) {
                if (loadPositioСurrentButton != null) {
                    loadPositioСurrentButton.Background = SpecialColor.mainBack();
                    loadPositioСurrentButton.Foreground = SpecialColor.mainBlue();
                }
                (sender as Button).Background = SpecialColor.mainBlue();
                (sender as Button).Foreground = SpecialColor.white();
                loadPositioСurrentButton = (sender as Button);
            } else {
                (sender as Button).Background = SpecialColor.mainBack();
                (sender as Button).Foreground = SpecialColor.mainBlue();
                loadPositioСurrentButton = null;
            }
        }

        //выделение для массива с выбором варианта
        private static Button variantСurrentButton = null;
        private void card_Click2(object sender, RoutedEventArgs e) {
            if ((sender as Button) != variantСurrentButton) {
                if (variantСurrentButton != null) {
                    variantСurrentButton.Background = SpecialColor.mainBack();
                    variantСurrentButton.Foreground = SpecialColor.mainBlue();
                }
                (sender as Button).Background = SpecialColor.mainBlue();
                (sender as Button).Foreground = SpecialColor.white();
                variantСurrentButton = (sender as Button);
            } else {
                (sender as Button).Background = SpecialColor.mainBack();
                (sender as Button).Foreground = SpecialColor.mainBlue();
                variantСurrentButton = null;
            }
        }

        private async void onNextPage_Click(object sender, RoutedEventArgs e) {
            bool isWrong = false;
            if (timeCount.Text== "Время в минутах") {
                DataPopupText.Content = "ВВЕДИТЕ ВРЕМЯ";
                NextPopup.IsOpen = true;
                isWrong = true;
            } else if(tipCount.Text == "Подсказки") {
                DataPopupText.Content = "ВВЕДИТЕ ПОДСКАЗКИ";
                NextPopup.IsOpen = true;
                isWrong = true;
            } else if (loadPositioСurrentButton==null) {
                DataPopupText.Content = "ВЫБЕРИТЕ ПОЛОЖЕНИЕ ОПОР";
                NextPopup.IsOpen = true;
                isWrong = true;
            } else if (variantСurrentButton == null) {
                DataPopupText.Content = "ВЫБЕРИТЕ ВАРИАНТ";
                NextPopup.IsOpen = true;
                isWrong = true;
            } else {
                testPopup.IsOpen = true;
                mainLayout.IsEnabled = false;
                mainLayout.Opacity = 0.3;
            }
            if (isWrong == true) {
                await Task.Delay(2000);
                NextPopup.IsOpen = false;
            }
        }

        private static testPage tPage;
        private void popupButton_Click(object sender, RoutedEventArgs e) {
            tPage = new testPage(contentControl,
                                titleLabel.Content + " Тест " + loadPositioСurrentButton.Content + "-" + variantСurrentButton.Content,
                                timeCount.Text);
            contentControl.Content = tPage;
        }
        public static testPage getTestPage() {
            return tPage;
        }

        private void popupExit_Click(object sender, RoutedEventArgs e) {
            testPopup.IsOpen = false;
            mainLayout.IsEnabled = true;
            mainLayout.Opacity = 1;
        }
        private void popupExit_MouseEnter(object sender, MouseEventArgs e) {
            line1.Stroke = SpecialColor.white();
            line2.Stroke = SpecialColor.white();
        }
        private void popupExit_MouseLeave(object sender, MouseEventArgs e) {
            line1.Stroke = SpecialColor.mainBlue();
            line2.Stroke = SpecialColor.mainBlue();
        }

        //возвращает состояние попапа testPopup
        public bool getPopupStatus() {
            return testPopup.IsOpen;
        }
        //закрывает попап resultPopup
        public void setPopupStatus() {
            testPopup.IsOpen = false;
            mainLayout.Opacity = 1;
            mainLayout.IsEnabled = true;
        }
    }
}
﻿using System;
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

            //загрузка тем в выпадающий список
            loadDataThemesCB.ItemsSource = themes;


        }
        private static ContentControl contentControl;
        //массив тем
        private static List<string> themes = new List<string> {
            "Фермы"
        };

        //возвращает на главную страницу
        private void onMainPage_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = WpfApp1.MainWindow.getMainPage();
        }

        private static List<Card> cardMass = new List<Card> {
            new Card("I.","View/Karpenko.jpeg"),
            new Card("II.","View/Sinitsin.jpg"),
            new Card("III.","View/Jagaev.jpg"),
            new Card("IV.","View/Smirnov.jpg")
        };

        //тестовая загрузка карточек блоков
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {

            foreach(Card card in cardMass) {
                Button button = new Button();
                button.Style = this.Resources["buttonCard"] as Style;
                button.Content = card.title;
                button.Tag = card.path;
                button.Width = (choiceContent.ActualWidth - 61) / 4;
                button.Height = ((choiceContent.ActualWidth - 45) / 4)/8*5;
                button.Foreground = SpecialColor.mainBlue();
                button.Click += card_Click;
                choiceContent.Children.Add(button);
            }
        }
        private static Button currentButton = null;
        private void card_Click(object sender, RoutedEventArgs e) {
            if ((sender as Button) != currentButton) {
                if (currentButton != null) {
                    currentButton.Background = SpecialColor.mainBack();
                    currentButton.Foreground = SpecialColor.mainBlue();
                }
                (sender as Button).Background = SpecialColor.mainBlue();
                (sender as Button).Foreground = SpecialColor.white();
                currentButton = (sender as Button);
            } else {
                (sender as Button).Background = SpecialColor.mainBack();
                (sender as Button).Foreground = SpecialColor.mainBlue();
                currentButton = null;
            }
        }

        //переход на следующий этап начала тестирования
        private async void onNextPage_Click(object sender, RoutedEventArgs e) {
            if (currentButton == null) {
                NextPopup.IsOpen = true;
                await Task.Delay(2000);
                NextPopup.IsOpen = false;
            } else {
                cNPage = new choiceNextPage(contentControl, loadDataThemesCB.Text + ". Часть " + currentButton.Content.ToString());
                contentControl.Content = cNPage;//new choiceNextPage(contentControl, loadDataThemesCB.Text+". "+ currentButton.Content.ToString());
            }
        }

        private static choiceNextPage cNPage;
        public static choiceNextPage getChoiceNextPage() {
            return cNPage;
        }
    }
    
}

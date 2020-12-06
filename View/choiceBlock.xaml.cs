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
            //currentButton = null;
            //choiceContent.Children.Clear();

            if (choiceContent.Children.Count == 0) {
                foreach (Card card in cardMass) {
                    Button button = new Button();
                    button.Style = this.Resources["buttonCard"] as Style;
                    button.Content = card.title;
                    button.Tag = card.path;
                    button.Width = (choiceContent.ActualWidth - 61) / 4;
                    button.Height = ((choiceContent.ActualWidth - 45) / 4) / 8 * 5;
                    button.Foreground = SpecialColor.mainBlue();
                    button.Click += card_Click;
                    choiceContent.Children.Add(button);
                }
            }


        }

        //Перевод в римские цифры
        public static string ToRoman(int number) {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        public static Button currentButton = null;
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

        public static List<string> dataList = new List<string>();
        //переход на следующий этап начала тестирования
        private async void onNextPage_Click(object sender, RoutedEventArgs e) {
            if (currentButton == null) {
                NextPopup.IsOpen = true;
                await Task.Delay(2000);
                NextPopup.IsOpen = false;
            } else {
                dataList.Clear();
                dataList.Add(loadDataThemesCB.Text);
                dataList.Add(currentButton.Content.ToString());
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

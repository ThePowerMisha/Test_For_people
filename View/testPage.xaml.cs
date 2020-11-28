using System;
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
using System.Windows.Threading;
using System.Globalization;

namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для testPage.xaml
    /// </summary>
    public partial class testPage : UserControl {
        public testPage(ContentControl cC, string title, string time) {
            InitializeComponent();

            //заголовок
            adaptiveTitle(testTitle, title);

            Placeholder.add(answer, "Введите ответ");

            //общие данные (Timer and Score)
            timer(time);

            contentControl = cC;
        }
        private static ContentControl contentControl;
        private void adaptiveTitle(Label testTitle, string title) {
            double fz = testTitle.FontSize;
            testTitle.Content = title;
            FormattedText ft = new FormattedText(title, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), fz, SpecialColor.white());
            while ((ft.Width * 96 / 72) >= (SystemParameters.PrimaryScreenWidth - 900)) {
                if (fz == 10)
                    break;
                fz -= 0.1;
                ft.SetFontSize(fz);
            }
            testTitle.FontSize = fz;
            testTitle.Content = ft.Text;
        }

        //флаг остановки при выходе времени
        private static bool timeOut = false;
        //общий счет баллов
        public static int score=0;
        //общий счет кол-во правельных ответов
        public static int correctAnswersCount=0;
        //время затраченное на тест
        private static int timeWaste=0;

        //таймер
        private static TimeSpan _time;
        private static DispatcherTimer _timer;
        //Timer
        void timer(string time) {
            _time = TimeSpan.FromMinutes(Int32.Parse(time)); //FromSeconds(5);
            subtitleLabel.Content = "Осталось: " + _time.ToString("c") + "      Балл: " + score.ToString();
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate {
                _time = _time.Add(TimeSpan.FromSeconds(-1));
                timeWaste++;
                subtitleLabel.Content = "Осталось: " + _time.ToString("c") + "      Балл: " + score.ToString();
            }, Application.Current.Dispatcher);

            void timerStop(object sender, EventArgs e) {
                if (_time == TimeSpan.Zero) {
                    _timer.Stop();
                    WpfApp1.MainWindow.headersBlock();
                    timeOut = true;
                    mainLayout.Opacity = 0.3;
                    mainLayout.IsEnabled = false;
                    exitTestPopupTitle.Text = "ВРЕМЯ ВЫШЛО";
                    exitTestPopup.IsOpen = true;
                    popupExit.Opacity = 0;
                    popupExit.IsEnabled = false;
                }
            }

            _timer.Tick += timerStop;
            _timer.Start();
        }

        //вылидация поля ввода
        private string textBeforeChange;
        private int selectionBeforeChange;
        private int selectionLengthBeforeChange;
        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            textBeforeChange = (sender as TextBox).Text;
            selectionBeforeChange = (sender as TextBox).SelectionStart;
            selectionLengthBeforeChange = (sender as TextBox).SelectionLength;
        }
        private void answer_TextChanged(object sender, TextChangedEventArgs e) {
            string input = (sender as TextBox).Text;
            if (!Regex.IsMatch(input, "(^[0-9a-zA-Z\\/\\*\\^\\+\\-\\(\\)]*$|^Введите ответ$)")) {
                if (input.Length != 0) {
                    (sender as TextBox).Text = textBeforeChange;
                    input = textBeforeChange;
                }
                (sender as TextBox).SelectionStart = selectionBeforeChange;
                (sender as TextBox).SelectionLength = selectionLengthBeforeChange;
            }
        }

        //возвращает состояние попапа exitTestPopup
        public bool getPopupStatus() {
            return exitTestPopup.IsOpen;
        }
        //закрывает попап exitTestPopup
        public void setPopupStatus() {
            if (timeOut == false) {
                exitTestPopup.IsOpen = false;
                mainLayout.Opacity = 1;
                mainLayout.IsEnabled = true;
            }
            
        }

        //завершение теста
        private void onNextPage_Click(object sender, RoutedEventArgs e) {
            exitTestPopup.IsOpen = true;
            mainLayout.Opacity = 0.3;
            mainLayout.IsEnabled = false;
        }

        private async void confirmAnswer_Click(object sender, RoutedEventArgs e) {
            if (false) {
                AnswerPopupContent.Background = SpecialColor.green();
                AnswerPopupContent.Content = "ПРАВИЛЬНО";
                correctAnswersCount++;
            } else {
                AnswerPopupContent.Background = SpecialColor.red();
                AnswerPopupContent.Content = "НЕПРАВИЛЬНО";
            }
            AnswerPopup.IsOpen = true;
            await Task.Delay(2000);
            AnswerPopup.IsOpen = false;
        }

        private void popupExit_Click(object sender, RoutedEventArgs e) {
            exitTestPopup.IsOpen = false;
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

        //переход к результатам
        private void popupButton_Click(object sender, RoutedEventArgs e) {
            WpfApp1.MainWindow.headersUnlock();
            _timer.Stop();
            contentControl.Content = new afterTestPage(contentControl,
                                                       TimeSpan.FromSeconds(timeWaste).ToString("c"));
        }
    }
}

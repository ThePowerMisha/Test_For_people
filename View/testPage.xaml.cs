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
using System.Windows.Threading;

namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для testPage.xaml
    /// </summary>
    public partial class testPage : UserControl {
        public testPage(ContentControl cC, string title, string time) {
            InitializeComponent();

            //заголовок
            testTitle.Content = title;

            //общие данные (Timer and Score)
            timer(time);

            contentControl = cC;
        }
        private static ContentControl contentControl;

        private static bool timeOut = false;
        private static int score=0;
        private static TimeSpan _time;
        //Timer
        void timer(string time) {
            DispatcherTimer _timer;
            _time = TimeSpan.FromMinutes(Int32.Parse(time));
            subtitleLabel.Content = "Осталось: " + _time.ToString("c") + "      Балл: " + score.ToString();
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate {
                _time = _time.Add(TimeSpan.FromSeconds(-1));
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

        private void popupButton_Click(object sender, RoutedEventArgs e) {
            WpfApp1.MainWindow.headersUnlock();
            contentControl.Content = new afterTestPage(contentControl);
        }
    }
}

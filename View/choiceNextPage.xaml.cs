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
using System.IO;
using dBController;

namespace WpfApp1.View
{
    /// <summary>
    /// Логика взаимодействия для choiceNextPage.xaml
    /// </summary>
    public partial class choiceNextPage : UserControl
    {
        public choiceNextPage(ContentControl cC, string title)
        {
            InitializeComponent();

            contentControl = cC;

            Placeholder.add(timeCount, "Время в минутах");
            Placeholder.add(tipCount, "Подсказки");

            titleLabel.Content = title;
        }
        private static ContentControl contentControl;

        private void onPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = WpfApp1.View.mainPage.getChoiceBlock();
        }

        //Валидаторы и плэйсхолдеры
        private string textBeforeChange;
        private int selectionBeforeChange;
        private int selectionLengthBeforeChange;
        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            textBeforeChange = (sender as TextBox).Text;
            selectionBeforeChange = (sender as TextBox).SelectionStart;
            selectionLengthBeforeChange = (sender as TextBox).SelectionLength;
        }
        private void timeCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text;
            if (!Regex.IsMatch(input, "(^([0-9]){1,3}$|^Время в минутах$)"))
            {
                if (input.Length != 0)
                {
                    (sender as TextBox).Text = textBeforeChange;
                    input = textBeforeChange;
                }
                (sender as TextBox).SelectionStart = selectionBeforeChange;
                (sender as TextBox).SelectionLength = selectionLengthBeforeChange;
            }
            if (input != "Время в минутах" && input.Length != 0)
            {
                if (Int32.Parse(input) > 180)
                    input = "180";
                else if (Int32.Parse(input) == 0)
                    input = "10";
                (sender as TextBox).Text = Int32.Parse(input).ToString();
            }
        }
        private void timeCount_LostFocus(object sender, RoutedEventArgs e)
        {
            string input = (sender as TextBox).Text;
            if (input != "Время в минутах" && input.Length != 0)
            {
                if (Int32.Parse(input) < 10)
                    input = "10";
                (sender as TextBox).Text = Int32.Parse(input).ToString();
            }
        }
        private void tipCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text;
            if (!Regex.IsMatch(input, "(^([0-9]){1,1}$|^Подсказки$)"))
            {
                if (input.Length != 0)
                {
                    (sender as TextBox).Text = textBeforeChange;
                    input = textBeforeChange;
                }
                (sender as TextBox).SelectionStart = selectionBeforeChange;
                (sender as TextBox).SelectionLength = selectionLengthBeforeChange;
            }
            if (input != "Подсказки" && input.Length != 0)
            {
                if (Int32.Parse(input) > 3)
                    input = "3";
                (sender as TextBox).Text = Int32.Parse(input).ToString();
            }
        }

        //Первый массив карточек
        private static List<List<string>> loadPositionСardMass;
        //Второй массив карточек
        private static List<List<string>> variantCardMass;

        //загрузка всех карточек(изначально варианты загружаются для первого полежения)
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadPositionСardMass = questions.getLoadsImgPath(choiceBlock.theme, choiceBlock.blockID);
            //variantCardMass = questions.getVariantsImgPath(choiceBlock.theme, choiceBlock.blockID, 1);

            int num = 1;
            foreach (List<string> card in loadPositionСardMass)
            {
                Button button = new Button();
                button.Style = this.Resources["buttonCard"] as Style;
                button.Content = choiceBlock.ToRoman(num);
                num++;
                button.Tag = System.IO.Path.GetFullPath(card[1]);
                button.Width = (loadPosition.ActualHeight) / 5 * 8;
                button.Height = (loadPosition.ActualHeight);
                button.Foreground = SpecialColor.mainBlue();
                button.Click += card_Click;
                loadPosition.Children.Add(button);
            }

            //num = 1;
            //foreach (string card in variantCardMass) {
            //    Button button = new Button();
            //    button.Style = this.Resources["buttonCard"] as Style;
            //    button.Content = choiceBlock.ToRoman(num);
            //    num++;
            //    button.Tag = System.IO.Path.GetFullPath(card);
            //    button.Width = (variant.ActualHeight) / 5 * 8;
            //    button.Height = (variant.ActualHeight);
            //    button.Foreground = SpecialColor.mainBlue();
            //    button.Click += card_Click2;
            //    variant.Children.Add(button);
            //}
        }

        public static int loadID;
        public static int variantID;
        //выделение для массива с выбором нагрузок
        private static Button loadPositioСurrentButton = null;
        private void card_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button) != loadPositioСurrentButton)
            {
                if (loadPositioСurrentButton != null)
                {
                    loadPositioСurrentButton.Background = SpecialColor.mainBack();
                    loadPositioСurrentButton.Foreground = SpecialColor.mainBlue();
                }
                (sender as Button).Background = SpecialColor.mainBlue();
                (sender as Button).Foreground = SpecialColor.white();
                loadPositioСurrentButton = (sender as Button);


                int id = 0;
                foreach (Button button in loadPosition.Children)
                {
                    if (loadPositioСurrentButton == button)
                    {
                        variantCardMass = questions.getVariantsImgPath(choiceBlock.theme, choiceBlock.blockID, Int32.Parse(loadPositionСardMass[id][0]));
                        loadID = Int32.Parse(loadPositionСardMass[id][0]);
                        break;
                    }
                    id++;
                }
                //изменение вариантов
                //variantCardMass = questions.getVariantsImgPath(choiceBlock.theme, choiceBlock.blockID, ID);
                variantСurrentButton = null;
                variant.Children.Clear();
                int num = 1;
                foreach (List<string> card in variantCardMass)
                {
                    Button button = new Button();
                    button.Style = this.Resources["buttonCard"] as Style;
                    button.Content = choiceBlock.ToRoman(num);
                    num++;
                    button.Tag = System.IO.Path.GetFullPath(card[1]);
                    button.Width = (variant.ActualHeight) / 5 * 8;
                    button.Height = (variant.ActualHeight);
                    button.Foreground = SpecialColor.mainBlue();
                    button.Click += card_Click2;
                    variant.Children.Add(button);
                }
            }
            else
            {
                (sender as Button).Background = SpecialColor.mainBack();
                (sender as Button).Foreground = SpecialColor.mainBlue();
                loadPositioСurrentButton = null;


                if (variantСurrentButton != null)
                {
                    variantСurrentButton.Background = SpecialColor.mainBack();
                    variantСurrentButton.Foreground = SpecialColor.mainBlue();
                }
                variantСurrentButton = null;
                variant.Children.Clear();

            }
        }

        //выделение для массива с выбором варианта
        private static Button variantСurrentButton = null;
        private void card_Click2(object sender, RoutedEventArgs e)
        {
            if ((sender as Button) != variantСurrentButton)
            {
                if (variantСurrentButton != null)
                {
                    variantСurrentButton.Background = SpecialColor.mainBack();
                    variantСurrentButton.Foreground = SpecialColor.mainBlue();
                }
                (sender as Button).Background = SpecialColor.mainBlue();
                (sender as Button).Foreground = SpecialColor.white();
                variantСurrentButton = (sender as Button);

                int id = 0;
                foreach (Button button in variant.Children)
                {
                    if (variantСurrentButton == button)
                    {
                        variantID = Int32.Parse(variantCardMass[id][0]);
                        break;
                    }
                    id++;
                }
            }
            else
            {
                (sender as Button).Background = SpecialColor.mainBack();
                (sender as Button).Foreground = SpecialColor.mainBlue();
                variantСurrentButton = null;
            }
        }

        //обработка введенности данных
        private async void onNextPage_Click(object sender, RoutedEventArgs e)
        {
            bool isWrong = false;
            if (timeCount.Text == "Время в минутах")
            {
                DataPopupText.Content = "ВВЕДИТЕ ВРЕМЯ";
                NextPopup.IsOpen = true;
                isWrong = true;
            }
            else if (tipCount.Text == "Подсказки")
            {
                DataPopupText.Content = "ВВЕДИТЕ ПОДСКАЗКИ";
                NextPopup.IsOpen = true;
                isWrong = true;
            }
            else if (loadPositioСurrentButton == null)
            {
                DataPopupText.Content = "ВЫБЕРИТЕ ПОЛОЖЕНИЕ ОПОР";
                NextPopup.IsOpen = true;
                isWrong = true;
            }
            else if (variantСurrentButton == null)
            {
                DataPopupText.Content = "ВЫБЕРИТЕ ВАРИАНТ";
                NextPopup.IsOpen = true;
                isWrong = true;
            }
            else
            {
                testPopup.IsOpen = true;
                mainLayout.IsEnabled = false;
                mainLayout.Opacity = 0.3;
            }
            if (isWrong == true)
            {
                await Task.Delay(2000);
                NextPopup.IsOpen = false;
            }
        }

        //переход к тесту
        public static List<string> dataList = new List<string>();
        private static testPage tPage;
        private void popupButton_Click(object sender, RoutedEventArgs e)
        {
            tPage = new testPage(contentControl,
                                titleLabel.Content + " Тест " + loadPositioСurrentButton.Content + "-" + variantСurrentButton.Content,
                                timeCount.Text);
            dataList.Clear();
            dataList.Add(loadPositioСurrentButton.Content.ToString());
            dataList.Add(variantСurrentButton.Content.ToString());
            dataList.Add(tipCount.Text.ToString());
            contentControl.Content = tPage;

            //=================
            TestControl test = new TestControl();
            //LoaderClass loaderClass = new LoaderClass("Questions.txt");
            CheckAnswer checkAnswer = new CheckAnswer();

            // Загружаем из файла словарь
            checkAnswer.Variables = questions.getQuestionParams(choiceBlock.theme, choiceBlock.blockID, loadID, variantID);
            
            // Загружаем в графический интерфейс данные массива
            string testtext = "";
            foreach (var massiv in checkAnswer.Variables)
            {
                testtext += $"{massiv.Key} = {massiv.Value}\n";
            }
            test.DataExtraInfo(testtext);

            // Загружаем текст вопроса
            test.DataMainInfo(questions.getQuestionText(choiceBlock.theme, choiceBlock.blockID, loadID, variantID, 1));

            // Загружаем неизвестные переменные, которые нужно найти
            test.QuestionVals(questions.getQuestionFinds(choiceBlock.theme, choiceBlock.blockID, loadID, variantID, 1));

            

            test.GraphContent(System.IO.Path.GetFullPath(questions.getVariantsImgPath(choiceBlock.theme, choiceBlock.blockID, loadID)[0][1].ToString()));


            /*
            // Загружаем из файла словарь
            checkAnswer.Variables = loaderClass.returnVariables();

            // Загружаем текст вопроса
            test.DataMainInfo(loaderClass.returnQuestionText());

            // Загружаем в графический интерфейс данные массива
            string testtext = "";
            foreach (var massiv in checkAnswer.Variables)
            {
                testtext += $"{massiv.Key} = {massiv.Value}\n";
            }
            test.DataExtraInfo(testtext);

            // Загружаем неизвестные переменные, которые нужно найти

            test.QuestionVals(loaderClass.returnQuestionFindParams());
            */
            // test.AnswerTip("shalom!", "red");
            // test.AnswerTip("hallo!", "green");
            // test.GraphContent("View/Karpenko.jpeg");
            // test.SecondGraphContent("View/Karpenko.jpeg");
            // test.SecondGraphContent("View/Karpenko.jpeg");
            // test.AnswerTipRemove("Правильный ответ...");
            // test.AnswerTipRemove(1);
            // test.SecondGraphContentRemove(1);
            // test.SecondGraphContentRemove("View/Karpenko.jpeg");
            // test.VisualTip("E:/GroupProject/AppXML/WpfApp1/WpfApp1/View/Karpenko.jpeg");
            // test.VisualTip("E:/GroupProject/AppXML/WpfApp1/WpfApp1/View/Jagaev.jpg");
            // test.VisualTip("E:/GroupProject/AppXML/WpfApp1/WpfApp1/View/Smirnov.jpg");
            // test.VisualTip("E:/GroupProject/AppXML/WpfApp1/WpfApp1/View/Sinitsin.jpg");
            // test.DataExtraInfo(test.Time());
            //==================
        }
        public static testPage getTestPage()
        {
            return tPage;
        }

        //закрытие попапа
        private void popupExit_Click(object sender, RoutedEventArgs e)
        {
            testPopup.IsOpen = false;
            mainLayout.IsEnabled = true;
            mainLayout.Opacity = 1;
        }
        //анимация крестика попапа
        private void popupExit_MouseEnter(object sender, MouseEventArgs e)
        {
            line1.Stroke = SpecialColor.white();
            line2.Stroke = SpecialColor.white();
        }
        private void popupExit_MouseLeave(object sender, MouseEventArgs e)
        {
            line1.Stroke = SpecialColor.mainBlue();
            line2.Stroke = SpecialColor.mainBlue();
        }

        //возвращает состояние попапа testPopup
        public bool getPopupStatus()
        {
            return testPopup.IsOpen;
        }
        //закрывает попап resultPopup
        public void setPopupStatus()
        {
            testPopup.IsOpen = false;
            mainLayout.Opacity = 1;
            mainLayout.IsEnabled = true;
        }
    }
}

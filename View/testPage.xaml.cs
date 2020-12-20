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
using System.Windows.Threading;
using System.Globalization;
using dBController;

namespace WpfApp1.View {
    /// <summary>
    /// Логика взаимодействия для testPage.xaml
    /// </summary>
    public partial class testPage : UserControl {
        public testPage(ContentControl cC, string title, string time) {
            InitializeComponent();

            //заголовок
            adaptiveTitle(testTitle, title);

            //служебная инициализация
            Placeholder.add(answer, "Введите ответ");
            timeWaste = 0;
            timeOut = false;
            score = 0;
            correctAnswersCount = 0;

            //общие данные (Timer and Score)
            timer(time);

            mainLayout.MaxWidth = SystemParameters.PrimaryScreenWidth;

            contentControl = cC;
        }

        private int currentQuestion = 0;
        

        private static ContentControl contentControl;
        public double aWidth;

        //адаптиваня ширина заголовка
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
        public int score=0;
        //общий счет кол-во правельных ответов
        public static int correctAnswersCount=0;
        //время затраченное на тест
        public int timeWaste=0;

        //таймер
        public TimeSpan _time;
        private DispatcherTimer _timer;
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
            if (!Regex.IsMatch(input, "(^[0-9a-zA-Z\\/\\*\\^\\+\\-\\(\\)\\,\\.]*$|^Введите ответ$)")) {
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
        CheckAnswer checkAnswer =new CheckAnswer();

        // счетчик
        public int Num = 1;

        // Список попыток ответа для каждой неизветсной переменной
        public List<int> numTry = null;

        // Список неизвестных переменных
        public List<string> questionFindList = null;

        // Список правильных формул для неизвестных переменных
        public List<Dictionary<string, string>> questionFormulasDict = null;

        public int maxStageNum = questions.getMaxQuestionFinds(choiceBlock.theme, choiceBlock.blockID, choiceNextPage.loadID, choiceNextPage.variantID);

        //подтверждение ответа
        private async void confirmAnswer_Click(object sender, RoutedEventArgs e) {
            
            TestControl test_one = new TestControl();

            // Создаем словарь исходных данных
            if (checkAnswer.Variables == null)
                checkAnswer.Variables = questions.getQuestionParams(choiceBlock.theme, choiceBlock.blockID, choiceNextPage.loadID, choiceNextPage.variantID);

            // Создаем счетчик попыток для каждой неизвестной переменной, которую необходимо найти.
            if (numTry == null)
            {
                numTry = new List<int>();
                foreach (var item in questions.getQuestionFinds(choiceBlock.theme, choiceBlock.blockID, choiceNextPage.loadID, choiceNextPage.variantID, Num))
                {
                    numTry.Add(0);
                }
            }



            // Создаем список неизвестных переменных
            if (questionFindList == null) 
                questionFindList = questions.getQuestionFinds(choiceBlock.theme, choiceBlock.blockID, choiceNextPage.loadID, choiceNextPage.variantID, Num);

            if (questionFormulasDict == null)
                questionFormulasDict = questions.getQuestionFormuls(choiceBlock.theme, choiceBlock.blockID, choiceNextPage.loadID, choiceNextPage.variantID, Num);

            // Рассчитывем введенную формулу
            checkAnswer.Formula = answer.Text;
            string formualaInput = checkAnswer.Check();


            // Строка правильной формулы
            checkAnswer.Formula = questionFormulasDict[test_one.SelectedIndex()]["0"];
            string formulaCorrectExport = checkAnswer.Formula;
            string formulaCorrect = checkAnswer.Check();


            // Проверяем на ошибку "NullOrEmpty Error!" - если была передана пустая строка
            if (formualaInput == "NullOrEmpty Error!")
            {
                AnswerPopupContent.Background = SpecialColor.orange();
                AnswerPopupContent.Content = "ОШИБКА";
                test_one.AnswerTipsClear();
                test_one.AnswerTip($"Ошибка! Введена пустая строка", "orange");
            }


            // Проверяем на ошибку "Syntax Error!" - если была выявлена синтаксическая ошибка
            else if (formualaInput == "Syntax Error!")
            {
                AnswerPopupContent.Background = SpecialColor.orange();
                AnswerPopupContent.Content = "ОШИБКА";
                test_one.AnswerTipsClear();
                test_one.AnswerTip($"Была выявлена синтаксическая ошибка!", "orange");
            }


            // Проверяем на ошибку "Bracket Error!" - если была выявлена ошибка правильности написания скобок
            else if (formualaInput == "Bracket Error!")
            {
                AnswerPopupContent.Background = SpecialColor.orange();
                AnswerPopupContent.Content = "ОШИБКА";
                test_one.AnswerTipsClear();
                test_one.AnswerTip($"Ошибка! Неправильно расставлены скобки!", "orange");
            }


            // Проверяем на ошибку "UnknownData Error!" - если были переданы неизвестные данные
            else if (formualaInput == "UnknownData Error!")
            {
                AnswerPopupContent.Background = SpecialColor.orange();
                AnswerPopupContent.Content = "ОШИБКА";
                test_one.AnswerTipsClear();
                test_one.AnswerTip($"Ошибка! Введены неизвестные данные!", "orange");
            }

            // Если ошибок нет, то проводим рассчет введенной формулы и сравниваем её с правильной формулой
            // Если ответ совпал, то выводим информацию о "правильности" ответа и засчитываем правильный ответ.
            else if (Convert.ToDouble(formulaCorrect) == Convert.ToDouble(formualaInput))
            {
                AnswerPopupContent.Background = SpecialColor.green();
                AnswerPopupContent.Content = "ПРАВИЛЬНО";

                // +1 к счетчику правильных ответов
                correctAnswersCount++;

                // За правильный ответ дается 5 баллов
                test_one.Score(5);

                // На вопрос был дан ответ, следовательно попытки больше не нужны
                numTry[test_one.SelectedIndex()] = 3;

                test_one.AnswerTipsClear();
                test_one.AnswerTip($"Правильно! Ответ: {formulaCorrectExport}", "green");

            }
            else
            {
                AnswerPopupContent.Background = SpecialColor.red();
                AnswerPopupContent.Content = "НЕПРАВИЛЬНО";

                // За не правильный ответ балл на 1
                test_one.Score(-1);

                // +1 к кол-ву попыток
                numTry[test_one.SelectedIndex()]++;

                // Очищаем другие уведомления
                test_one.AnswerTipsClear();

                if (numTry[test_one.SelectedIndex()] < 3)
                {
                    test_one.AnswerTip($"Дан неправильный ответ. У вас осталось еще {3 - numTry[test_one.SelectedIndex()]} попытки(-а)", "red");
                }
                else
                {
                    test_one.AnswerTip($"Попыток больше нет!", "red");
                    test_one.AnswerTip($"Правильная формула: {formulaCorrectExport}", "red");
                    foreach(var item in questionFormulasDict[test_one.SelectedIndex()])
                    {
                        if (checkAnswer.Variables.ContainsKey(item.Key))
                        {
                            test_one.AnswerTip($"Правильная формула: {item.Value}", "red");
                        }
                    }
                }
            }

            if (numTry[test_one.SelectedIndex()] >= 3)
            {
                // Если эта переменная еще не найдена, то добавляем ее в словарь переменных
                checkAnswer.Variables.Add(questionFindList[test_one.SelectedIndex()].ToLower(), Convert.ToDouble(formulaCorrect));
                
                // Обновляем в графическом интерфейсе граф "задача"
                test_one.QuestionVal(test_one.SelectedValue(), formulaCorrect);

                // Удаляем найденную переменную из списка неизвестных переменных и выпадающего списка
                questionFindList.Remove(test_one.SelectedValue());

                // Удаляем счетчик кол-ва попыток ответа для найденой переменной
                numTry.RemoveAt(test_one.SelectedIndex());

                // Удаляем формулу для найденной переменной
                questionFormulasDict.RemoveAt(test_one.SelectedIndex());

                // Очищаем вводное поле
                answer.Clear();

                // Обновляем выпадающий список
                test_one.QuestionValsСВ(questionFindList);
            }

            AnswerPopup.IsOpen = true;
            await Task.Delay(1000);
            AnswerPopup.IsOpen = false;

            
            // Если закончился 1-ый этап ответов на вопрос, то переходим к следующему, иначе завершаем тест
            
            if (questionFindList.Count <= 0)
            {
                Num += 1;
                
                if (Num >=maxStageNum + 1)
                    popupButton_Click(sender, e);
                else
                {
                    // Обновляем исходные данные
                    string testtext = "";
                    foreach (var massiv in checkAnswer.Variables)
                    {
                        testtext += $"{massiv.Key} = {massiv.Value}\n";
                    }
                    test_one.DataExtraInfo(testtext);

                    test_one.DataMainInfo(questions.getQuestionText(choiceBlock.theme, choiceBlock.blockID, choiceNextPage.loadID, choiceNextPage.variantID, Num));
                    test_one.QuestionVals(questions.getQuestionFinds(choiceBlock.theme, choiceBlock.blockID, choiceNextPage.loadID, choiceNextPage.variantID, Num));

                    numTry = null;
                    questionFindList = null;
                    questionFormulasDict = null;
                   
                }
                
            }
            
            

            /*
            

            
            

            

            // Если ответы не совпали, то выводим информацию об ошибке и выводим правильную формулу. Правильный ответ не засчитывается
            

            // Если попыток больше нет
            

            AnswerPopup.IsOpen = true;
            await Task.Delay(1000);
            AnswerPopup.IsOpen = false;
            
            // Если закончился 1-ый этап ответов на вопрос, то переходим к следующему, иначе завершаем тест
            if (loaderClasses[currentQuestion].returnQuestionFindParams().Count <= 0)
            {
                currentQuestion += 1;
                if (currentQuestion >= loaderClasses.Count)
                    popupButton_Click(sender, e);
                else
                {
                    // Обновляем исходные данные
                    string testtext = "";
                    foreach (var massiv in checkAnswer.Variables)
                    {
                        testtext += $"{massiv.Key} = {massiv.Value}\n";
                    }
                    test_one.DataExtraInfo(testtext);

                    test_one.DataMainInfo(loaderClasses[currentQuestion].returnQuestionText());
                    test_one.QuestionVals(loaderClasses[currentQuestion].returnQuestionFindParams());
                }
            }
            
            //popupButton_Click( sender, e);
            */
        }

        //выход их попапа
        private void popupExit_Click(object sender, RoutedEventArgs e) {
            exitTestPopup.IsOpen = false;
            mainLayout.IsEnabled = true;
            mainLayout.Opacity = 1;
        }
        //анимация крестика
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
                                                       TimeSpan.FromSeconds(timeWaste).ToString("c"),
                                                       score.ToString());
        }
    }
}

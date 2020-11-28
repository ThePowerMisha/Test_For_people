using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1 {
    class TestControl {
        /// <summary>
        /// Конструктор по умолчанию (взаимодействует с проинициализированным тестом)
        /// </summary>
        public TestControl() {
            this.tPage = View.choiceNextPage.getTestPage();
        }
        private View.testPage tPage;

        /// <summary>
        /// В формате строки возвращает основной текст из "Дано"
        /// </summary>
        /// <returns>текст из "Дано"</returns>
        public string DataMainInfo() => this.tPage.DataMainInfo.Text;

        /// <summary>
        /// Записывает в основной текст из "Дано" новый текст
        /// </summary>
        /// <param name="text">Текст для записи</param>
        public void DataMainInfo(string text) {
            this.tPage.DataMainInfo.Text = text;
        }

        /// <summary>
        /// В формате строки возвращает дополнительный текст из "Дано"
        /// </summary>
        /// <returns>дополнительный текст из "Дано"</returns>
        public string DataExtraInfo() => this.tPage.DataExtraInfo.Text;

        /// <summary>
        /// Записывает в дополнительный текст из "Дано" новый текст
        /// </summary>
        /// <param name="text">Текст для записи</param>
        public void DataExtraInfo(string text) {
            this.tPage.DataExtraInfo.Text = text;
        }

        /// <summary>
        /// В формате строки возвращает текст из "Задача"
        /// </summary>
        /// <returns></returns>
        public string QuestionInfo() => this.tPage.QuestionInfo.Text;

        /// <summary>
        /// Записывает в "Задача" новый текст
        /// </summary>
        /// <param name="text">Текст для записи</param>
        public void QuestionInfo(string text) {
            this.tPage.QuestionInfo.Text = text;
        }

        /// <summary>
        /// Записывает в "Задача" и выпадающий список переменные, которые необходимо найти
        /// </summary>
        /// <param name="valsMass">массив переменных в формате строки</param>
        public void QuestionVals(List<string> valsMass) {
            this.tPage.QuestionVals.Children.Clear();
            foreach (string text in valsMass) {
                Label label = new Label();
                label.Content = text+" = ?";
                this.tPage.QuestionVals.Children.Add(label);
            }
            this.tPage.QuestionValueCB.Items.Clear();
            this.tPage.QuestionValueCB.ItemsSource = valsMass;
            this.tPage.QuestionValueCB.SelectedIndex = 0;
        }

        /// <summary>
        /// Создает уведомление, которое принимет текст, который будет содержать уведомление, и формат уведомления
        /// типы форматов:
        /// classic - классический серый фон и классивеский синий шрифт(при неправильном параметре, будет установлен по умолчанию);
        /// red     - красный фон и белый шрифт;
        /// green   - зеленый фон и белый шрифт;
        /// orange  - оранженый фон и белый шрифт
        /// </summary>
        /// <param name="text">Текст уведомления</param>
        /// <param name="format">Формат уведомления</param>
        public void AnswerTip(string text, string format) {
            Label label = new Label();
            label.Content = text;
            if (format == "red") {
                label.Foreground = SpecialColor.white();
                label.Background = SpecialColor.red();
            } else if (format == "green") {
                label.Foreground = SpecialColor.white();
                label.Background = SpecialColor.green();
            } else if (format == "orange") {
                label.Foreground = SpecialColor.white();
                label.Background = SpecialColor.orange();
            }
            this.tPage.answerTips.Children.Add(label);
        }

        /// <summary>
        /// Очищает все уведомления
        /// </summary>
        public void AnswerTipsClear() {
            this.tPage.answerTips.Children.Clear();
        }

        /// <summary>
        /// Удаляет объявление с определенным текстом (удаляется только первое встреченное объявление)
        /// </summary>
        /// <param name="text">текст для удаления</param>
        public void AnswerTipRemove(string text) {
            foreach (Label label in this.tPage.answerTips.Children) {
                if (label.Content.ToString() == text) {
                    this.tPage.answerTips.Children.Remove(label);
                    break;
                }
            }
        }

        /// <summary>
        /// Удаляет объявление под определенным индексом
        /// </summary>
        /// <param name="index">индекс элемента</param>
        public void AnswerTipRemove(int index) {
            this.tPage.answerTips.Children.RemoveAt(index);
        }

        /// <summary>
        /// Устанавливает изображение главного чертежа (предыдущий чертеж будет перезатерт)
        /// </summary>
        /// <param name="path">путь к изоюражению</param>
        public void GraphContent(string path) {
            this.tPage.GraphContentLabel.Opacity = 0;
            if (this.tPage.GraphContent.Children.Count > 1)
                this.tPage.GraphContent.Children.RemoveAt(1);
            Border image = new Border();
            image.Style = this.tPage.Resources["borderImage"] as Style;
            image.Tag = path;
            this.tPage.GraphContent.Children.Add(image);
        }

        /// <summary>
        /// Удаляет изображение главного чертежа
        /// </summary>
        public void GraphContentClear() {
            this.tPage.GraphContentLabel.Opacity = 0.3;
            while (this.tPage.GraphContent.Children.Count > 1)
                this.tPage.GraphContent.Children.RemoveAt(1);
        }

        /// <summary>
        /// Устанавливает изображение для эпюры моментов (добавляет новое изображение поверх предыдущего)
        /// </summary>
        /// <param name="path">путь к изоюражению</param>
        public void SecondGraphContent(string path) {
            this.tPage.newGraphContentLabel.Opacity = 0;
            Border image = new Border();
            image.Style = this.tPage.Resources["borderImage"] as Style;
            image.Tag = path;
            this.tPage.newGraphContent.Children.Add(image);
        }

        /// <summary>
        /// Удаляет изображение эпюры моментов
        /// </summary>
        public void SecondGraphContentClear() {
            this.tPage.newGraphContentLabel.Opacity = 0.3;
            while (this.tPage.newGraphContent.Children.Count > 1)
                this.tPage.newGraphContent.Children.RemoveAt(1);
        }

        /// <summary>
        /// Удаляет эпюру моментов с определенным путем к изображению (удаляется только первое встреченное изображения)
        /// </summary>
        /// <param name="text">текст для удаления</param>
        public void SecondGraphContentRemove(string path) {
            for (int i = 0; i < this.tPage.newGraphContent.Children.Count; i++) {
                if (this.tPage.newGraphContent.Children[i] is Border && (this.tPage.newGraphContent.Children[i] as Border).Tag.ToString() == path) {
                    this.tPage.newGraphContent.Children.Remove(this.tPage.newGraphContent.Children[i]);
                    break;
                }
            }
            if (this.tPage.newGraphContent.Children.Count==1)
                this.tPage.newGraphContentLabel.Opacity = 0.3;
        }

        /// <summary>
        /// Удаляет эпюру моментов под определенным индексом (учти, что самый первый дочерний элемент в данном массиве это Label, а не нужное изображение)
        /// </summary>
        /// <param name="index">индекс элемента</param>
        public void SecondGraphContentRemove(int index) {
            this.tPage.newGraphContent.Children.RemoveAt(index);
            if (this.tPage.newGraphContent.Children.Count == 1)
                this.tPage.newGraphContentLabel.Opacity = 0.3;
        }
    }
}

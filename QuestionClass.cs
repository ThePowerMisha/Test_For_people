using System;
using System.Runtime.CompilerServices;

namespace TestTrainingProgram
{
    class QuestionClass
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="questionText"></param>
        /// <param name="questionImagepath"></param>
        /// <param name="questionHelptext"></param>
        /// <param name="questionAnswer"></param>
        /// <param name="questionFormulaGraphs"></param>
        public QuestionClass(string questionText, string questionImagepath,string questionHelptext,string[] questionAnswer,string questionFormulaGraphs) {
            this.questionText = questionText;
            this.questionImagepath = questionImagepath;
            this.questionHelptext = questionHelptext;
            this.questionAnswer = questionAnswer;
            this.questionFormulaGraphs = questionFormulaGraphs;
        }
        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string questionText { get; set; }

        /// <summary>
        /// Путь к изображениям
        /// </summary>
        public string questionImagepath { get; set; }

        /// <summary>
        /// Текст подсказки
        /// </summary>
        public string questionHelptext { get; set; }

        /// <summary>
        /// Ответы на вопросы
        /// </summary>
        public string[] questionAnswer { get; set; }

        /// <summary>
        /// Формула графика
        /// </summary>
        public string questionFormulaGraphs { get; set; }
    }
}    
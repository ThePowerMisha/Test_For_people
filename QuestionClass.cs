using System;
using System.Runtime.CompilerServices;

namespace TestTrainingProgram
{
    public class QuestionClass
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public QuestionClass(){}
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
        public string questionText { get; }

        /// <summary>
        /// Путь к изображениям
        /// </summary>
        public string questionImagepath { get; }

        /// <summary>
        /// Текст подсказки
        /// </summary>
        public string questionHelptext { get; }

        /// <summary>
        /// Ответы на вопросы
        /// </summary>
        public string[] questionAnswer { get; }

        /// <summary>
        /// Формула графика
        /// </summary>
        public string questionFormulaGraphs { get; }
    }
}    
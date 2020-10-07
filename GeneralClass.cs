using System;
using System.Collections.Generic;

namespace TestTrainingProgram
{
    public class GeneralClass
    {
        /// <summary>
        /// 
        /// </summary>
        private List<QuestionClass> qustions = new List<QuestionClass>();
        
        /// <summary>
        /// Возвращает данные по вопросу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public QuestionClass returnQuestionInfo(int index) {
            return question[index];
        }
        
        /// <summary>
        /// Храниение пользовательских данных {ФИО, Группа}
        /// </summary>
        public String[] studentData { get; set; }
            
        
    }
}
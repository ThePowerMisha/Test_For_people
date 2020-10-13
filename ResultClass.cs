using System;

namespace TestTrainingProgram
{
    public class ResultClass
    {
        /// <summary>
        /// Потраченное время
        /// </summary>
        public int timeSpent { get; set; }
        
        /// <summary>
        /// Название теста
        /// </summary>
        public string testName { get; set; }
        
        /// <summary>
        /// Название темы
        /// </summary>
        public string themeName { get; set; }
        
        /// <summary>
        /// Результат теста
        /// </summary>
        public int scoreResult { get; set; }
        
        /// <summary>
        /// Вариант
        /// </summary>
        public int variant { get; set; }
        
        /// <summary>
        /// Дата теста
        /// </summary>
        public string testDate { get; set; }
        
        /// <summary>
        /// Количество вопросов[Кол-во;Верные]
        /// </summary>
        public string[] questionNumbers { get; set; }
        
    }
}
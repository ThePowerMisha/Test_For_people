using System;

namespace TestTrainingProgram
{
    public class ResultClass
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ResultClass(){}
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="timeSpent"></param>
        /// <param name="testName"></param>
        /// <param name="themeName"></param>
        /// <param name="scoreResult"></param>
        /// <param name="variant"></param>
        /// <param name="testDate"></param>
        /// <param name="questionNumbers"></param>
        public ResultClass(int timeSpent, string testName, string themeName, int scoreResult, int variant, string testDate, string[] questionNumbers) 
        {
            this.timeSpent = timeSpent;
            this.testName = testName;
            this.themeName = themeName;
            this.scoreResult = scoreResult;
            this.variant = variant;
            this.testDate = testDate;
            this.questionNumbers = questionNumbers;
        }


        /// <summary>
        /// Потраченное время
        /// </summary>
        private int _timeSpent;
        public int timeSpent 
        {
            get => _timeSpent;

            set
            {
                if (value < 0)
                {
                    _timeSpent = 0;
                }
                else
                {
                    _timeSpent = value;
                }
            }
        }

        /// <summary>
        /// Название теста
        /// </summary>
        private string _testName = "Test_Name";
        public string testName 
        {
            get => _testName;

            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _testName = value;
                }
                else
                {
                    throw new ArgumentException($"Передана пустая строка: {nameof(_testName)}");
                }
            }
        }

        /// <summary>
        /// Название темы
        /// </summary>
        private string _themeName = "Theme_Name";
        public string themeName 
        {
            get => _themeName;

            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _themeName = value;
                }
                else
                {
                    throw new ArgumentException($"Передана пустая строка: {nameof(_themeName)}");
                }
            }
        }

        /// <summary>
        /// Результат теста
        /// </summary>
        private int _scoreResult = 0;
        public int scoreResult 
        {
            get => _scoreResult;

            set
            {
                if (value < 0)
                {
                    _scoreResult = 0;
                }
                else if (value > 100)
                {
                    _scoreResult = 100;
                }
                else
                {
                    _scoreResult = value;
                }
            }
        }

        /// <summary>
        /// Вариант
        /// </summary>
        private int _variant = 1;
        public int variant 
        {
            get => _variant;

            set
            {
                if (value <= 0)
                {
                    _variant = 1;
                }
                else
                {
                    _variant = value;
                }
            }
        }

        /// <summary>
        /// Дата теста
        /// </summary>
        private string _testDate = "01.01.2020";
        public string testDate 
        {
            get => _testDate;

            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _testDate = value;
                }
                else
                {
                    throw new ArgumentException($"Передана пустая строка: {nameof(_testDate)}");
                }
            }
        }
        
        /// <summary>
        /// Количество вопросов[Кол-во;Верные]
        /// </summary>
        public string[] questionNumbers { get; set; }
        
    }
}
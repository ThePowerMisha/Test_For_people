using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace WpfApp1
{
   public class LoaderClass
    {
        // Путь к вопросу
        public string path;
        
        // Спиток известных переменных и их значений
        public Dictionary<string, double> dictionary;
        
        // Текст вопроса
        public string QuestionText;
        
        // Ответы в виде формул на вопрос
        public List<string> Formula;
        
        // Переменные которые надо найти
        public List<string> QuestionFindParams;

        // Cчетчик попыток для каждой неизвестной переменной
        public List<int> QuestionFindParamsTry;

        // Конструктор класса
        public LoaderClass(string path)
        {
            this.path = path;
            this.LoadQuestion();
        }
            
        /// <summary>
        /// Загрузка вопроса
        /// </summary>
        public void LoadQuestion()
        {
            // Спиток известных переменных и их значений
            List<string> arr = new List<string>();
            // Ответы в виде формул на вопрос
            List<string> formula = new List<string>();
            // Текст вопроса
            string text = "";
            // Переменные которые надо найти
            List<string> Params = new List<string>();
            // Счетчик попыток для каждой неизвестной переменной
            List<int> counter = new List<int>();


            bool isDict = false;
            bool isText = false;
            bool isFormula = false;
            bool isFindParams = false;
            bool isId = false;
           
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line =  sr.ReadLine()) != null)
                {
                    if ("[/QuestionId]" == line)
                    {
                        isId = false;
                    }

                    if (isId)
                    {
                        arr.Add(line);

                    }

                    if ("[QuestionId]" == line)
                    {
                        isId = true;
                    }            
        
                    if ("[/QuestionParams]" == line)
                    {
                        isDict = false;
                    }

                    if (isDict)
                    {
                        arr.Add(line);

                    }

                    if ("[QuestionParams]" == line)
                    {
                        isDict = true;
                    }

                    if ("[/QuestionFormula]" == line)
                    {
                        isFormula = false;
                    }

                    if (isFormula)
                    {
                        formula.Add(line);
                    }

                    if ("[QuestionFormula]" == line)
                    {
                        isFormula = true;
                    }

                    if ("[/QuestionText]" == line)
                    {
                        isText = false;
                    }

                    if (isText)
                    {
                        text = line;
                    }

                    if ("[QuestionText]" == line)
                    {
                        isText = true;
                    }

                    if ("[/QuestionFind]" == line)
                    {

                        isFindParams = false;
                    }

                    if (isFindParams)
                    {
                        Params.Add(line);
                        counter.Add(0);
                    }

                    if ("[QuestionFind]" == line)
                    {
                        isFindParams = true;
                    }
                }
            }
            
            //Преобразует текст в файле из string в словарь <string,double>
            var dict = arr.Select((s) => new {s})
                .ToDictionary(x => x.s.Split(':')[0], x => double.Parse(x.s.Split(':')[1]));
            dictionary = dict;
            QuestionText = text;
            Formula = formula;
            QuestionFindParams = Params;
            QuestionFindParamsTry = counter;
        }
        
        /// <summary>
        /// Возвращает текст вопроса
        /// </summary>
        /// <returns></returns>
        public string returnQuestionText()
        {
            return QuestionText;
        }
        
        
        /// <summary>
        /// Возвращает переменные и их значения
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, double> returnVariables()
        {
            return dictionary;
        }
        
        /// <summary>
        /// Возвращает формулу вопроса
        /// </summary>
        /// <returns></returns>
        public List<string> returnFormula()
        {
            return Formula;
        }

        /// <summary>
        /// Возвращает переменные которые надо найти
        /// </summary>
        /// <returns></returns>
        public List<string> returnQuestionFindParams()
        {
            return QuestionFindParams;
        }

        /// <summary>
        /// Возвращает счетчик попыпот ответа на вопрос для каждой из неизвестный переменных
        /// </summary>
        /// <returns></returns>
        public List<int> returnQuestionParamsTry()
        {
            return QuestionFindParamsTry;
        }
    }
}
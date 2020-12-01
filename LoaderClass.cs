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
        //Путь к вопросу
        private string path;
        
        //Спиток известных переменных и их значений
        private Dictionary<string, double> dictionary;
        
        //Текст вопроса
        private string QuestionText;
        
        //ОТветы в виде формул на вопрос
        private List<string> Formula;
        
        //Переменные которые нажо найти
        private List<string> QuestionFindParams;
        
        //Конструктор класса
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
            //Спиток известных переменных и их значений
            List<string> arr = new List<string>();
            //ОТветы в виде формул на вопрос
            List<string> formula = new List<string>();
            //Текст вопроса
            string text = "";
            // Переменыне которые надо найти
            List<string> Params = new List<string>();
            bool isDict = false;
            bool isText = false;
            bool isFormula = false;
            bool isFindParams = false;
           
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line =  sr.ReadLine()) != null)
                {

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
    }
}
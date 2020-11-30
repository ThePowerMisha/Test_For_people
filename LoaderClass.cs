using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class LoaderClass
    {
        private string path;
        private Dictionary<string, int> dictionary;
        private string QuestionText;
        private List<string> Formula;

        public LoaderClass(string path)
        {
            this.path = path;
        }

        async void LoadQuestion()
        {
            List<string> arr = new List<string>();
            List<string> formula = new List<string>();
            string text = "";
            bool isDict = false;
            bool isText = false;
            bool isFormula = false;
           
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
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
                        isFormula = false;
                    }

                    if (isFormula)
                    {
                        text = line;
                    }

                    if ("[QuestionText]" == line)
                    {
                        isFormula = true;
                    }

                }
            }

            var dict = arr.Select((s) => new {s})
                .ToDictionary(x => x.s.Split(':')[0], x => int.Parse(x.s.Split(':')[1]));
            dictionary = dict;
            QuestionText = text;
            Formula = formula;

        }

        public string returnQuestionText()
        {
            return QuestionText;
        }

        public Dictionary<string, int> returnVariables()
        {
            return dictionary;
        }

        public List<string> returnFormula()
        {
            return Formula;
        }
    }
}
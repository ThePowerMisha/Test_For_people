using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
// original author - https://github.com/mariuszgromada/MathParser.org-mXparser
using org.mariuszgromada.math.mxparser;

namespace WpfApp1
{
    public class CheckAnswer
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public CheckAnswer() { }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="Variables">Словарь исходных данных</param>
        public CheckAnswer(Dictionary<string, double> Variables)
        {
            this.Variables = Variables;
        }

        /// <summary>
        /// Строка формулы, который вводит студент
        /// </summary>
        public string Formula { get; set; }


        /// <summary>
        /// Словарь, который хранит исходные данные задания
        /// Ключ - название переменной(string); Значение - значение переменной(int)
        /// </summary>
        public Dictionary<string, double> Variables { get; set; }


        public void AddVariables(Dictionary<string, double> variables)
        {
            if (variables.Count <=0)
            {
                return;
            }
            Variables = variables;
        }

        /// <summary>
        /// Паттерн арифметических операций ('+', '-', '*', '/', '(', ')'), необходимый для разделения формулы на массив
        /// </summary>
        private string pattern = @"(\+)|(-)|(\*)|(/)|(\))|(\()|(\^)";


        /// <summary>
        /// Функция, реализующая проверку введенной формулы на правильность написания скобок, а именно:
        ///  1) Если нарушена целостность структуры скобок (прим. "(x+y*(d"), то выводится ошибка.
        ///  2) Если скобка начинается с закрывающей (прим. ")x+y)"), то выводится ошибка.
        ///  3) Если все правильно, то обработка формулы идет дальше.
        /// </summary>
        /// <param name="Formula"></param>
        /// <returns>Булевая функция: true - если соблюдена правильность написания скобок;
        /// false - обнаружена ошибка</returns>
        private bool BracketCheck(string Formula)
        {
            bool checkStatus = true;
            int number = 0;

            for (int i = 0; i < Formula.Length; i++)
            {
                if (Formula[i] == '(')
                {
                    number++;
                }

                if (Formula[i] == ')' && number == 0)
                {
                    // Ошибка!
                    //"Скобка начинается с закрывающей";
                    return !checkStatus;
                }

                if (Formula[i] == ')' && number >= 1)
                {
                    number--;
                }
            }

            if (number == 0)
            {
                // Все ок!
                return checkStatus;
            }
            else
            {
                // Ошибка!
                //"Где то у тебя не хватило скобки...";
                return !checkStatus;
            }
        }
        /// <summary>
        /// Функция, реализующая проверку формулы на синтаксис, а именно нельзя вводить:
        ///  1) "+a", "a+"
        ///  2) "a++b", "a*-b", "a/c//b"
        ///  3) "a+(+b)", "(-)+a"
        ///  4) "a+()"
        ///  5) "a(b)", "a(b+c)" (искл "sqrt(a+b)")
        ///  6) "(b+)+a", "a+(b+c+)"
        ///  7) "(b+a)c", "(b)a"
        /// </summary>
        /// <param name="Formula"></param>
        /// <returns>Булевая функция: true - если соблюдена правильность синтаксиса;
        /// false - обнаружена ошибка</returns>
        private bool SyntaxСheck(string Formula)
        {
            // Булевая переменная: true - синтатических ошибок нет; false - есть
            bool checkStatus = true;

            // паттерн арифметический операций
            string patternOperation = @"(\+)|(-)|(\*)|(/)|(\^)";
            string patternOperationNoMinus = @"(\+)|(\*)|(/)|(\^)";

            // Если арифм. операция находится в начали или конце формулы, то вызывается ошибка
            // Примеры: "+a", "a+"
            // Исключение: "-a"
            if (Regex.IsMatch(Formula[0].ToString(), patternOperationNoMinus) || (Regex.IsMatch(Formula[Formula.Length - 1].ToString(), patternOperation)))
            {
                return !checkStatus;
            }

            // Поэлементно разбираем формулу
            for (int i = 0; i < Formula.Length; i++)
            {
                // Если арифм. операции находятся рядом друг с другом, то вызывается ошибка
                // Примеры: "a++b", "a*-b", "a/c//b"
                if (i > 0 && i < (Formula.Length - 1))
                {
                    if (Regex.IsMatch(Formula[i].ToString(), patternOperation))
                    {
                        if (Regex.IsMatch(Formula[i - 1].ToString(), patternOperation) || Regex.IsMatch(Formula[i + 1].ToString(), patternOperation))
                        {
                            return !checkStatus;
                        }
                    }
                }

                // Рассматриваем символ открывающей скобки '('
                if (i < (Formula.Length - 1))
                {
                    if (Formula[i] == '(')
                    {
                        // Если после открывающей скобки идет арифметическая операция, то вызывается ошибка
                        // Примеры: "a+(+b)", "(-)+a"
                        // Исключение: "a+(-b)"
                        if (Regex.IsMatch(Formula[i + 1].ToString(), patternOperationNoMinus))
                        {
                            return !checkStatus;
                        }

                        // Если после открывающей скобки идет сразу закрывающая, то вызывается ошибка
                        // Примеры: "a+()"
                        if (Formula[i + 1] == ')')
                        {
                            return !checkStatus;
                        }

                        // Если перед открывающей скобкой нет арифметической операции, то вызывается ошибка (Исключение функция квадратного корня sqrt)
                        // Примеры: "a(b)", "a(b+c)"
                        // Исключение: "sqrt(b+c)", "((a+b))"
                        if (i > 0)
                        {
                            if (!Regex.IsMatch(Formula[i - 1].ToString(), patternOperation))
                            {
                                if (Formula[i-1] != '(')
                                {
                                    if (i > 3)
                                    {
                                        if (Formula[i - 4] != 's' && Formula[i - 3] != 'q' && Formula[i - 2] != 'r' && Formula[i - 1] != 't')
                                        {
                                            return !checkStatus;
                                        }
                                    }
                                    else
                                    {
                                        return !checkStatus;
                                    }
                                }
                            }
                        }
                    }
                }

                // Рассматриваем символ закрывающей скобки ")"
                if (i > 0)
                {
                    if (Formula[i] == ')')
                    {
                        // Если перед закрыающей скобкой находится арифметическая операция, то вызывается ошибка
                        // Примеры: "(b+)+a", "a+(b+c+)"
                        if (Regex.IsMatch(Formula[i - 1].ToString(), patternOperation))
                        {
                            return !checkStatus;
                        }

                        // Если перед закрывающей скобкой находится открывающая, то вызывается ошибка
                        // Примеры: "a+()"
                        if (Formula[i - 1] == '(')
                        {
                            return !checkStatus;
                        }

                        // Если после открывающей скобки нет арифметических операций, то вызывается ошибка
                        // Примеры: "(b+a)c", "(b)a"
                        // Исключение: "((a+b))"
                        if (i < (Formula.Length - 1))
                        {
                            if (!Regex.IsMatch(Formula[i + 1].ToString(), patternOperation))
                            {
                                if (Formula[i+1] != ')')
                                {
                                    return !checkStatus;
                                }
                            }
                        }
                    }
                }
            }
            return checkStatus;
        }
        
        public String Check()
        {
            // Удаляем ненужные пробелы из строки и переводим все в нижний регистр
            Formula = Formula.Replace(" ", "").ToLower();

            // Проверка, пустая ли строка
            if (String.IsNullOrEmpty(Formula))
            {
                return "NullOrEmpty Error!";
            }

            // Проверяем на правильность написания операций
            if (!SyntaxСheck(Formula))
            {
                return "Syntax Error!";
            }

             // Проверка на правильность написания скобок
            if (!BracketCheck(Formula))
            {
                // дальше не идет, выводит что то другое!
                // Вывод текста с ошибкой
                return "Bracket Error!";
            }

            // Делим введенную формулу на элементы массива, используя паттерн арифметический операций
            string[] array = Regex.Split(Formula, pattern);

            // Булевая функция
            // true - если такой символ с формуле допустим
            // false - такого символ не определен
            bool variableArrayCheck;

            for (int i = 0; i < array.Length; i++)
            {
                variableArrayCheck = false;
                foreach (var element in Variables)
                {
                    // Если этот элемент не является названием исходной переменной
                    if (array[i] != element.Key)
                    {
                        // Если это целое или десячичное число
                        if (Regex.IsMatch(array[i], @"^\d+([\.]\d+)?$"))
                        {
                            variableArrayCheck = true;
                        }

                        // Если это операционное выражение
                        if (Regex.IsMatch(array[i], pattern))
                        {
                            variableArrayCheck = true;
                        }

                        // Если это выражения квадратного корня, косинус, синус, или пустая строка
                        if (array[i] == "sqrt" || array[i] == "")
                        {
                            variableArrayCheck = true;
                        }

                    }

                    // Если элемент массива является названием исходной переменной, то заменяем название переменной на ее значение
                    if (array[i] == element.Key)
                    {
                        array[i] = element.Value.ToString();
                        variableArrayCheck = true;
                    }

                    // Если значение переменной является десятичным числом с запятой, то меняем запятую на точку, чтобы не возникало ошибок при обработке
                    if (Regex.IsMatch(array[i], @"^\d+([(\.)|(\,)]\d+)?$"))
                    {
                        // Заменяем запятую на точку
                        array[i] = array[i].Replace(",", ".");
                        variableArrayCheck = true;
                    }
                }

                // Если такой элемент массива недопустим, то выводится ошибка
                if (!variableArrayCheck)
                {
                    return ("Введены неизвестные данные!");
                }

            }

            // Соединяем элементы массива обратно в строку
            Formula = string.Join(null, array);

            // Высчитываем готовый ответ с помощью пользвательской библиотеки "mXparser"
            Expression expFormula = new Expression(Formula);
            string calculatedFormula = expFormula.calculate().ToString();

            // Выводим ответ
            return calculatedFormula;
        }
    }
}
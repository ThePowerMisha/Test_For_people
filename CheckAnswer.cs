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
        /// Строка формулы, который вводит студент
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// Словарь, который хранит исходные данные задания
        /// Ключ - название переменной(string); Значение - значение переменной(int)
        /// </summary>
        public Dictionary<string, int> Variables { get; set; }

        /// <summary>
        /// Паттерн арифметических операций ('+', '-', '*', '/', '(', ')'), необходимый для разделения формулы на массив
        /// </summary>
        private string pattern = @"(\+)|(-)|(\*)|(/)|(\))|(\()";

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
                return checkStatus;
            }
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public CheckAnswer() { }
        public String Check()
        {
            // Тестовый словарь для проверки работоспособности алгоритма
            // Ключ - название переменной
            // Значение - значение перменной
            Dictionary<string, int> Variables = new Dictionary<string, int>
            {
                {"f1", 21},
                {"f2", 5},
                {"f3", 6},
                {"d", 7},
                {"h", 10}
            };

            // Перевод строки в нижний регистр
            Formula = Formula.ToLower();
            

            // Делим введенную формулу на элементы массива, используя паттерн арифметический операций
            string[] array = Regex.Split(Formula, pattern);

            // Проверка на правильность написания скобок
            if (!BracketCheck(Formula))
            {
                // дальше не идет, выводит что то другое!
                // Вывод текста с ошибкой
                return "Bracket Error!";
            }

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
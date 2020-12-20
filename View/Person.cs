using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.View {
    /// <summary>
    /// Класс персональных данных
    /// </summary>
    class Person {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Person() { }
        /// <summary>
        /// Конструктор класса персональных данных
        /// </summary>
        /// <param name="lastName">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="secondName">Отчество</param>
        /// <param name="group">Группа</param>
        /// <param name="data">Массив данных пройденного теста</param>
        public Person(string lastName, string firstName, string secondName, string group, List<PersonData> data) {
            this.lastName = lastName;
            this.firstName = firstName;
            this.secondName = secondName;
            this.group = group;
            this.data = data;
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string lastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string firstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string secondName { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        public string group { get; set; }

        /// <summary>
        /// Массив данных пройденного теста
        /// </summary>
        public List<PersonData> data{get; set;}
    }

    /// <summary>
    /// Класс массива данных пройденного теста
    /// </summary>
    class PersonData {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public PersonData() { }
        /// <summary>
        /// Конструктор класса массива данных пройденного теста
        /// </summary>
        /// <param name="theme">Тема</param>
        /// <param name="part">Часть</param>
        /// <param name="loads">Положение нагрузок</param>
        /// <param name="variant">Вариант</param>
        /// <param name="time">Время выполнения теста</param>
        /// <param name="tips">Подсказки</param>
        /// <param name="score">Количество баллов</param>
        /// <param name="answers">Кол-во правильных ответов</param>
        /// <param name="date">Дата прохождения</param>
        public PersonData(string theme, string part, string loads, string variant, string time, string tips, string score, string answers, string date) {
            this.theme = theme;
            this.part = part;
            this.loads = loads;
            this.variant = variant;
            this.time = time;
            this.tips = tips;
            this.score = score;
            this.answers = answers;
            this.date = date;
        }
        /// <summary>
        /// Тема
        /// </summary>
        public string theme { get; set; }

        /// <summary>
        /// Часть
        /// </summary>
        public string part { get; set; }

        /// <summary>
        /// Положение нагрузок
        /// </summary>
        public string loads { get; set; }

        /// <summary>
        /// Вариант
        /// </summary>
        public string variant { get; set; }

        /// <summary>
        /// Время выполнения теста
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// Подсказки
        /// </summary>
        public string tips { get; set; }

        /// <summary>
        /// Количество баллов
        /// </summary>
        public string score { get; set; }

        /// <summary>
        /// Кол-во правильных ответов
        /// </summary>
        public string answers { get; set; }

        /// <summary>
        /// Дата прохождения
        /// </summary>
        public string date { get; set; }
    }
}

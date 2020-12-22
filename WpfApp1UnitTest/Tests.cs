using System;
using System.Collections.Generic;
using NUnit.Framework;
using WpfApp1;

namespace WpfApp1UnitTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.True(true);
        }
        [Test]
        public void CheckAsnwer10and10_result20()
        {
            int result = 20;
            
            Dictionary<string, double> dictTest = new Dictionary<string, double>
            {
                {"f1", 5 },
                {"f2", 3 }
            };

            CheckAnswer check = new CheckAnswer(dictTest);


            check.Formula = "10 +       10";
            
            Assert.AreEqual(check.Check(), result.ToString());

        }

        [Test]
        public void UnknownData_Error()
        {
            string expected = "UnknownData Error!";
            
            Dictionary<string, double> dictTest = new Dictionary<string, double>
            {
                {"f1", 5 },
                {"f2", 3 }
            };

            CheckAnswer check = new CheckAnswer(dictTest);
            
            check.Formula = "HB";
            
            Assert.AreEqual(expected,check.Check());
        }
        [Test]
        public void CheckAsnwerF1andF2_resultmin2()
        {
            string expected = "-2";

            Dictionary<string, double> dictTest = new Dictionary<string, double>
            {
                {"f1", 5 },
                {"f2", 3 }
            };

            CheckAnswer check = new CheckAnswer(dictTest);

            check.Formula = "F2 -       F1";
            
            Assert.AreEqual(expected,check.Check());
        }

        // Проверка на синтаксическую ошибку
        [Test]
        public void CheckAsnwer_syntaxError()
        {
            string expected = "Syntax Error!";

            Dictionary<string, double> dictTest = new Dictionary<string, double>
            {
                {"f1", 5 },
                {"f2", 3 }
            };
            CheckAnswer check = new CheckAnswer(dictTest);

            check.Formula = "+ f2";

            Assert.AreEqual(expected, check.Check());
        }

        // Проверка на правильность написания скобок
        [Test]
        public void CheckAsnwer_bracketError()
        {
            string expected = "Bracket Error!";

            Dictionary<string, double> dictTest = new Dictionary<string, double>
            {
                {"f1", 5 },
                {"f2", 3 }
            };
            CheckAnswer check = new CheckAnswer(dictTest);

            check.Formula = "(f1+ f2)*f2)";

            Assert.AreEqual(expected, check.Check());
        }

        // Проверка на пустую строку
        [Test]
        public void CheckAsnwer_nullemptyError()
        {
            string expected = "NullOrEmpty Error!";

            Dictionary<string, double> dictTest = new Dictionary<string, double>
            {
                {"f1", 5 },
                {"f2", 3 }
            };
            CheckAnswer check = new CheckAnswer(dictTest);

            check.Formula = "";

            Assert.AreEqual(expected, check.Check());
        }
    }
}
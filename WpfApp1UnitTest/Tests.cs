using System;
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
            
            CheckAnswer check = new CheckAnswer();

            check.Formula = "10 +       10";
            
            Assert.AreEqual(check.Check(), result.ToString());

        }

        [Test]
        public void CheckAsnwerF1andF2_resultmin16()
        {
            string expected = "-16";
            
            CheckAnswer check = new CheckAnswer();

            check.Formula = "F2 -       F1";
            
            Assert.AreEqual(expected,check.Check());
        }
    }
}
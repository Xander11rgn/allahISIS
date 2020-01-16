using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace MyFirstNUnitTest
{
    [TestFixture]
    public class Test
    {
        private Form1 f;
        private INIManager ini;
        [SetUp]
        public void Setup()
        {
            f = new Form1();
            ini = new INIManager(Environment.CurrentDirectory + "\\my.ini");
        }

        [Test]
        public void testTest()
        {
            int a = 300;
            Assert.That(a == 250);
        }

        [Test]
        public void numTest()
        {
            Assert.That(f.test()==50);
        }

        [Test]
        public void fileLoadTest()
        {
            try
            {
                //f.fileLoad(Environment.CurrentDirectory + "//test.txt");
                //f.fileLoad(Environment.CurrentDirectory + "//warpeace.txt");
                f.fileLoad(Environment.CurrentDirectory + "//warpeacex3.txt");
            }
            catch
            {
                Assert.Fail("Тест загрузки файла не пройден");
            }
        }

        [Test]
        public void GetControlExceptionTest()
        {
            try
            {
                f.GetControlTest(f, "txt");
            }
            catch
            {
                Assert.Fail("Тест поиска элемента не пройден");
            }
        }

        [Test]
        public void GetControlTest()
        {
            Assert.AreEqual(false, f.GetControlTest(f, "txt"));
        }

        [Test]
        public void wordsExceptionTest()
        {
            try
            {
                f.words(f.fileLoad(Environment.CurrentDirectory + "//test.txt"));
            }
            catch
            {
                Assert.Fail("Тест разделения текста на слова не пройден");
            }
            
        }

        [Test]
        public void wordsTest()
        {
            Assert.NotNull(f.words(f.fileLoad(Environment.CurrentDirectory + "//warpeacex3.txt")));
        }

        [Test]
        public void wordsCountTest()
        {
            string[] words = f.words(f.fileLoad(Environment.CurrentDirectory + "//test.txt"));
            Assert.NotNull(f.wordsCount(words,3));
        }

        [Test]
        public void wordsCountResultTest()
        {
            string[] words = f.words(f.fileLoad(Environment.CurrentDirectory + "//test1.txt"));
            int[] result = f.wordsCount(words, 5);
            Assert.AreEqual(3,result[4]);
        }

        [Test]
        public void wordsCountExceptionTest()
        {
            try
            {
                string[] words = f.words(f.fileLoad(Environment.CurrentDirectory + "//warpeacex3.txt"));
                f.wordsCount(words, 3);
            }
            catch
            {
                Assert.Fail("Тест подсчета слов не пройден");
            }

        }
    }
}

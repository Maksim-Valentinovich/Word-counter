using Microsoft.VisualStudio.TestTools.UnitTesting;
using New_Structure;
using System;

namespace TestCheckText
{
    [TestClass]
    public class CountWordsTest
    {
        [TestMethod]
        public void Estimation_count_returned()
        {
            ///arrege
            string text = "asd asd ";

            int extendet = 2;

            ///act 
            
            Estimation estimation = new Estimation();

            int actual = estimation.CountWords(text);

            ///assert
            Assert.AreEqual(extendet, actual);
        }
    }
}

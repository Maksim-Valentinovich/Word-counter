using Microsoft.VisualStudio.TestTools.UnitTesting;
using New_Structure;
using System;

namespace TestCheckText
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidateText_text_bool_returned()
        {
            ///arrege
            string text = " ";

            bool extendet = false;

            ///act 

            ValidateText validate = new ValidateText();

            bool actual = validate.CheckSymbols(text);

            ///assert
            Assert.AreEqual(extendet, actual);
        }
    }
}

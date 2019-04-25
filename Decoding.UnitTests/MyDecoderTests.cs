using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Decoding.UnitTests
{
    [TestClass]
    public class MyDecoderTests
    {
        #region Clue

        [TestMethod]
        public void MethodName_Scenario_Behavior()
        {
            // Arrange

            // Act

            // Assert
        }

        #endregion

        #region Testing MyDecoder.Decode()

        [TestMethod]
        public void DecodeWithNoParams_StringToDecodeIsNullOrEmpty_ReturnsSpecifiedString()
        {
            var decoder1 = new MyDecoder(null);
            var decoder2 = new MyDecoder(string.Empty);
            var specifiedString = "The string is empty";
                        
            Assert.IsTrue( decoder1.Decode() == specifiedString && decoder2.Decode() == specifiedString);
        }

        [TestMethod]
        public void DecodeWithNoParams_StringToDecodeHasValue_ReturnsDecodedString()
        {
            var decoder = new MyDecoder("2[abc]d3[a]");
            var expected = "abcabcdaaa";

            var result = decoder.Decode();

            Assert.IsTrue( expected == result );
        }

        #endregion

        #region Testing MyDecoder.Decode(string value)

        [TestMethod]
        public void DecodeWith1Param_GivenStringIsNullOrEmpty_ReturnsSpecifiedString()
        {
            var decoder = new MyDecoder(string.Empty);
            var specifiedString = "The string is empty";
            

            Assert.IsTrue(decoder.Decode(null) == specifiedString && decoder.Decode(string.Empty) == specifiedString);
        }

        [TestMethod]
        public void DecodeWith1Param_GivenStringIsValidString_ReturnsDecodedString()
        {
            var decoder = new MyDecoder("");
            var givenString = "2[abc3[f]]";
            var expected = "abcfffabcfff";

            var result = decoder.Decode(givenString);

            Assert.IsTrue(expected == result);
        }

        #endregion

        #region Testing private MyDecoder.ValidityChecker(string value)

        [TestMethod]
        public void ValidityChecker_GivenStringIsValid_ReturnGivenString()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var validString1 = "abcdefg";
            var validString2 = "2[abc]";
            var validString3 = "3[assd2[ss]]sss";

            var result1 = privateObject.Invoke("ValidityChecker", validString1);
            var result2 = privateObject.Invoke("ValidityChecker", validString2);
            var result3 = privateObject.Invoke("ValidityChecker", validString3);

            Assert.IsTrue(validString1 == (string)result1 && validString2 == (string)result2 && validString3 == (string)result3);
        }

        [TestMethod]
        public void ValidityChecker_GivenStringHasInapropriateSymbol_ReturnsSpecifiedString()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var givenString = "2[aasd*]";
            var expected = "Unacceptable symbol is used:\nSymbol: \"*\", at: 6";

            var result = privateObject.Invoke("ValidityChecker", givenString);

            Assert.IsTrue((string)result == expected);
        }

        [TestMethod]
        public void ValidityChecker_GivenStringHasDigitsInTheWrongPlace_ReturnsSpecifiedString()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var givenString = "2[avc2s]";
            var expected = "Неправильное выражение";

            var result = privateObject.Invoke("ValidityChecker", givenString);

            Assert.IsTrue((string)result == expected);
        }

        [TestMethod]
        public void ValidityChecker_GivenStringHasLettersInTheWrongPlace_ReturnsSpecifiedString()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var givenString = "2ss[avcs]";
            var expected = "Неправильное выражение";

            var result = privateObject.Invoke("ValidityChecker", givenString);

            Assert.IsTrue((string)result == expected);
        }

        [TestMethod]
        public void ValidityChecker_GivenStringHasOnlyLettersInsideBrackets_ReturnsSpecifiedString()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var givenString = "[abcdefjklmno]";
            var expected = "Неправильное выражение";

            var result = privateObject.Invoke("ValidityChecker", givenString);

            Assert.IsTrue((string)result == expected);
        }

        [TestMethod]
        public void ValidityChecker_GivenStringHasOpeningBracketsRightBeforeClosingBrackets_ReturnsSpecifiedString()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var givenString1 = "[]";
            var givenString2 = "2[abc2[]]";
            var expected = "Неправильное выражение";

            var result1 = privateObject.Invoke("ValidityChecker", givenString1);
            var result2 = privateObject.Invoke("ValidityChecker", givenString2);

            Assert.IsTrue((string)result1 == expected && (string)result2 == expected);
        }

        [TestMethod]
        public void ValidityChecker_OpeningAndClosingBracketsAmountDontMatchInTheGivenString_ReturnsSpecifiedString()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var givenString = "2[xxx]]";
            var expected = "Число открывающих скобок не равно числу закрывающих";

            var result = privateObject.Invoke("ValidityChecker", givenString);

            Assert.IsTrue((string)result == expected);
        }

        #endregion

        #region Testing private MyDecoder.UnfoldString(string value)

        [TestMethod]
        public void StringUnfolder_GivenStringHasNoNumbers_ReturnsGivenString()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var givenString1 = "[avcs]";
            var givenString2 = "kkksssspppp";

            var result1 = privateObject.Invoke("StringUnfolder", givenString1);
            var result2 = privateObject.Invoke("StringUnfolder", givenString2);

            Assert.IsTrue((string)result1 == givenString1 && (string)result2 == givenString2);
        }

        [TestMethod]
        public void StringUnfolder_GivenStringIsValidMatch_ReturnsUnfoldedString()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var givenString = "2[avcs]aaa";
            var expected = "avcsavcsaaa";

            var result = privateObject.Invoke("StringUnfolder", givenString);

            Assert.IsTrue((string)result == expected);
        }

        #endregion

        #region Testing private MyDecoder.Repeat(string s, int n, string remainer)

        [TestMethod]
        public void Repeat_IsGivenStringNumberAndRemainer_ReturnsNumberOfTimesRepeatedStringPlusRemainer()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var givenString = "aaa";
            var number = 3;
            var remainer = "kkk";
            var expected = "aaaaaaaaakkk";

            var result = privateObject.Invoke("Repeat", givenString, number, remainer);

            Assert.IsTrue((string)result == expected);
        }

        #endregion

        #region Testing private MyDecoder.Replace(string inputString, string toReplace, int index, int length)

        [TestMethod]
        public void Replace_IsGivenParameters_ReturnsProcessedString()
        {
            var decoder = new MyDecoder("");
            PrivateObject privateObject = new PrivateObject(decoder);
            var currentInputStringCondition = "2[abc2[sas]sa]";
            var unfoldedMatchValue = "sassassa";
            var matchIndex = 5;
            var matchLength = 8;
            var expected = "2[abcsassassa]";

            var result = privateObject.Invoke("Replace", currentInputStringCondition, unfoldedMatchValue, matchIndex, matchLength);

            Assert.IsTrue((string)result == expected);
        }

        #endregion
    }
}

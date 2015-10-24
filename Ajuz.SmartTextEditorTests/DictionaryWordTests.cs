using Ajuz.SmartTextEditor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditorTests
{
    /// <summary>
    /// Модульные тесты для класса Ajuz.SmartTextEditor.DictionaryWord
    /// </summary>
    [TestClass]
    public class DictionaryWordTests
    {
        /// <summary>
        /// Тестовый метод:
        /// Если в конструктор объекта DictionaryWord параметру text передается
        /// значение null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Constructor_WhenTextIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string text = null;
            int frequency = 1;

            // act                     
            try
            {
                var dictionaryWord = new DictionaryWord(text, frequency);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("text", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод:
        /// Если в конструктор объекта DictionaryWord параметру text передается
        /// пустая строка, должно быть выброшено исключение ArgumentException.
        /// </summary>
        [TestMethod]
        public void Constructor_WhenTextIsEmpty_ShouldThrowArgumentException()
        {
            // arrange
            string text = string.Empty;
            int frequency = 1;

            // act                     
            try
            {
                var dictionaryWord = new DictionaryWord(text, frequency);
            }
            catch (ArgumentException e)
            {
                //assert
                Assert.AreEqual("text", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод:
        /// Если в конструктор объекта DictionaryWord параметру frequency передается
        /// значение меньше 1, должно быть выброшено исключение ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void Constructor_WhenFrequencyLess1_ShouldThrowArgumentOutOfRangeException()
        {
            // arrange
            string text = "a";
            int frequency = 0;

            // act                     
            try
            {
                var dictionaryWord = new DictionaryWord(text, frequency);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //assert
                Assert.AreEqual("frequency", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод:
        /// Конструктор объекта должен проинициализировать свойство Text значением
        /// параметра text.
        /// </summary>
        [TestMethod]
        public void Constructor_MustSetPropertyTextFromTextArgumentOfConstructor()
        {
            // arrange
            string text = "abcdefgh";
            int frequency = 1;

            // act                     
            var dictionaryWord = new DictionaryWord(text, frequency);

            //assert
            Assert.AreEqual(
                text,
                dictionaryWord.Text,
                false,
                "Значение свойства Text отличается от аргумента, переданного в конструктор");
        }

        /// <summary>
        /// Тестовый метод:
        /// Конструктор объекта должен проинициализировать свойство Frequency значением
        /// параметра frequency.
        /// </summary>
        [TestMethod]
        public void Constructor_MustSetPropertyFrequencyFromFrequencyArgumentOfConstructor()
        {
            // arrange
            string text = "abcdefgh";
            int frequency = 123456;

            // act                     
            var dictionaryWord = new DictionaryWord(text, frequency);

            //assert
            Assert.AreEqual(
                frequency,
                dictionaryWord.Frequency,
                "Значение свойства Frequency отличается от аргумента, переданного в конструктор");
        }

        /// <summary>
        /// Тестовый метод:
        /// Экземплярный метод ToString должен возвращать строку, содержащую
        /// значения свойств объекта: Text, Frequency.
        /// </summary>
        [TestMethod]
        public void ToString_ReturnStringWithValuesOfTextAndFrequencyProperties()
        {
            // arrange
            string text = "abcdefgh";
            int frequency = 123456;
            
            var dictionaryWord = new DictionaryWord(text, frequency);

            // act                     
            var result = dictionaryWord.ToString();

            //assert
            Assert.AreEqual(
                "(Text: 'abcdefgh'; Frequency: 123456)",
                result,
                false);
        }
    }
}

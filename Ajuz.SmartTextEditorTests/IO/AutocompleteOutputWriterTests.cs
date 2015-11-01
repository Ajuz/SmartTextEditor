using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Ajuz.SmartTextEditor.IO;
using System.Collections.Generic;
using Ajuz.SmartTextEditor;

namespace Ajuz.SmartTextEditorTests.IO
{
    /// <summary>
    /// Модульные тесты для класса Ajuz.SmartTextEditor.IO.AutocompleteOutputWriter
    /// </summary>
    [TestClass]
    public class AutocompleteOutputWriterTests
    {
        /// <summary>
        /// Тестовый метод: 
        /// Если в конструктор объекта AutocompleteOutputWriter параметру streamWriter 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Constructor_WhenStreamWriterIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            StreamWriter streamWriter = null;

            // act
            try
            {
                var autocompleteOutputWriter = new AutocompleteOutputWriter(streamWriter);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("streamWriter", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод Write объекта AutocompleteOutputWriter параметру 
        /// autocompleteCollection передается null, должно быть выброшено 
        /// исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Write_WhenAutocompleteCollectionIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            StreamWriter streamWriter = new StreamWriter(new MemoryStream());
            var autocompleteOutputWriter = new AutocompleteOutputWriter(streamWriter);

            IEnumerable<DictionaryWord> autocompleteCollection = null;

            // act
            try
            {
                autocompleteOutputWriter.Write(autocompleteCollection);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("autocompleteCollection", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод Write объекта AutocompleteOutputWriter параметру 
        /// autocompleteCollection передается пустая коллекция, 
        /// в поток должно быть записано два переноса строки.
        /// </summary>
        [TestMethod]
        public void Write_WhenAutocompleteCollectionIsEmpty_ShouldWriteStringWithTwoLineBreaks()
        {
            // arrange
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.AutoFlush = true;
            var autocompleteOutputWriter = new AutocompleteOutputWriter(streamWriter);

            IEnumerable<DictionaryWord> autocompleteCollection = new DictionaryWord[0];

            var expectedStreamContent = string.Format("{0}{0}", Environment.NewLine);
            
            // act
            autocompleteOutputWriter.Write(autocompleteCollection);
            
            //assert
            stream.Seek(0, SeekOrigin.Begin);
            var resultStreamReader = new StreamReader(stream);
            var actualStreamContent = resultStreamReader.ReadToEnd();
            
            Assert.AreEqual(
                expectedStreamContent,
                actualStreamContent,
                false);
        }

        /// <summary>
        /// Если в метод Write объекта AutocompleteOutputWriter параметру 
        /// autocompleteCollection передается НЕ пустая коллекция объектов 
        /// DictionaryWord, в поток должны быть записаны значения свойства 
        /// Text объектов DictionaryWord, разделенные переносами строки;
        /// в конце в поток вывода должно быть записано два переноса строки.
        /// </summary>
        [TestMethod]
        public void Write_WhenAutocompleteCollectionIsNotEmpty_ShouldWriteStringWithElementsSeparatedByLineBreaksWithTwoLineBreaksAtEnd()
        {
            // arrange
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.AutoFlush = true;
            var autocompleteOutputWriter = new AutocompleteOutputWriter(streamWriter);

            IEnumerable<DictionaryWord> autocompleteCollection = new DictionaryWord[] 
            {
                new DictionaryWord("element1", 1),
                new DictionaryWord("element2", 2),
                new DictionaryWord("element3", 3)
            };

            var expectedStreamContent = string.Format(
                "element1{0}element2{0}element3{0}{0}",
                Environment.NewLine);

            // act
            autocompleteOutputWriter.Write(autocompleteCollection);

            //assert
            stream.Seek(0, SeekOrigin.Begin);
            var resultStreamReader = new StreamReader(stream);
            var actualStreamContent = resultStreamReader.ReadToEnd();

            Assert.AreEqual(
                expectedStreamContent,
                actualStreamContent,
                false);
        }
    }
}

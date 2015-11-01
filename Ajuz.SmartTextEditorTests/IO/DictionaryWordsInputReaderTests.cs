using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using Ajuz.SmartTextEditor.IO;
using System.Collections.Generic;
using Ajuz.SmartTextEditor;
using System.Collections;

namespace Ajuz.SmartTextEditorTests.IO
{
    /// <summary>
    /// Модульные тесты класса Ajuz.SmartTextEditor.IO.DictionaryWordsInputReader
    /// </summary>
    [TestClass]
    public class DictionaryWordsInputReaderTests
    {
        /// <summary>
        /// Тестовый метод: 
        /// Если в конструктор объекта DictionaryWordsInputReader параметру streamReader 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Constructor_WhenStreamReaderIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            StreamReader streamReader = null;

            // act
            try
            {
                var dictionaryWordsInputReader =
                    new DictionaryWordsInputReader(streamReader);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("streamReader", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод:
        /// При вызове экземплярного метода Read в случае, если в первой строке 
        /// считываемого потока содержится не числовое значение - должно быть выброшено
        /// исключение FormatException
        /// </summary>
        [TestMethod]
        public void Read_WhenStreamFirstRowDataIsNotNumber_ShouldThrowFormatException()
        {
            // arrange
            var stream = new MemoryStream();

            var streamWriter = new StreamWriter(stream);
            streamWriter.Write("some string");
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            var streamReader = new StreamReader(stream);
            var dictionaryWordsInputReader =
                new DictionaryWordsInputReader(streamReader);

            // act
            try
            {
                dictionaryWordsInputReader.Read();
            }
            catch (FormatException)
            {
                //assert
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод:
        /// При вызове экземплярного метода Read в случае, если в первой строке 
        /// считываемого потока содержится число меньше 0 - должно быть выброшено
        /// исключение InvalidOperationException
        /// </summary>
        [TestMethod]
        public void Read_WhenNumberInStreamFirstRowLessThan0_ShouldThrowInvalidOperationException()
        {
            // arrange
            var stream = new MemoryStream();

            var streamWriter = new StreamWriter(stream);
            streamWriter.Write(-1);
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            var streamReader = new StreamReader(stream);
            var dictionaryWordsInputReader =
                new DictionaryWordsInputReader(streamReader);

            // act
            try
            {
                dictionaryWordsInputReader.Read();
            }
            catch (InvalidOperationException)
            {
                //assert
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод:
        /// При вызове экземплярного метода Read в случае, если строка 
        /// (начиная со 2-ой) читаемого потока содержит данные в некорректном формате,
        /// должно быть выброшено исключение FormatException.
        /// </summary>
        [TestMethod]
        public void Read_WhenStreamFromSecondRowDataHasInvalidFormat_ShouldThrowFormatException()
        {
            // arrange
            var stream = new MemoryStream();

            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(2);
            streamWriter.WriteLine("Element1 10");  // корректный формат
            streamWriter.WriteLine("Element2");     // некорректный формат
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            var streamReader = new StreamReader(stream);
            var dictionaryWordsInputReader =
                new DictionaryWordsInputReader(streamReader);

            // act
            try
            {
                dictionaryWordsInputReader.Read();
            }
            catch (FormatException)
            {
                //assert
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод:
        /// Экземплярный метод Read возвращает коллекцию объектов, количество элементов
        /// которой равено числу из первой строки читаемого потока.
        /// </summary>
        [TestMethod]
        public void Read_ReturnCollectionWithSizeEqualNumberFromStreamFirstRowData()
        {
            // arrange
            var stream = new MemoryStream();

            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(3);
            streamWriter.WriteLine("element1 10");
            streamWriter.WriteLine("element2 20");
            streamWriter.WriteLine("element3 30");
            streamWriter.WriteLine("element4 40");
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            var expectedResultCollectionSize = 3;

            var streamReader = new StreamReader(stream);
            var dictionaryWordsInputReader =
                new DictionaryWordsInputReader(streamReader);

            // act
            var actualResultCollection = dictionaryWordsInputReader.Read();

            //assert
            var actualResultArray = actualResultCollection.ToArray();
            Assert.AreEqual(expectedResultCollectionSize, actualResultArray.Length);
        }

        /// <summary>
        /// Тестовый метод:
        /// Экземплярный метод Read возвращает коллекцию объектов DictionaryWord, 
        /// каждый элемент которой содержит данные строки читаемого потока 
        /// (за исключением первой строки).
        /// </summary>
        [TestMethod]
        public void Read_ReturnCollectionOfDictionaryWordObjectsWithDataFromStreamRowsSkipFirst()
        {
            // arrange
            var stream = new MemoryStream();

            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(3);
            streamWriter.WriteLine("element1 10");
            streamWriter.WriteLine("element2 20");
            streamWriter.WriteLine("element3 30");
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            var expectedResultArray = new DictionaryWord[]
            {
                new DictionaryWord("element1", 10),
                new DictionaryWord("element2", 20),
                new DictionaryWord("element3", 30)
            };

            var streamReader = new StreamReader(stream);
            var dictionaryWordsInputReader =
                new DictionaryWordsInputReader(streamReader);

            // act
            var actualResultCollection = dictionaryWordsInputReader.Read();

            //assert
            var actualResultArray = actualResultCollection.ToArray();
            CollectionAssert.AreEqual(
                expectedResultArray, 
                actualResultArray,
                new DictionaryWordComparer());
        }

        /// <summary>
        /// Класс, обеспечивающий сравнение двух объектов DictionaryWord
        /// </summary>
        private class DictionaryWordComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null || y == null)
                {
                    return x == null && y == null ? 0
                        : x == null && y != null ? -1
                        : 1;
                }

                var dictionaryWordX = x as DictionaryWord;
                var dictionaryWordY = y as DictionaryWord;

                if (dictionaryWordX == null || dictionaryWordY == null)
                {
                    throw new InvalidOperationException("Некорректный тип аргументов");
                }

                if (dictionaryWordX.Text == dictionaryWordY.Text &&
                   dictionaryWordX.Frequency == dictionaryWordY.Frequency)
                {
                    return 0;
                }

                return string.CompareOrdinal(dictionaryWordX.Text, dictionaryWordY.Text);
            }
        }
    }
}

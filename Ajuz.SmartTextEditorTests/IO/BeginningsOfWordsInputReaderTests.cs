using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using Ajuz.SmartTextEditor.IO;
using System.Collections.Generic;
using Ajuz.SmartTextEditor;

namespace Ajuz.SmartTextEditorTests.IO
{
    /// <summary>
    /// Модульные тесты для класса Ajuz.SmartTextEditor.IO.BeginningsOfWordsInputReader
    /// </summary>
    [TestClass]
    public class BeginningsOfWordsInputReaderTests
    {
        /// <summary>
        /// Тестовый метод: 
        /// Если в конструктор объекта BeginningsOfWordsInputReader параметру streamReader 
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
                var beginningsOfWordsInputReader = 
                    new BeginningsOfWordsInputReader(streamReader);
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
            var beginningsOfWordsInputReader =
                new BeginningsOfWordsInputReader(streamReader);

            // act
            try
            {
                beginningsOfWordsInputReader.Read();
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
            var beginningsOfWordsInputReader = 
                new BeginningsOfWordsInputReader(streamReader);

            // act
            try
            {
                beginningsOfWordsInputReader.Read();
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
        /// Экземплярный метод Read возвращает коллекцию объектов, размер которой
        /// равен числу из первой строки читаемого потока.
        /// </summary>
        [TestMethod]
        public void Read_ReturnCollectionWithSizeEqualNumberFromStreamFirstRowData()
        {
            // arrange
            var stream = new MemoryStream();

            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(3);
            streamWriter.WriteLine("element1");
            streamWriter.WriteLine("element2");
            streamWriter.WriteLine("element3");
            streamWriter.WriteLine("element4");
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            var expectedResultCollectionSize = 3;

            var streamReader = new StreamReader(stream);
            var beginningsOfWordsInputReader =
                new BeginningsOfWordsInputReader(streamReader);

            // act
            var actualResultCollection = beginningsOfWordsInputReader.Read();

            //assert
            var actualResultArray = actualResultCollection.ToArray();
            Assert.AreEqual(expectedResultCollectionSize, actualResultArray.Length);
        }

        /// <summary>
        /// Тестовый метод:
        /// Экземплярный метод Read возвращает коллекцию строк, каждый элемент которой
        /// содержит данные строки читаемого потока (за исключением первой строки).
        /// </summary>
        [TestMethod]
        public void Read_ReturnCollectionOfStringsWithDataFromStreamRowsSkipFirst()
        {
            // arrange
            var stream = new MemoryStream();

            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(3);
            streamWriter.WriteLine("element1");
            streamWriter.WriteLine("element2");
            streamWriter.WriteLine("element3");
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            var expectedResultArray = new string[]
            {
                "element1", "element2", "element3"
            };

            var streamReader = new StreamReader(stream);
            var beginningsOfWordsInputReader =
                new BeginningsOfWordsInputReader(streamReader);

            // act
            var actualResultCollection = beginningsOfWordsInputReader.Read();
            
            //assert
            var actualResultArray = actualResultCollection.ToArray();
            CollectionAssert.AreEqual(expectedResultArray, actualResultArray);
        }
    }
}

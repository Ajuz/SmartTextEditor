using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ajuz.SmartTextEditor.IO;
using System.Collections.Generic;
using Ajuz.SmartTextEditor.Standalone;
using System.Collections;

namespace Ajuz.SmartTextEditor.StandaloneTests
{
    /// <summary>
    /// Модульные тесты для класса Ajuz.SmartTextEditor.Standalone.AutocompleteApplication
    /// </summary>
    [TestClass]
    public class AutocompleteApplicationTests
    {
        /// <summary>
        /// Тестовый метод: 
        /// Если в конструктор объекта AutocompleteApplication параметру 
        /// dictionaryWordsInputReader передается null, должно быть 
        /// выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Constructor_WhenDictionaryWordsInputReaderIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            IInputReader<DictionaryWord> dictionaryWordsInputReader = null;
            IInputReader<string> beginningsOfWordsInputReader = 
                new BeginningsOfWordsInputReaderMock();
            IOutputWriter<DictionaryWord> autocompleteOutputWriter = 
                new AutocompleteOutputWriterMock();

            // act
            try
            {
                var autocompleteApplication =
                    new AutocompleteApplication(
                        dictionaryWordsInputReader,
                        beginningsOfWordsInputReader,
                        autocompleteOutputWriter);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("dictionaryWordsInputReader", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в конструктор объекта AutocompleteApplication параметру 
        /// beginningsOfWordsInputReader передается null, должно быть 
        /// выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Constructor_WhenBeginningsOfWordsInputReaderIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            IInputReader<DictionaryWord> dictionaryWordsInputReader = 
                new DictionaryWordsInputReaderMock();
            IInputReader<string> beginningsOfWordsInputReader = null;
            IOutputWriter<DictionaryWord> autocompleteOutputWriter =
                new AutocompleteOutputWriterMock();

            // act
            try
            {
                var autocompleteApplication =
                    new AutocompleteApplication(
                        dictionaryWordsInputReader,
                        beginningsOfWordsInputReader,
                        autocompleteOutputWriter);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("beginningsOfWordsInputReader", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в конструктор объекта AutocompleteApplication параметру 
        /// autocompleteOutputWriter передается null, должно быть 
        /// выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Constructor_WhenAutocompleteOutputWriterIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            IInputReader<DictionaryWord> dictionaryWordsInputReader =
                new DictionaryWordsInputReaderMock();
            IInputReader<string> beginningsOfWordsInputReader = 
                new BeginningsOfWordsInputReaderMock();
            IOutputWriter<DictionaryWord> autocompleteOutputWriter = null;

            // act
            try
            {
                var autocompleteApplication =
                    new AutocompleteApplication(
                        dictionaryWordsInputReader,
                        beginningsOfWordsInputReader,
                        autocompleteOutputWriter);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("autocompleteOutputWriter", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// SearchAutocomplete не должен записывать в выходные данные автодополнения,
        /// если начальные части слов отсутсвуют во входящих данных.
        /// </summary>
        [TestMethod]
        public void SearchAutocomplete_WhenEmptyInputBeginningOfWord_ShouldNoDataWrittenToOutput()
        {
            // arrange
            var dictionaryWordsInputReader =
                new DictionaryWordsInputReaderMock(
                    new List<DictionaryWord>
                    {
                        new DictionaryWord("kare", 10),
                        new DictionaryWord("kanojo", 20),
                        new DictionaryWord("karetachi", 1),
                        new DictionaryWord("korosu", 7),
                        new DictionaryWord("sakura", 3)
                    });
            var beginningsOfWordsInputReader =
                new BeginningsOfWordsInputReaderMock();
            var autocompleteOutputWriter = 
                new AutocompleteOutputWriterMock();

            var autocompleteApplication =
                    new AutocompleteApplication(
                        dictionaryWordsInputReader,
                        beginningsOfWordsInputReader,
                        autocompleteOutputWriter);

            var expectedOutputWriteCalls = 0;

            // act
            autocompleteApplication.SearchAutocomplete();

            //assert
            Assert.AreEqual(
                expectedOutputWriteCalls, 
                autocompleteOutputWriter.WriteCallCount);
        }

        /// <summary>
        /// Тестовый метод: 
        /// SearchAutocomplete должен осуществлять запись в выходные данные автодополнений для 
        /// каждого из заданных начальных частей слов даже в том случае, если 
        /// автодополнения не найдены. В этом случае записываемые автодополнения - пустые массивы.
        /// </summary>
        [TestMethod]
        public void SearchAutocomplete_WhenAutocompleteNotFound_ShouldCalledMethodWriteAndNoDataWrittenToOutput()
        {
            // arrange
            var dictionaryWordsInputReader =
                new DictionaryWordsInputReaderMock(
                    new List<DictionaryWord>()
                    {
                        new DictionaryWord("a", 1),
                        new DictionaryWord("b", 2),
                        new DictionaryWord("c", 3)
                    });
            var beginningsOfWordsInputReader =
                new BeginningsOfWordsInputReaderMock(new List<string> { "k", "ka", "kar" });
            var autocompleteOutputWriter =
                new AutocompleteOutputWriterMock();

            var autocompleteApplication =
                    new AutocompleteApplication(
                        dictionaryWordsInputReader,
                        beginningsOfWordsInputReader,
                        autocompleteOutputWriter);

            var expectedOutputWriteCalls = 3;
            var expectedOutputCollection = new DictionaryWord[0];

            // act
            autocompleteApplication.SearchAutocomplete();

            //assert
            Assert.AreEqual(
                expectedOutputWriteCalls,
                autocompleteOutputWriter.WriteCallCount);

            CollectionAssert.AreEqual(
                expectedOutputCollection,
                autocompleteOutputWriter.OutputCollection);
        }

        /// <summary>
        /// Тестовый метод: 
        /// SearchAutocomplete должен осуществлять запись в выходные данные найденных
        /// автодополнений для каждого из начальных частей слов с соблюдением порядка.
        /// </summary>
        [TestMethod]
        public void SearchAutocomplete_WhenAutocompleteFound_AutocompleteMustBeWrittenToOutputForEachBeginningOfWord()
        {
            // arrange
            var dictionaryWordsInputReader =
                new DictionaryWordsInputReaderMock(
                    new List<DictionaryWord>()
                    {
                        new DictionaryWord("kare", 10),
                        new DictionaryWord("kanojo", 20),
                        new DictionaryWord("karetachi", 1),
                        new DictionaryWord("korosu", 7),
                        new DictionaryWord("sakura", 3)
                    });
            var beginningsOfWordsInputReader =
                new BeginningsOfWordsInputReaderMock(new List<string> { "k", "ka", "kar" });
            var autocompleteOutputWriter =
                new AutocompleteOutputWriterMock();

            var autocompleteApplication =
                    new AutocompleteApplication(
                        dictionaryWordsInputReader,
                        beginningsOfWordsInputReader,
                        autocompleteOutputWriter);

            var expectedOutputWriteCalls = 3;
            var expectedOutputCollection = new List<DictionaryWord>()
            {
                new DictionaryWord("kanojo", 20),   
                new DictionaryWord("kare", 10),
                new DictionaryWord("korosu", 7),
                new DictionaryWord("karetachi", 1),

                new DictionaryWord("kanojo", 20),
                new DictionaryWord("kare", 10),
                new DictionaryWord("karetachi", 1),

                new DictionaryWord("kare", 10),
                new DictionaryWord("karetachi", 1)
            };

            // act
            autocompleteApplication.SearchAutocomplete();

            //assert
            Assert.AreEqual(
                expectedOutputWriteCalls,
                autocompleteOutputWriter.WriteCallCount);

            CollectionAssert.AreEqual(
                expectedOutputCollection,
                autocompleteOutputWriter.OutputCollection,
                new DictionaryWordComparer());
        }

        /// <summary>
        /// Объект-заглушка DictionaryWordsInputReader для тестирования AutocompleteApplication
        /// </summary>
        private class DictionaryWordsInputReaderMock : IInputReader<DictionaryWord>
        {
            private IEnumerable<DictionaryWord> _inputCollection;

            public DictionaryWordsInputReaderMock(IEnumerable<DictionaryWord> inputCollection = null)
            {
                _inputCollection = inputCollection ?? new List<DictionaryWord>();
            }

            public IEnumerable<DictionaryWord> Read()
            {
                return _inputCollection;
            }
        }


        /// <summary>
        /// Объект-заглушка BeginningsOfWordsInputReader для тестирования AutocompleteApplication
        /// </summary>
        private class BeginningsOfWordsInputReaderMock : IInputReader<string>
        {
            private IEnumerable<string> _inputCollection;

            public BeginningsOfWordsInputReaderMock(IEnumerable<string> inputCollection = null)
            {
                _inputCollection = inputCollection ?? new List<string>();
            }

            public IEnumerable<string> Read()
            {
                return _inputCollection;
            }
        }


        /// <summary>
        /// Объект-заглушка AutocompleteOutputWriterMock для тестирования AutocompleteApplication
        /// </summary>
        private class AutocompleteOutputWriterMock : IOutputWriter<DictionaryWord>
        {
            public AutocompleteOutputWriterMock ()
	        {
                OutputCollection = new List<DictionaryWord>();
                WriteCallCount = 0;
	        }
            
            public List<DictionaryWord> OutputCollection { get; private set; }
            public int WriteCallCount { get; private set; }

            public void Write(IEnumerable<DictionaryWord> collection)
            {
                WriteCallCount++;
                OutputCollection.AddRange(collection);
            }
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

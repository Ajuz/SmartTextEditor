using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ajuz.SmartTextEditor;

namespace Ajuz.SmartTextEditorTests
{
    /// <summary>
    /// Модульные тесты для класса Ajuz.SmartTextEditor.AutocompleteSearcher
    /// </summary>
    [TestClass]
    public class AutocompleteSearcherTests
    {
        /// <summary>
        /// Тестовый метод: 
        /// Если в конструктор объекта AutocompleteSearcher параметру dictionaryWords 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Constructor_WhenDictionaryWordsIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            IEnumerable<DictionaryWord> dictionaryWords = null;

            // act                     
            try
            {
                var autocompleteSearcher = new AutocompleteSearcher(dictionaryWords);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("dictionaryWords", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в экземплярный метод FindWordsBeginingWith параметру beginOfWords 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void FindWordsBeginingWith_WhenBeginOfWordsIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>();

            var autocompleteSearcher = new AutocompleteSearcher(dictionaryWords);

            // act
            try
            {
                autocompleteSearcher.FindWordsBeginingWith(null);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("beginOfWords", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в экземплярный метод FindWordsBeginingWith параметру beginOfWords 
        /// передается пустая строка, должно быть выброшено исключение ArgumentException.
        /// </summary>
        [TestMethod]
        public void FindWordsBeginingWith_WhenBeginOfWordsIsEmpty_ShouldThrowArgumentException()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>();

            var autocompleteSearcher = new AutocompleteSearcher(dictionaryWords);

            // act
            try
            {
                autocompleteSearcher.FindWordsBeginingWith(string.Empty);
            }
            catch (ArgumentException e)
            {
                //assert
                Assert.AreEqual("beginOfWords", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в экземплярный метод FindWordsBeginingWith параметру maxCount 
        /// передается значение меньше 1, должно быть выброшено исключение 
        /// ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void FindWordsBeginingWith_WhenMaxCountLess1_ShouldThrowArgumentOutOfRangeException()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>();
            var beginOfWords = "aa";

            var autocompleteSearcher = new AutocompleteSearcher(dictionaryWords);

            // act
            try
            {
                autocompleteSearcher.FindWordsBeginingWith(beginOfWords, 0);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert
                Assert.AreEqual("maxCount", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Набор объектов, возвращаемый экземплярным методом FindWordsBeginingWith, 
        /// должен являться подмножеством набора объектов dictionaryWords, 
        /// переданного параметром конструктору AutocompleteSearcher.
        /// </summary>
        [TestMethod]
        public void FindWordsBeginingWith_ReturnSubsetOfDictionaryWordsArgumentOfConstructor()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>()
            {
                new DictionaryWord("aabbbccc", 1),
                new DictionaryWord("bbbaaaddd", 10),
                new DictionaryWord("aaaabbbccc", 100)
            };

            var beginOfWords = "aa";

            var autocompleteSearcher = new AutocompleteSearcher(dictionaryWords);

            // act
            IEnumerable<DictionaryWord> result =
                autocompleteSearcher.FindWordsBeginingWith(beginOfWords);

            // assert
            foreach (var item in result)
            {
                Assert.IsTrue(
                    dictionaryWords.Contains(item),
                    string.Format("Элемент {0} не входит в словарь", item));
            }
        }

        /// <summary>
        /// Тестовый метод: 
        /// Слова, возвращаемые экземплярным методом FindWordsBeginingWith, 
        /// должны в своем начале содержать подстроку, переданную в параметр
        /// beginOfWords.
        /// </summary>
        [TestMethod]
        public void FindWordsBeginingWith_ReturnWordsWithSameBegining()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>()
            {
                new DictionaryWord("aabbbccc", 1),
                new DictionaryWord("bbbaaaddd", 10),
                new DictionaryWord("aaaabbbccc", 100),
                new DictionaryWord("caabbbccc", 1000)
            };
            
            var beginOfWords = "aa";
            
            var autocompleteSearcher = new AutocompleteSearcher(dictionaryWords);

            // act
            IEnumerable<DictionaryWord> result = 
                autocompleteSearcher.FindWordsBeginingWith(beginOfWords);

            // assert
            foreach (var item in result)
            {
                Assert.IsTrue(
                    item.Text.StartsWith(
                        beginOfWords,
                        StringComparison.Ordinal),
                    "Результат содержит слово '{0}', его начало не совпадает с '{1}'",
                    item.Text,
                    beginOfWords);
            }
        }

        /// <summary>
        /// Тестовый метод: 
        /// Количество слов, возвращаемых экземплярным методом FindWordsBeginingWith, 
        /// должно быть ограничено числом, переданным в параметр maxCount.
        /// </summary>
        [TestMethod]
        public void FindWordsBeginingWith_ReturnWordsCountLimitedByMaxCountArgument()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>()
            {
                new DictionaryWord("aabbbccc", 1),
                new DictionaryWord("aaaabbbccc", 10),
                new DictionaryWord("aaabbbccc", 100),
                new DictionaryWord("aaaabbbccc", 1000)
            };

            var beginOfWords = "aa";
            var wordsMaxCount = 2;

            var autocompleteSearcher = new AutocompleteSearcher(dictionaryWords);

            // act
            IEnumerable<DictionaryWord> result =
                autocompleteSearcher.FindWordsBeginingWith(beginOfWords, wordsMaxCount);

            // assert
            Assert.IsTrue(
                result.Count() <= wordsMaxCount,
                "Результат содержит слишком много элементов");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Набор слов, возвращаемый экземплярным методом FindWordsBeginingWith, 
        /// должнен быть отсортирован по частоте слова по возрастанию,
        /// а при совпадении частоты - по алфавиту по убыванию.
        /// </summary>
        [TestMethod]
        public void FindWordsBeginingWith_ReturnWordsOrderedByFrequencyDescendingAndTextAscending()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>()
            {
                new DictionaryWord("aabbbccc", 10),
                new DictionaryWord("aaaabbbccc", 1),
                new DictionaryWord("aaabbbccc", 1000),
                new DictionaryWord("aaaabbbccc", 1000)
            };

            var beginOfWords = "aa";

            var autocompleteSearcher = new AutocompleteSearcher(dictionaryWords);

            // act
            IEnumerable<DictionaryWord> result =
                autocompleteSearcher.FindWordsBeginingWith(beginOfWords);

            // assert
            DictionaryWord prevItem = null;

            foreach (var item in result)
            {
                if (prevItem != null)
                {
                    Assert.IsTrue(
                        item.Frequency <= prevItem.Frequency, 
                        "Нет сортировки по убыванию частоты слова");

                    if (prevItem.Frequency == item.Frequency)
                    {
                        Assert.IsTrue(
                            string.CompareOrdinal(item.Text, prevItem.Text) >= 0,
                            "Нет сортировки по алфавиту");
                    }
                }

                prevItem = item;
            }
        }
    }
}

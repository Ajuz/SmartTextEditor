using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ajuz.SmartTextEditor;

namespace Ajuz.SmartTextEditorTests
{
    [TestClass]
    public class FastAutocompleteSearcherTests
    {
        /// <summary>
        /// Тестовый метод: 
        /// Если в конструктор объекта FastAutocompleteSearcher параметру dictionaryWords 
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
                var fastAutocompleteSearcher = 
                    new FastAutocompleteSearcher(dictionaryWords);
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
        /// Если в экземплярный метод FindWordsStartsWith параметру value 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void FindWordsStartsWith_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>();

            var fastAutocompleteSearcher = 
                new FastAutocompleteSearcher(dictionaryWords);

            // act
            try
            {
                fastAutocompleteSearcher.FindWordsStartsWith(null);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("value", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в экземплярный метод FindWordsStartsWith параметру value 
        /// передается пустая строка, должно быть выброшено исключение ArgumentException.
        /// </summary>
        [TestMethod]
        public void FindWordsStartsWith_WhenValueIsEmpty_ShouldThrowArgumentException()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>();

            var fastAutocompleteSearcher = new FastAutocompleteSearcher(dictionaryWords);

            // act
            try
            {
                fastAutocompleteSearcher.FindWordsStartsWith(string.Empty);
            }
            catch (ArgumentException e)
            {
                //assert
                Assert.AreEqual("value", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в экземплярный метод FindWordsStartsWith параметру maxCount 
        /// передается значение меньше 1, должно быть выброшено исключение 
        /// ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void FindWordsStartsWith_WhenMaxCountLess1_ShouldThrowArgumentOutOfRangeException()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>();
            var value = "aa";

            var fastAutocompleteSearcher = new FastAutocompleteSearcher(dictionaryWords);

            // act
            try
            {
                fastAutocompleteSearcher.FindWordsStartsWith(value, 0);
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
        /// Набор объектов, возвращаемый экземплярным методом FindWordsStartsWith, 
        /// должен являться подмножеством набора объектов dictionaryWords, 
        /// переданного параметром конструктору FastAutocompleteSearcher.
        /// </summary>
        [TestMethod]
        public void FindWordsStartsWith_ReturnSubsetOfDictionaryWordsArgumentOfConstructor()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>()
            {
                new DictionaryWord("aabbbccc", 1),
                new DictionaryWord("bbbaaaddd", 10),
                new DictionaryWord("aaaabbbccc", 100)
            };

            var value = "aa";

            var fastAutocompleteSearcher = new FastAutocompleteSearcher(dictionaryWords);

            // act
            IEnumerable<DictionaryWord> result =
                fastAutocompleteSearcher.FindWordsStartsWith(value);

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
        /// Слова, возвращаемые экземплярным методом FindWordsStartsWith, 
        /// должны в своем начале содержать подстроку, переданную в параметр
        /// value.
        /// </summary>
        [TestMethod]
        public void FindWordsStartsWith_ReturnWordsWithSameBeginning()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>()
            {
                new DictionaryWord("aabbbccc", 1),
                new DictionaryWord("bbbaaaddd", 10),
                new DictionaryWord("aaaabbbccc", 100),
                new DictionaryWord("caabbbccc", 1000)
            };

            var value = "aa";

            var fastAutocompleteSearcher = new FastAutocompleteSearcher(dictionaryWords);

            // act
            IEnumerable<DictionaryWord> result =
                fastAutocompleteSearcher.FindWordsStartsWith(value);

            // assert
            foreach (var item in result)
            {
                Assert.IsTrue(
                    item.Text.StartsWith(
                        value,
                        StringComparison.Ordinal),
                    "Результат содержит слово '{0}', его начало не совпадает с '{1}'",
                    item.Text,
                    value);
            }
        }

        /// <summary>
        /// Тестовый метод: 
        /// Количество слов, возвращаемых экземплярным методом FindWordsStartsWith, 
        /// должно быть ограничено числом, переданным в параметр maxCount.
        /// </summary>
        [TestMethod]
        public void FindWordsStartsWith_ReturnWordsCountLimitedByMaxCountArgument()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>()
            {
                new DictionaryWord("aabbbccc", 1),
                new DictionaryWord("aaaabbbccc", 10),
                new DictionaryWord("aaabbbccc", 100),
                new DictionaryWord("aaaabbbccc", 1000)
            };

            var value = "aa";
            var wordsMaxCount = 2;

            var fastAutocompleteSearcher = new FastAutocompleteSearcher(dictionaryWords);

            // act
            IEnumerable<DictionaryWord> result =
                fastAutocompleteSearcher.FindWordsStartsWith(value, wordsMaxCount);

            // assert
            Assert.IsTrue(
                result.Count() <= wordsMaxCount,
                "Результат содержит слишком много элементов");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Набор слов, возвращаемый экземплярным методом FindWordsStartsWith, 
        /// должнен быть отсортирован по частоте слова по возрастанию,
        /// а при совпадении частоты - по алфавиту по убыванию.
        /// </summary>
        [TestMethod]
        public void FindWordsStartsWith_ReturnWordsOrderedByFrequencyDescendingAndTextAscending()
        {
            // arrange
            var dictionaryWords = new List<DictionaryWord>()
            {
                new DictionaryWord("aabbbccc", 10),
                new DictionaryWord("aaaabbbccc", 1),
                new DictionaryWord("aaabbbccc", 1000),
                new DictionaryWord("aaaabbbccc", 1000)
            };

            var value = "aa";

            var fastAutocompleteSearcher = new FastAutocompleteSearcher(dictionaryWords);

            // act
            IEnumerable<DictionaryWord> result =
                fastAutocompleteSearcher.FindWordsStartsWith(value);

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

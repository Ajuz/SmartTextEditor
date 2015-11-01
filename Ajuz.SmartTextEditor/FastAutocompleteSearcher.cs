using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ajuz.SmartTextEditor.Utils;

namespace Ajuz.SmartTextEditor
{
    /// <summary>
    /// Искатель автодополнений к словам (быстрая реализация)
    /// </summary>
    public class FastAutocompleteSearcher
    {
        /// <summary>
        /// Словарь автодополнений, отсортированный по тексту
        /// </summary>
        private readonly DictionaryWord[] _dictionaryWordsSortedByText;

        /// <summary>
        /// Инициализирует новый экземпляр класса FastAutocompleteSearcher
        /// </summary>
        /// <param name="dictionaryWords">Словарь автодополнений</param>
        /// <exception cref="ArgumentNullException">dictionaryWords содержит null</exception>
        public FastAutocompleteSearcher(IEnumerable<DictionaryWord> dictionaryWords)
        {
            if (dictionaryWords == null)
            {
                throw new ArgumentNullException("dictionaryWords");
            }

            _dictionaryWordsSortedByText = dictionaryWords.ToArray();
             Array.Sort(
                _dictionaryWordsSortedByText,
                (x, y) => string.CompareOrdinal(x.Text, y.Text));
        }

        /// <summary>
        /// Найти слова, начинающиеся с указанной строки
        /// </summary>
        /// <param name="value">Строка для сравнения</param>
        /// <param name="maxCount">Максимальное количество возвращаемых элементов</param>
        /// <returns>Набор слов, подходящих для автодополнения</returns>
        /// <exception cref="ArgumentNullException">value содержит null</exception>
        /// <exception cref="ArgumentOutOfRangeException">maxCount содержит значение меньше 1</exception>
        /// <exception cref="ArgumentException">value содержит пустую строку</exception>
        public IEnumerable<DictionaryWord> FindWordsStartsWith(string value, int maxCount = 10)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (value == string.Empty)
            {
                throw new ArgumentException(
                    "Аргумент содержит пустую строку",
                    "value");
            }

            if (maxCount < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "maxCount",
                    maxCount,
                    "Аргумент должен быть не меньше 1.");
            }

            // выбираем слова с тем же началом
            var dictionaryWordsWithSameBeginning = 
                _dictionaryWordsSortedByText.BinarySearchAll(
                    new DictionaryWord(value, 1), 
                    (x, y) => x.Text.StartsWith(y.Text) ? 0 : string.CompareOrdinal(x.Text, y.Text));

            // выбираем слова с максимальной частотой, упорядоченные
            // 1) по убыванию частоты 
            // 2) затем по возрастанию по алфавиту
            var topDictionaryWordsWithSameBeginning = 
                dictionaryWordsWithSameBeginning.GetTopOrderedByDescending(
                    maxCount, 
                    (x, y) => 
                        x.Frequency > y.Frequency ? 1 
                        : x.Frequency < y.Frequency ? -1 
                        : -1 * string.CompareOrdinal(x.Text, y.Text));
            
            return topDictionaryWordsWithSameBeginning;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor
{
    /// <summary>
    /// Искатель автодополнений к словам
    /// </summary>
    public class AutocompleteSearcher
    {
        /// <summary>
        /// Словарь для автодополнений
        /// </summary>
        private readonly IEnumerable<DictionaryWord> _dictionaryWords;

        /// <summary>
        /// Инициализирует новый экземпляр класса AutocompleteSearcher
        /// </summary>
        /// <param name="dictionaryWords">Словарь автодополнений</param>
        /// <exception cref="ArgumentNullException">dictionaryWords содержит null</exception>
        public AutocompleteSearcher(IEnumerable<DictionaryWord> dictionaryWords)
        {
            if (dictionaryWords == null)
            {
                throw new ArgumentNullException("dictionaryWords");
            }

            _dictionaryWords = dictionaryWords;
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
                    "Аргумент не может быть меньше 1.");
            }

            return _dictionaryWords
                .Where(w => w.Text.StartsWith(value, StringComparison.Ordinal))
                .OrderByDescending(w => w.Frequency)
                .ThenBy(w => w.Text)
                .Take(maxCount)
                .ToList();
        }
    }
}

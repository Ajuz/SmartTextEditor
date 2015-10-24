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
        public AutocompleteSearcher(IEnumerable<DictionaryWord> dictionaryWords)
        {
            if (dictionaryWords == null)
            {
                throw new ArgumentNullException("dictionaryWords");
            }

            _dictionaryWords = dictionaryWords;
        }

        /// <summary>
        /// Найти слова, начинающиеся с указанной подстроки
        /// </summary>
        /// <param name="beginOfWords">Начало слова</param>
        /// <param name="maxCount">Максимальное количество возвращаемых элементов</param>
        /// <returns>Набор слов, подходящих для автодополнения </returns>
        public IEnumerable<DictionaryWord> FindWordsBeginingWith(string beginOfWords, int maxCount = 10)
        {
            if (beginOfWords == null)
            {
                throw new ArgumentNullException("beginOfWords");
            }

            if (beginOfWords == string.Empty)
            {
                throw new ArgumentException(
                    "Аргумент содержит пустую строку",
                    "beginOfWords");
            }

            if (maxCount < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "maxCount",
                    maxCount,
                    "Аргумент не может быть меньше 1.");
            }

            return _dictionaryWords
                .Where(w => w.Text.StartsWith(beginOfWords, StringComparison.Ordinal))
                .OrderByDescending(w => w.Frequency)
                .ThenBy(w => w.Text)
                .Take(maxCount)
                .ToList();
        }
    }
}

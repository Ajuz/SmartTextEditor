using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor.IO
{
    /// <summary>
    /// Класс чтения словарных слов
    /// </summary>
    public class DictionaryWordsInputReader : IInputReader<DictionaryWord>
    {
        private readonly StreamReader _streamReader;

        /// <summary>
        /// Инициализирует новый экземпляр класса DictionaryWordsInputReader
        /// </summary>
        /// <param name="streamReader">Объект чтения потока</param>
        /// <exception cref="ArgumentNullException">streamReader содержит null</exception>
        public DictionaryWordsInputReader(StreamReader streamReader)
        {
            if (streamReader == null)
            {
                throw new ArgumentNullException("streamReader");
            }

            _streamReader = streamReader;
        }
        
        /// <summary>
        /// Парсить из строки <paramref name="value"/> объект DictionaryWord.
        /// </summary>
        /// <param name="value">Исходная строка</param>
        /// <returns>Объект DictionaryWord</returns>
        /// <exception cref="ArgumentNullException">value содержит null</exception>
        /// <exception cref="ArgumentException">value содержит пустую строку</exception>
        protected virtual DictionaryWord ParseDictionaryWord(string value)
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

            var dictionaryWordElements = value.Split(' ');

            var dictionaryWord = new DictionaryWord(
                dictionaryWordElements[0],
                int.Parse(dictionaryWordElements[1]));

            return dictionaryWord;
        }

        /// <summary>
        /// Прочитать словарные слова из потока.
        /// </summary>
        /// <returns>Набор словарных слов</returns>
        /// <exception cref="InvalidOperationException">
        /// Количество получаемых словарных слов меньше 0.
        /// </exception>
        public IEnumerable<DictionaryWord> Read()
        {
            var dictionaryWordsCount = int.Parse(_streamReader.ReadLine());

            if (dictionaryWordsCount < 0)
            {
                throw new InvalidOperationException(
                    "Количество получаемых словарных слов меньше 0");
            }

            var dictionaryWords = new List<DictionaryWord>(dictionaryWordsCount);

            for (int i = 0; i < dictionaryWordsCount; i++)
            {
                var dictionaryWord = ParseDictionaryWord(_streamReader.ReadLine());
                dictionaryWords.Add(dictionaryWord);
            }

            return dictionaryWords;
        }
    }
}

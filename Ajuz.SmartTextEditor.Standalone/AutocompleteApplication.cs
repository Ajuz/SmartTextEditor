using Ajuz.SmartTextEditor.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor.Standalone
{
    /// <summary>
    /// Приложение поиска автодополнений
    /// </summary>
    internal class AutocompleteApplication
    {
        /// <summary>
        /// Обеспечивает чтение словарных слов
        /// </summary>
        private IInputReader<DictionaryWord> _dictionaryWordsInputReader;

        /// <summary>
        /// Обеспечивает чтение начальных частей слов
        /// </summary>
        private IInputReader<string> _beginningsOfWordsInputReader;

        /// <summary>
        /// Обеспечивает вывод найденных автодополнений
        /// </summary>
        private IOutputWriter<DictionaryWord> _autocompleteOutputWriter;

        /// <summary>
        /// Инициализирует новый экземпляр класса AutocompleteApplication
        /// </summary>
        /// <param name="dictionaryWordsInputReader">
        /// Обеспечивает чтение словарных слов
        /// </param>
        /// <param name="beginningsOfWordsInputReader">
        /// Обеспечивает чтение начальных частей слов
        /// </param>
        /// <param name="autocompleteOutputWriter">
        /// Обеспечивает вывод найденных автодополнений
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// dictionaryWordsInputReader, beginningsOfWordsInputReader или
        /// autocompleteOutputWriter содержит null.
        /// </exception>
        public AutocompleteApplication(
            IInputReader<DictionaryWord> dictionaryWordsInputReader,
            IInputReader<string> beginningsOfWordsInputReader,
            IOutputWriter<DictionaryWord> autocompleteOutputWriter)
        {
            if (dictionaryWordsInputReader == null)
            {
                throw new ArgumentNullException("dictionaryWordsInputReader");
            }

            if (beginningsOfWordsInputReader == null)
            {
                throw new ArgumentNullException("beginningsOfWordsInputReader");
            }

            if (autocompleteOutputWriter == null)
            {
                throw new ArgumentNullException("autocompleteOutputWriter");
            }

            _dictionaryWordsInputReader = dictionaryWordsInputReader;
            _beginningsOfWordsInputReader = beginningsOfWordsInputReader;
            _autocompleteOutputWriter = autocompleteOutputWriter;
        }

        /// <summary>
        /// Найти автодополнения
        /// </summary>
        public void SearchAutocomplete()
        {
            IEnumerable<DictionaryWord> dictionaryWords = 
                _dictionaryWordsInputReader.Read();
            
            var autocompleteSearcher = new FastAutocompleteSearcher(dictionaryWords);
            
            IEnumerable<string> beginningsOfWords = _beginningsOfWordsInputReader.Read();

            foreach (var beginningOfWord in beginningsOfWords)
            {
                IEnumerable<DictionaryWord> autocompleteCollection =
                    autocompleteSearcher.FindWordsStartsWith(beginningOfWord);

                _autocompleteOutputWriter.Write(autocompleteCollection);
            }
        }
    }
}

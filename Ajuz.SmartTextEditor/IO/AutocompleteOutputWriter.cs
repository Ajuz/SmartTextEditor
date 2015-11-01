using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor.IO
{
    /// <summary>
    /// Класс вывода автодополнений
    /// </summary>
    public class AutocompleteOutputWriter : IOutputWriter<DictionaryWord>
    {
        private readonly StreamWriter _streamWriter;

        /// <summary>
        /// Инициализирует новый экземпляр класса AutocompleteOutputWriter
        /// </summary>
        /// <param name="streamWriter">Объект записи в поток</param>
        /// <exception cref="ArgumentNullException">streamWriter содержит null</exception>
        public AutocompleteOutputWriter(StreamWriter streamWriter)
        {
            if (streamWriter == null)
            {
                throw new ArgumentNullException("streamWriter");
            }

            _streamWriter = streamWriter;
        }

        /// <summary>
        /// Записать в поток варианты автодополнений для слова
        /// </summary>
        /// <param name="autocompleteCollection">Набор вариантов автодополнений</param>
        /// <exception cref="ArgumentNullException">autocompleteCollection содержит null</exception>
        public void Write(IEnumerable<DictionaryWord> autocompleteCollection)
        {
            if (autocompleteCollection == null)
            {
                throw new ArgumentNullException("autocompleteCollection");
            }

            _streamWriter.Write(
                "{0}{1}{1}",
                string.Join(
                    Environment.NewLine,
                    autocompleteCollection.Select(w => w.Text)), 
                Environment.NewLine);
        }
    }
}

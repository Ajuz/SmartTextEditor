using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor.IO
{
    /// <summary>
    /// Класс, читающий начала слов
    /// </summary>
    public class BeginningsOfWordsInputReader : IInputReader<string>
    {
        private readonly StreamReader _streamReader;

        /// <summary>
        /// Инициализирует новый экземпляр класса BeginningsOfWordsInputReader
        /// </summary>
        /// <param name="streamReader">Объект чтения потока</param>
        /// <exception cref="ArgumentNullException">streamReader содержит null</exception>
        public BeginningsOfWordsInputReader(StreamReader streamReader)
        {
            if (streamReader == null)
            {
                throw new ArgumentNullException("streamReader");
            }

            _streamReader = streamReader;
        }

        /// <summary>
        /// Прочитать начала слов из потока
        /// </summary>
        /// <returns>Набор строк, каждая из которых начало слова</returns>
        public IEnumerable<string> Read()
        {
            var beginningsOfWordsCount = int.Parse(_streamReader.ReadLine());
            var beginningsOfWords = new List<string>(beginningsOfWordsCount);

            for (int i = 0; i < beginningsOfWordsCount; i++)
            {
                var beginningOfWord = _streamReader.ReadLine();
                beginningsOfWords.Add(beginningOfWord);
            }

            return beginningsOfWords;
        }
    }
}

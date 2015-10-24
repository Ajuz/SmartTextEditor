using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor
{
    /// <summary>
    /// Словарное слово (слово из словаря)
    /// </summary>
    public class DictionaryWord
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса DictionaryWord
        /// </summary>
        /// <param name="text">Буквенное обозначение слова</param>
        /// <param name="frequency">Частота (сколько раз встречается в текстах)</param>
        /// <exception cref="ArgumentNullException">text содержит null</exception>
        /// <exception cref="ArgumentOutOfRangeException">frequency содержит значение меньше 1</exception> 
        /// <exception cref="ArgumentException">text содержит пустую строку</exception>        
        public DictionaryWord(string text, int frequency)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            if (text == string.Empty)
            {
                throw new ArgumentException(
                    "Аргумент содержит пустую строку", 
                    "text");
            }

            if (frequency < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "frequency", 
                    frequency, 
                    "Аргумент не может быть меньше 1.");
            }

            Text = text;
            Frequency = frequency;
        }

        /// <summary>
        /// Буквенное обозначение слова
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Частота (сколько раз встречается в текстах)
        /// </summary>
        public int Frequency { get; private set; }

        /// <summary>
        /// Преобразовать в строку
        /// </summary>
        /// <returns>Представление объекта в виде строки</returns>
        public override string ToString()
        {
            return string.Format(
                "(Text: '{0}'; Frequency: {1})",
                Text,
                Frequency);
        }
    }
}

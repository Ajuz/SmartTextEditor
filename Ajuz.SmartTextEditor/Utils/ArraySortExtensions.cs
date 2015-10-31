using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor.Utils
{
    /// <summary>
    /// Сортировка массива
    /// </summary>
    public static class ArraySortExtensions
    {
        /// <summary>
        /// Получить топовые (наибольшие) элементы массива, упорядоченные по убыванию.
        /// </summary>
        /// <typeparam name="T">Тип элементов массива</typeparam>
        /// <param name="array">Массив</param>
        /// <param name="comparison">Правило сравнения элементов</param>
        /// <param name="countOfTopElements">Максимальное количество топовых элементов</param>
        /// <returns>
        /// Массив из топовых (наибольших) элементов исходного массива, 
        /// упорядоченных по убыванию.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// array или comparison содержит null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// countOfTopElements содержит значение меньше 0.
        /// </exception>
        public static T[] GetTopOrderedByDescending<T>(this T[] array, Comparison<T> comparison, int countOfTopElements) where T: DictionaryWord
        {            
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }

            if (countOfTopElements < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "countOfTopElements",
                    countOfTopElements,
                    "Аргумент должен быть не меньше 0.");
            }

            if (countOfTopElements == 0)
            {
                return new T[0];
            }

            // Алгоритм:
            // В цикле пробегаемся по всем элементам исходного массива array
            // и заполняем (в порядке убывания) максимальными из них массив topSortedBuffer.
            // Массив topSortedBuffer имеет фиксированный размер = countOfTopElements.
            // Если массив topSortedBuffer полный - то очередной добавляемый в него элемент
            // вытеснит элемент с максимальным индексом (т.е. наименьший из ранее добавленных).
            // Фактическое количество хранящихся в нем элементов содержится в topSortedCount.
            
            // В качестве topSortedBuffer удобнее было бы использовать LinkedList<T>, 
            // но значительно падает производительность.

            var topSortedBuffer = new T[countOfTopElements];
            var topSortedCount = 0;
            var addedToTopSortedBuffer = false;

            for (int inputIndex = 0; inputIndex < array.Length; inputIndex++)
            {
                addedToTopSortedBuffer = false;

                for (int topSortedIndex = 0; topSortedIndex < topSortedCount; topSortedIndex++)
                {
                    if (comparison(topSortedBuffer[topSortedIndex], array[inputIndex]) < 0)
                    {
                        Array.Copy(
                            topSortedBuffer,
                            topSortedIndex,
                            topSortedBuffer,
                            topSortedIndex + 1,
                            countOfTopElements - topSortedIndex - 1);

                        topSortedBuffer[topSortedIndex] = array[inputIndex];

                        if (topSortedCount < topSortedBuffer.Length)
                        {
                            topSortedCount++;
                        }
                        
                        addedToTopSortedBuffer = true;
                        break;
                    }
                }

                if (topSortedCount < topSortedBuffer.Length && !addedToTopSortedBuffer)
                {
                    topSortedBuffer[topSortedCount] = array[inputIndex];
                    topSortedCount++;
                }
            }
           
            return topSortedBuffer.Take(topSortedCount).ToArray();
        }
    }
}

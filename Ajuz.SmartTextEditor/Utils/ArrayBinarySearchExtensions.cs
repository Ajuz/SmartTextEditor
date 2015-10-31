using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor.Utils
{
    /// <summary>
    /// Бинарный поиск в массиве
    /// </summary>
    public static class ArrayBinarySearchExtensions
    {
        /// <summary>
        /// Результат бинарного поиска в случае отсуствия совпадений.
        /// </summary>
        private const int BinarySearchNotFoundResult = -1;

        /// <summary>
        /// Выполнить бинарный поиск в сортированном массиве с нулевым начальным индексом 
        /// всех элементов, соответствующих значению <paramref name="value"/> с 
        /// использованием сравнения <paramref name="comparison"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов массива</typeparam>
        /// <param name="sortedArray">Исходный сортированный массив</param>
        /// <param name="value">Значение для поиска</param>
        /// <param name="comparison">Правило сравнения элементов</param>
        /// <returns>Найденные элементы</returns>
        /// <exception cref="ArgumentNullException">sortedArray, value или comparison содержит null.</exception>
        /// <seealso cref="https://en.wikipedia.org/wiki/Binary_search_algorithm"/>
        public static T[] BinarySearchAll<T>(this T[] sortedArray, T value, Comparison<T> comparison)
        {
            if (sortedArray == null)
            {
                throw new ArgumentNullException("sortedArray");
            }
            
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }

            var firstFoundIndex = BinarySearchFirst(sortedArray, value, comparison);

            if (firstFoundIndex != BinarySearchNotFoundResult)
            {
                var lastFoundIndex = BinarySearchLast(
                    sortedArray,
                    firstFoundIndex,
                    sortedArray.Length - firstFoundIndex,
                    value,
                    comparison);

                var searchResult = new T[lastFoundIndex - firstFoundIndex + 1];

                Array.Copy(
                    sortedArray, 
                    firstFoundIndex, 
                    searchResult, 
                    0, 
                    searchResult.Length);

                return searchResult;
            }

            return new T[0];
        }

        /// <summary>
        /// Выполнить бинарный поиск в сортированном массиве с нулевым начальным индексом 
        /// первого элемента, соответствующего значению <paramref name="value"/> с 
        /// использованием сравнения <paramref name="comparison"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов массива</typeparam>
        /// <param name="sortedArray">Исходный сортированный массив</param>
        /// <param name="value">Значение для поиска</param>
        /// <param name="comparison">Правило сравнения элементов</param>
        /// <returns>
        /// Индекс первого (по порядку) элемента, соответствующего условиям поиска.
        /// Если такой элемент не найден - возвращает -1.
        /// </returns>
        /// <exception cref="ArgumentNullException">sortedArray, value или comparison содержит null.</exception>
        /// <seealso cref="https://en.wikipedia.org/wiki/Binary_search_algorithm"/> 
        public static int BinarySearchFirst<T>(this T[] sortedArray, T value, Comparison<T> comparison)
        {
            if (sortedArray == null)
            {
                throw new ArgumentNullException("sortedArray");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }

            return BinarySearchFirst<T>(
                sortedArray, 
                0, 
                sortedArray.Length, 
                value, 
                comparison);
        }

        /// <summary>
        /// Выполнить бинарный поиск в диапазоне элементов сортированного массива 
        /// (с нулевым начальным индексом) - первого элемента, соответствующего значению 
        /// <paramref name="value"/> с использованием сравнения <paramref name="comparison"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов массива</typeparam>
        /// <param name="sortedArray">Исходный сортированный массив</param>
        /// <param name="index">Индекс начала диапазона поиска.</param>
        /// <param name="length">Количество элементов диапазона поиска</param>
        /// <param name="value">Значение для поиска</param>
        /// <param name="comparison">Правило сравнения элементов</param>
        /// <returns>
        /// Индекс первого (по порядку) элемента, соответствующего условиям поиска.
        /// Если такой элемент не найден - возвращает -1.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// sortedArray, value или comparison содержит null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// index или length меньше 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Аргументы index, length приводят к выходу за пределы массива.
        /// </exception>
        /// <seealso cref="https://en.wikipedia.org/wiki/Binary_search_algorithm"/>
        public static int BinarySearchFirst<T>(this T[] sortedArray, int index, int length, T value, Comparison<T> comparison)
        {
            if (sortedArray == null)
            {
                throw new ArgumentNullException("sortedArray");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "index",
                    index,
                    "Аргумент должен быть не меньше 0.");
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "length",
                    index,
                    "Аргумент должен быть не меньше 0.");
            }

            if (sortedArray.Length < index + length)
            {
                throw new ArgumentException(
                    "Аргументы index, length приводят к выходу за пределы массива.");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }

            var firstIndex = index;
            var lastIndex = index + length - 1;

            while (firstIndex <= lastIndex)
            {
                var middleIndex = firstIndex + (lastIndex - firstIndex) / 2;

                if (comparison(sortedArray[middleIndex], value) == 0)
                {
                    if (middleIndex == index)
                    {
                        // рассматриваемый индекс минимальный,
                        // значит мы нашли первый элемент с соответствием
                        return index;
                    }

                    if (comparison(sortedArray[middleIndex - 1], value) != 0)
                    {
                        // предыдущий элемент элемент не удовлетворяет условию сравнения,
                        // значит мы нашли первый элемент с соответствием 
                        return middleIndex;
                    }

                    // элемент найден, но он не первый (из соответствующих)
                    // продолжаем поиск в "левой" половине
                    lastIndex = middleIndex - 1;
                }
                else
                {
                    if (comparison(sortedArray[middleIndex], value) > 0)
                    {
                        lastIndex = middleIndex - 1;
                    }
                    else
                    {
                        firstIndex = middleIndex + 1;
                    }
                }
            }

            return BinarySearchNotFoundResult;
        }

        /// <summary>
        /// Выполнить бинарный поиск в диапазоне элементов сортированного массива 
        /// (с нулевым начальным индексом) - последнего элемента, соответствующего значению 
        /// <paramref name="value"/> с использованием сравнения <paramref name="comparison"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов массива</typeparam>
        /// <param name="sortedArray">Исходный сортированный массив</param>
        /// <param name="index">Индекс начала диапазона поиска.</param>
        /// <param name="length">Количество элементов диапазона поиска</param>
        /// <param name="value">Значение для поиска</param>
        /// <param name="comparison">Правило сравнения элементов</param>
        /// <returns>
        /// Индекс последнего (по порядку) элемента, соответствующего условиям поиска.
        /// Если такой элемент не найден - возвращает -1.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// sortedArray, value или comparison содержит null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// index или length меньше 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Аргументы index, length приводят к выходу за пределы массива.
        /// </exception>
        /// <seealso cref="https://en.wikipedia.org/wiki/Binary_search_algorithm"/>
        public static int BinarySearchLast<T>(this T[] sortedArray, T value, Comparison<T> comparison)
        {
            if (sortedArray == null)
            {
                throw new ArgumentNullException("sortedArray");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }

            return BinarySearchLast<T>(
                sortedArray,
                0,
                sortedArray.Length,
                value,
                comparison);
        }

        /// <summary>
        /// Выполнить бинарный поиск в диапазоне элементов сортированного массива 
        /// (с нулевым начальным индексом) - последнего элемента, соответствующего значению 
        /// <paramref name="value"/> с использованием сравнения <paramref name="comparison"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов массива</typeparam>
        /// <param name="sortedArray">Исходный сортированный массив</param>
        /// <param name="index">Индекс начала диапазона поиска.</param>
        /// <param name="length">Количество элементов диапазона поиска</param>
        /// <param name="value">Значение для поиска</param>
        /// <param name="comparison">Правило сравнения элементов</param>
        /// <returns>
        /// Индекс последнего (по порядку) элемента, соответствующего условиям поиска.
        /// Если такой элемент не найден - возвращает -1.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// sortedArray, value или comparison содержит null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// index или length меньше 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Аргументы index, length приводят к выходу за пределы массива.
        /// </exception>
        /// <seealso cref="https://en.wikipedia.org/wiki/Binary_search_algorithm"/>
        public static int BinarySearchLast<T>(this T[] sortedArray, int index, int length, T value, Comparison<T> comparison)
        {
            if (sortedArray == null)
            {
                throw new ArgumentNullException("sortedArray");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "index",
                    index,
                    "Аргумент должен быть не меньше 0.");
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "length",
                    index,
                    "Аргумент должен быть не меньше 0.");
            }

            if (sortedArray.Length < index + length)
            {
                throw new ArgumentException(
                    "Аргументы index, length приводят к выходу за пределы массива.");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }

            var maxIndex = index + length - 1;
            var firstIndex = index;
            var lastIndex = maxIndex;

            while (firstIndex <= lastIndex)
            {
                var middleIndex = firstIndex + (lastIndex - firstIndex) / 2;

                if (comparison(sortedArray[middleIndex], value) == 0)
                {
                    if (middleIndex == maxIndex)
                    {
                        // рассматриваемый индекс максимальный,
                        // значит мы нашли последний элемент с соответствием
                        return maxIndex;
                    }

                    if (comparison(sortedArray[middleIndex + 1], value) != 0)
                    {
                        // следующий элемент элемент не удовлетворяет условию сравнения,
                        // значит мы нашли последний элемент с соответствием 
                        return middleIndex;
                    }

                    // элемент найден, но он не последний (из соответствующих)
                    // продолжаем поиск в "правой" половине
                    firstIndex = middleIndex + 1;
                }
                else
                {
                    if (comparison(sortedArray[middleIndex], value) > 0)
                    {
                        lastIndex = middleIndex - 1;
                    }
                    else
                    {
                        firstIndex = middleIndex + 1;
                    }
                }
            }

            return BinarySearchNotFoundResult;
        }
    }
}

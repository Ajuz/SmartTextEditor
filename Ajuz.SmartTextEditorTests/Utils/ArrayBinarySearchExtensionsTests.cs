using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using Ajuz.SmartTextEditor.IO;
using System.Collections.Generic;
using Ajuz.SmartTextEditor;
using System.Collections;
using Ajuz.SmartTextEditor.Utils;

namespace Ajuz.SmartTextEditorTests.Utils
{
    /// <summary>
    /// Модульные тесты класса Ajuz.SmartTextEditor.Utils.ArrayBinarySearchExtensions
    /// </summary>
    [TestClass]
    public class ArrayBinarySearchExtensionsTests
    {
        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchAll параметру sortedArray 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchAll_WhenSortedArrayIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            int[] sortedArray = null;
            int value = 1;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchAll(
                    sortedArray,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("sortedArray", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchAll параметру value 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchAll_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = null;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchAll(
                    sortedArray,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("value", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchAll параметру comparison 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchAll_WhenComparisonIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = string.Empty;
            Comparison<string> comparison = null;

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchAll(
                    sortedArray,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("comparison", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray не содержит искомого значения value - метод
        /// BinarySearchAll должен вернуть пустой массив.
        /// </summary>
        [TestMethod]
        public void BinarySearchAll_WhenSortedArrayNotContainsSearchedValue_ReturnEmptyArray()
        {
            // arrange
            var sortedArray = new[] {1, 2, 3, 4};
            int value = 5;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = new int[0];

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchAll(
                sortedArray,
                value,
                comparison);

            //assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray содержит только один элемент с искомым значением value
        /// метод BinarySearchAll должен вернуть новый одноэлементный массив с этим элементом.
        /// </summary>
        [TestMethod]
        public void BinarySearchAll_WhenSortedArrayContainsOneElementEqualsSearchedValue_ReturnArrayWithOneElement()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 4 };
            int value = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = new int[] { 3 };

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchAll(
                sortedArray,
                value,
                comparison);

            //assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray содержит несколько элементов с искомым значением value
        /// метод BinarySearchAll должен вернуть новый массив со всеми этими элементами.
        /// </summary>
        [TestMethod]
        public void BinarySearchAll_WhenSortedArrayContainsSeveralElementsEqualsSearchedValue_ReturnArrayWithAllEqualsElements()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 3, 3, 4 };
            int value = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = new int[] { 3, 3, 3 };

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchAll(
                sortedArray,
                value,
                comparison);

            //assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchFirst (версия с 3 параметрами) параметру sortedArray 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst3_WhenSortedArrayIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            int[] sortedArray = null;
            int value = 1;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchFirst(
                    sortedArray,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("sortedArray", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchFirst (версия с 3 параметрами) параметру value 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst3_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = null;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchFirst(
                    sortedArray,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("value", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchFirst (версия с 3 параметрами) параметру comparison 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst3_WhenComparisonIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = string.Empty;
            Comparison<string> comparison = null;

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchFirst(
                    sortedArray,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("comparison", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray не содержит искомого значения value - метод
        /// BinarySearchFirst (версия с 3 параметрами) должен вернуть значение константы
        /// ArrayBinarySearchExtensions.BinarySearchNotFoundResult
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst3_WhenSortedArrayNotContainsSearchedValue_ReturnStatusNotFound()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 4 };
            int value = 5;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = ArrayBinarySearchExtensions.BinarySearchNotFoundResult;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchFirst(
                sortedArray,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray содержит только один элемент с искомым значением value
        /// метод BinarySearchFirst (версия с 3 параметрами) должен вернуть его индекс.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst3_WhenSortedArrayContainsOneElementEqualsSearchedValue_ReturnIndexOfThatElement()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 4 };
            int value = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = 2;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchFirst(
                sortedArray,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray содержит только несколько элементов с искомым 
        /// значением value метод BinarySearchFirst (версия с 3 параметрами) должен 
        /// вернуть индекс первого подходящего элемента последовательности.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst3_WhenSortedArrayContainsSeveralElementEqualsSearchedValue_ReturnIndexOfFirstThatElement()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 3, 3, 4 };
            int value = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = 2; 

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchFirst(
                sortedArray,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchLast (версия с 3 параметрами) параметру sortedArray 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast3_WhenSortedArrayIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            int[] sortedArray = null;
            int value = 1;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchLast(
                    sortedArray,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("sortedArray", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchLast (версия с 3 параметрами) параметру value 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast3_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = null;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchLast(
                    sortedArray,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("value", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchLast (версия с 3 параметрами) параметру comparison 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast3_WhenComparisonIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = string.Empty;
            Comparison<string> comparison = null;

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchLast(
                    sortedArray,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("comparison", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray не содержит искомого значения value - метод
        /// BinarySearchLast (версия с 3 параметрами) должен вернуть значение константы
        /// ArrayBinarySearchExtensions.BinarySearchNotFoundResult
        /// </summary>
        [TestMethod]
        public void BinarySearchLast3_WhenSortedArrayNotContainsSearchedValue_ReturnStatusNotFound()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 4 };
            int value = 5;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = ArrayBinarySearchExtensions.BinarySearchNotFoundResult;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchLast(
                sortedArray,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray содержит только один элемент с искомым значением value
        /// метод BinarySearchLast (версия с 3 параметрами) должен вернуть его индекс.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast3_WhenSortedArrayContainsOneElementEqualsSearchedValue_ReturnIndexOfThatElement()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 4 };
            int value = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = 2;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchLast(
                sortedArray,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray содержит только несколько элементов с искомым 
        /// значением value метод BinarySearchLast (версия с 3 параметрами) должен 
        /// вернуть индекс последнего подходящего элемента последовательности.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast3_WhenSortedArrayContainsSeveralElementEqualsSearchedValue_ReturnIndexOfLastThatElement()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 3, 3, 4 };
            int value = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = 4;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchLast(
                sortedArray,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchFirst (версия с 5 параметрами) параметру sortedArray 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst5_WhenSortedArrayIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            int[] sortedArray = null;
            int value = 1;
            int index = 0;
            int length = 0;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchFirst(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("sortedArray", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchFirst (версия с 5 параметрами) параметру value 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst5_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = null;
            int index = 0;
            int length = 0;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchFirst(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("value", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchFirst (версия с 5 параметрами) параметру comparison 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst5_WhenComparisonIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = string.Empty;
            int index = 0;
            int length = 0;
            Comparison<string> comparison = null;

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchFirst(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("comparison", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchFirst (версия с 5 параметрами) параметру index 
        /// передается значение меньше 0, должно быть выброшено исключение 
        /// ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst5_WhenIndexLess0_ShouldThrowArgumentOutOfRangeException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = string.Empty;
            int index = -1;
            int length = 0;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchFirst(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //assert
                Assert.AreEqual("index", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchFirst (версия с 5 параметрами) параметру length 
        /// передается значение меньше 0, должно быть выброшено исключение 
        /// ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst5_WhenLengthLess0_ShouldThrowArgumentOutOfRangeException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = string.Empty;
            int index = 0;
            int length = -1;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchFirst(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //assert
                Assert.AreEqual("length", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchFirst (версия с 5 параметрами) применение
        /// параметров index, length приводит к выходу за пределы массива sortedArray,
        /// должно выбрасываться исключение ArgumentException.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst5_WhenUsingIndexAndLengthMakeOverflow_ShouldThrowArgumentException()
        {
            // arrange
            string[] sortedArray = new string[2];
            string value = string.Empty;
            int index = 1;
            int length = 2;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchFirst(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentException e)
            {
                //assert
                if (e.GetType() == typeof(ArgumentException))
                {
                    return;
                }
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray не содержит искомого значения value 
        /// в заданном диапазоне элементов аргументами index, length - 
        /// метод BinarySearchFirst (версия с 5 параметрами) должен вернуть 
        /// значение константы ArrayBinarySearchExtensions.BinarySearchNotFoundResult
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst5_WhenSortedArrayNotContainsSearchedValue_ReturnStatusNotFound()
        {
            // arrange
            int[] sortedArray = new[] { 1, 2, 3, 4, 5 };
            int value = 5;
            int index = 1;
            int length = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = ArrayBinarySearchExtensions.BinarySearchNotFoundResult;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchFirst(
                sortedArray,
                index,
                length,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray в заданном диапазоне элементов аргументами index, length 
        /// содержит только один элемент с искомым значением value,
        /// метод BinarySearchFirst (версия с 5 параметрами) должен вернуть его индекс.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst5_WhenSortedArrayContainsOneElementEqualsSearchedValue_ReturnIndexOfThatElement()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 4 };
            int value = 3;
            int index = 1;
            int length = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = 2;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchFirst(
                sortedArray,
                index,
                length,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray в заданном диапазоне элементов аргументами index, length
        /// содержит несколько элементов с искомым значением value метод 
        /// BinarySearchFirst (версия с 5 параметрами) должен вернуть индекс 
        /// первого подходящего элемента последовательности.
        /// </summary>
        [TestMethod]
        public void BinarySearchFirst5_WhenSortedArrayContainsSeveralElementEqualsSearchedValue_ReturnIndexOfFirstThatElement()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 3, 3, 4 };
            int value = 3;
            int index = 1;
            int length = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = 2;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchFirst(
                sortedArray,
                index,
                length,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }



















        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchLast (версия с 5 параметрами) параметру sortedArray 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast5_WhenSortedArrayIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            int[] sortedArray = null;
            int value = 1;
            int index = 0;
            int length = 0;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchLast(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("sortedArray", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchLast (версия с 5 параметрами) параметру value 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast5_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = null;
            int index = 0;
            int length = 0;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchLast(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("value", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchLast (версия с 5 параметрами) параметру comparison 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast5_WhenComparisonIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = string.Empty;
            int index = 0;
            int length = 0;
            Comparison<string> comparison = null;

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchLast(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("comparison", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchLast (версия с 5 параметрами) параметру index 
        /// передается значение меньше 0, должно быть выброшено исключение 
        /// ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast5_WhenIndexLess0_ShouldThrowArgumentOutOfRangeException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = string.Empty;
            int index = -1;
            int length = 0;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchLast(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //assert
                Assert.AreEqual("index", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchLast (версия с 5 параметрами) параметру length 
        /// передается значение меньше 0, должно быть выброшено исключение 
        /// ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast5_WhenLengthLess0_ShouldThrowArgumentOutOfRangeException()
        {
            // arrange
            string[] sortedArray = new string[0];
            string value = string.Empty;
            int index = 0;
            int length = -1;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchLast(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //assert
                Assert.AreEqual("length", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод BinarySearchLast (версия с 5 параметрами) применение
        /// параметров index, length приводит к выходу за пределы массива sortedArray,
        /// должно выбрасываться исключение ArgumentException.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast5_WhenUsingIndexAndLengthMakeOverflow_ShouldThrowArgumentException()
        {
            // arrange
            string[] sortedArray = new string[2];
            string value = string.Empty;
            int index = 1;
            int length = 2;
            Comparison<string> comparison = (x, y) => string.CompareOrdinal(x, y);

            // act
            try
            {
                ArrayBinarySearchExtensions.BinarySearchLast(
                    sortedArray,
                    index,
                    length,
                    value,
                    comparison);
            }
            catch (ArgumentException e)
            {
                //assert
                if (e.GetType() == typeof(ArgumentException))
                {
                    return;
                }
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray не содержит искомого значения value 
        /// в заданном диапазоне элементов аргументами index, length - 
        /// метод BinarySearchLast (версия с 5 параметрами) должен вернуть 
        /// значение константы ArrayBinarySearchExtensions.BinarySearchNotFoundResult
        /// </summary>
        [TestMethod]
        public void BinarySearchLast5_WhenSortedArrayNotContainsSearchedValue_ReturnStatusNotFound()
        {
            // arrange
            int[] sortedArray = new[] { 1, 2, 3, 4, 5 };
            int value = 5;
            int index = 1;
            int length = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = ArrayBinarySearchExtensions.BinarySearchNotFoundResult;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchLast(
                sortedArray,
                index,
                length,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray в заданном диапазоне элементов аргументами index, length 
        /// содержит только один элемент с искомым значением value,
        /// метод BinarySearchLast (версия с 5 параметрами) должен вернуть его индекс.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast5_WhenSortedArrayContainsOneElementEqualsSearchedValue_ReturnIndexOfThatElement()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 4 };
            int value = 3;
            int index = 1;
            int length = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = 2;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchLast(
                sortedArray,
                index,
                length,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если массив sortedArray в заданном диапазоне элементов аргументами index, length
        /// содержит несколько элементов с искомым значением value метод 
        /// BinarySearchLast (версия с 5 параметрами) должен вернуть индекс 
        /// последнего подходящего элемента последовательности.
        /// </summary>
        [TestMethod]
        public void BinarySearchLast5_WhenSortedArrayContainsSeveralElementEqualsSearchedValue_ReturnIndexOfLastThatElement()
        {
            // arrange
            var sortedArray = new[] { 1, 2, 3, 3, 3, 4 };
            int value = 3;
            int index = 1;
            int length = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = 3;

            // act
            var actualResult = ArrayBinarySearchExtensions.BinarySearchLast(
                sortedArray,
                index,
                length,
                value,
                comparison);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

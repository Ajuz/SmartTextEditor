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
    /// Модульные тесты для класса Ajuz.SmartTextEditor.Utils.ArraySortExtensions
    /// </summary>
    [TestClass]
    public class ArraySortExtensionsTests
    {
        /// <summary>
        /// Тестовый метод: 
        /// Если в метод GetTopOrderedByDescending параметру array 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void GetTopOrderedByDescending_WhenArrayIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            int[] sortedArray = null;
            int countOfTopElements = 1;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            // act
            try
            {
                ArraySortExtensions.GetTopOrderedByDescending(
                    sortedArray,
                    countOfTopElements,
                    comparison);
            }
            catch (ArgumentNullException e)
            {
                //assert
                Assert.AreEqual("array", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод GetTopOrderedByDescending параметру comparison 
        /// передается null, должно быть выброшено исключение ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void GetTopOrderedByDescending_WhenComparisonIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            int[] sortedArray = new int[1];
            int countOfTopElements = 1;
            Comparison<int> comparison = null;

            // act
            try
            {
                ArraySortExtensions.GetTopOrderedByDescending(
                    sortedArray,
                    countOfTopElements,
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
        /// Если в метод GetTopOrderedByDescending параметру countOfTopElements 
        /// передается значение меньше 0, должно быть выброшено исключение 
        /// ArgumentOutOfRangeException.
        /// </summary>
        [TestMethod]
        public void GetTopOrderedByDescending_WhenCountOfTopElementsLess0_ShouldThrowArgumentOutOfRangeException()
        {
            // arrange
            int[] sortedArray = new int[1];
            int countOfTopElements = -1;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            // act
            try
            {
                ArraySortExtensions.GetTopOrderedByDescending(
                    sortedArray,
                    countOfTopElements,
                    comparison);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //assert
                Assert.AreEqual("countOfTopElements", e.ParamName, false);
                return;
            }

            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        /// <summary>
        /// Тестовый метод: 
        /// Если в метод GetTopOrderedByDescending параметру countOfTopElements 
        /// передается значение = 0, метод возвращает пустой массив.
        /// </summary>
        [TestMethod]
        public void GetTopOrderedByDescending_WhenCountOfTopElementsEqual0_ReturnEmptyArray()
        {
            // arrange
            int[] sortedArray = new int[1];
            int countOfTopElements = 0;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = new int[0];

            // act
            var actualResult = ArraySortExtensions.GetTopOrderedByDescending(
                sortedArray,
                countOfTopElements,
                comparison);

            //assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Метод GetTopOrderedByDescending возвращает массив с количеством элементов
        /// не превосходящим countOfTopElements.
        /// </summary>
        [TestMethod]
        public void GetTopOrderedByDescending_ReturnedArrayWithLengthLimitedByCountOfTopElements()
        {
            // arrange
            int[] sortedArray = new int[] { 1, 2, 3, 4, 5 };
            int countOfTopElements = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResultLength = 3;

            // act
            var actualResult = ArraySortExtensions.GetTopOrderedByDescending(
                sortedArray,
                countOfTopElements,
                comparison);

            //assert
            Assert.AreEqual(expectedResultLength, actualResult.Length);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Метод GetTopOrderedByDescending возвращает массив, полученный из
        /// максимальных элементов исходного массива array (не рассматриваем порядок).
        /// </summary>
        [TestMethod]
        public void GetTopOrderedByDescending_ReturnArrayWithLargestElementsFromOriginalArray()
        {
            // arrange
            int[] sortedArray = new int[] { 1, 2, 3, 4, 5 };
            int countOfTopElements = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            var expectedResult = new int[] { 3, 4, 5 }; // не учитываем порядок

            // act
            var actualResult = ArraySortExtensions.GetTopOrderedByDescending(
                sortedArray,
                countOfTopElements,
                comparison);

            //assert
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }

        /// <summary>
        /// Тестовый метод: 
        /// Метод GetTopOrderedByDescending возвращает массив, элементы которого
        /// упорядочены по убыванию.
        /// </summary>
        [TestMethod]
        public void GetTopOrderedByDescending_ReturnArrayOrderedByDescending()
        {
            // arrange
            int[] sortedArray = new int[] { 1, 2, 3, 4, 5 };
            int countOfTopElements = 3;
            Comparison<int> comparison = (x, y) => x.CompareTo(y);

            // act
            var actualResult = ArraySortExtensions.GetTopOrderedByDescending(
                sortedArray,
                countOfTopElements,
                comparison);

            //assert
            for (int i = 0; i < actualResult.Length; i++)
            {
                if (i > 0)
                {
                    Assert.IsTrue(
                        comparison(actualResult[i - 1], actualResult[i]) >= 0,
                        "Предыдущее значение {0} меньше последующего {1}",
                        actualResult[i - 1],
                        actualResult[i]);
                }
            }
        }
    }
}

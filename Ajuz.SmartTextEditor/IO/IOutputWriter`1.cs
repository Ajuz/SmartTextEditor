using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor.IO
{
    /// <summary>
    /// Описывается классами, осуществляющими вывод выходных данных
    /// </summary>
    /// <typeparam name="T">Тип элемента набора выходных данных</typeparam>
    public interface IOutputWriter<T>
    {
        /// <summary>
        /// Записать выходные данные
        /// </summary>
        /// <param name="collection">Набор объектов типа <typeparamref name="T"/></param>
        void Write(IEnumerable<T> collection);
    }
}

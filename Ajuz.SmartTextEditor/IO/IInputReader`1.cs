using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajuz.SmartTextEditor.IO
{
    /// <summary>
    /// Описываеся классами, читающими входные данные
    /// </summary>
    /// <typeparam name="T">Тип элемента входных данных после разбора</typeparam>
    public interface IInputReader<T>
    {
        /// <summary>
        /// Прочитать входные данные
        /// </summary>
        /// <returns>Набор объектов типа <typeparamref name="T"/></returns>
        IEnumerable<T> Read();
    }
}

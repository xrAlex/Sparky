using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class CollectionExtension
    {
        /// <summary>Удаляет первый элемент в индексированной коллекцию,
        /// удовлетворяющий предикату <paramref name="predicate"/>.</summary>
        /// <typeparam name="T">Тип элемента коллекции.</typeparam>
        /// <param name="list">Индексированная коллекция.</param>
        /// <param name="predicate">Предикат для поиска элемента в коллекции, котороый надо удалить.</param>
        /// <returns><see langword="true"/> если элемент был найден и удалён, иначе - <see langword="false"/>.</returns>
        public static bool RemoveFirst<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}

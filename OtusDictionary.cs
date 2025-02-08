using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus_Collection
{
    //наследуем от интерфейса с парой ключ(int):значение(string) для того, шоп итерировать по for и foreach
    public class OtusDictionary : IEnumerable<KeyValuePair<int, string>>
    {
        public int[] KeyArray;
        public string[] ValueArray;
        int ArrayLength;
        HashSet<int> HashTableKey = new HashSet<int>();
        public int currentIndex = 0;

        public OtusDictionary(int arrlength)
        {
            ArrayLength = arrlength;
            KeyArray = new int[ArrayLength];
            ValueArray = new string[ArrayLength];            
        }

        /// <summary>
        /// Метод ресайза массива вдвое
        /// </summary>
        private void ResizeArrays()
        {
            ArrayLength *= 2;
            Array.Resize(ref KeyArray, ArrayLength);
            Array.Resize(ref ValueArray, ArrayLength);
        }

        public string Add(int key, string value)
        {
            var SB = new StringBuilder();            

            // Проверяем, уникален ли ключ
            if (!string.IsNullOrEmpty(value) && HashTableKey.Add(key))
            {
                // Если массив заполнен, увеличиваем его размер
                if (currentIndex >= ArrayLength)
                {
                    ResizeArrays();
                    //throw new OverflowException("Массив переполнен");
                }

                // Добавляем ключ и значение в массивы
                KeyArray[currentIndex] = key;
                ValueArray[currentIndex] = value;
                currentIndex++;

                // Выводим текущее состояние массивов
                for (int i = 0; i < currentIndex; i++)
                {
                    SB.Append($"\n{KeyArray[i]}: {ValueArray[i]}");
                }
            }
            return SB.ToString();
        }

        public string Get(int key) {
            int index = Array.IndexOf(KeyArray, key);
            if (index == -1)
                throw new Exception("Такого элемента нет");

            return ValueArray[index];
        }

        /// <summary>
        /// Индексатор для получения значения по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Значение, соответствующее ключу</returns>
        public string this[int key]
        {
            get
            {
                int index = Array.IndexOf(KeyArray, key);
                if (index == -1)
                    throw new Exception("Такого элемента нет");
                return ValueArray[index];
            }
            set
            {
                int index = Array.IndexOf(KeyArray, key);
                if (index == -1)
                    throw new Exception("Такого элемента нет");
                ValueArray[index] = value;
            }
        }


        /// <summary>
        /// Получение значения по индексу (для поддержки цикла for)
        /// </summary>
        public KeyValuePair<int, string> GetElementByIndex(int index)
        {
            if (index < 0 || index >= currentIndex)
                throw new IndexOutOfRangeException("Такого индекса нетъ");
            return new KeyValuePair<int, string>(KeyArray[index], ValueArray[index]);
        }

        /// <summary>
        /// Реализация интерфейса IEnumerable<KeyValuePair<int, string>>
        /// </summary>
        public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
        {
            for (int i = 0; i < currentIndex; i++)
            {
                //используем yield для последовательного возвращения пары ключ-значение в итераторе
                yield return new KeyValuePair<int, string>(KeyArray[i], ValueArray[i]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

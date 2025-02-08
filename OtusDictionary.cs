using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus_Collection
{
    //наследуем от интерфейса с парой ключ(int):значение(string) для того, шоп итерировать по foreach
    public class OtusDictionary : IEnumerable<KeyValuePair<int, string>>
    {
        public int[] KeyArray;
        public string[] ValueArray;
        int ArrayLength;
        public int CurrentIndex;
        private bool[] ArrayInitialized; // Массив для отслеживания инициализированных элементов

        public OtusDictionary(int arrlength)
        {
            ArrayLength = arrlength;
            KeyArray = new int[ArrayLength];
            ValueArray = new string[ArrayLength];
            ArrayInitialized = new bool[ArrayLength];
            CurrentIndex = 0;
        }
        /// <summary>
        /// Хэш-функция для поиска уникального элемента
        /// </summary>
        /// <param name="keyArray"></param>
        /// <param name="valueArray"></param>
        /// <param name="key"></param>
        /// <param name="arrayLength"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private int HashFunction(int[] keyArray, string[] valueArray, int key, int arrayLength)
        {
            int index = key % arrayLength; //Тут вместо arrayLength можно запилить простое число, например 17 или 19, чтоб не делать связный список
            //Проверяем что значение в массиве инициализированных ключей допускает ввод, на случай если значение ключа 0,
            //чтоб не конфликтовало с дефолтными нулями при задании массива ключей, а то чо вообще
            while (ArrayInitialized[index])
            {
                if (keyArray[index] == key)
                {
                    throw new ArgumentException("Элемент с таким ключом уже существует.");
                }
                index = (index + 1) % arrayLength; //тут тоже что и в делителе выше
            }
            return index;
        }

        /// <summary>
        /// Метод ресайза массива вдвое
        /// </summary>
        private void ResizeArrays()
        {
            int newLength = ArrayLength * 2;
            int[] newKeys = new int[newLength];
            string[] newValues = new string[newLength];
            bool[] newInitialized = new bool[newLength];

            // Копируем существующие элементы в новый массив
            for (int i = 0; i < ArrayLength; i++)
            {
                if (ArrayInitialized[i])
                {
                    int newIndex = HashFunction(newKeys, newValues, KeyArray[i], newLength);
                    newKeys[newIndex] = KeyArray[i];
                    newValues[newIndex] = ValueArray[i];
                }
            }

            KeyArray = newKeys;
            ValueArray = newValues;
            ArrayLength = newLength;
        }

        /// <summary>
        /// Добавляйка в словарь
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="Exception"></exception>
        public string Add(int key, string value)
        {
            var SB = new StringBuilder();

            if (value == null)
            {
                throw new Exception($"Значение {key} в этом словаре не может быть null!11");
            }

            // Проверяем, есть ли уже такой ключ
            for (int i = 0; i < ArrayLength; i++)
            {
                if (ArrayInitialized[i] && KeyArray[i] == key)
                {
                    throw new Exception("Такой ключ уже существует");
                }
            }

            // Если массив заполнен, увеличиваем его размер
            if (CurrentIndex >= ArrayLength)
            {
                ResizeArrays();
            }

            // Находим свободный индекс для вставки
            int index = HashFunction(KeyArray, ValueArray, key, ArrayLength);
            KeyArray[index] = key;
            ValueArray[index] = value;
            ArrayInitialized[index] = true;
            CurrentIndex++;

            // Выводим текущее состояние массивов
            for (int i = 0; i < CurrentIndex; i++)
            {
                SB.Append($"\n{KeyArray[i]}: {ValueArray[i]}");
            }

            return SB.ToString();
        }
        /// <summary>
        /// Забирайка из словаря
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string Get(int key)
        {
            for (int i = 0; i < ArrayLength; i++)
            {
                if (ArrayInitialized[i] && KeyArray[i] == key)
                {
                    return ValueArray[i];
                }
            }

            throw new Exception("Такого элемента нетъ");
        }

        /// <summary>
        /// Индексатор для получения значений по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// Реализация интерфейса IEnumerable<KeyValuePair<int, string>> для foreach
        /// </summary>
        public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
        {
            for (int i = 0; i < CurrentIndex; i++)
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

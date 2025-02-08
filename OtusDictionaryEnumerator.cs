using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus_Collection
{
    public class OtusDictionaryEnumerator : IEnumerator
    {
        private readonly OtusDictionary _dictionary;
        private int _currentIndex = -1;

        public OtusDictionaryEnumerator(OtusDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        public object Current
        {
            get
            {
                if (_currentIndex < 0 || _currentIndex >= _dictionary.currentIndex)
                    throw new InvalidOperationException("Перечислитель находится вне допустимого диапазона.");
                return new KeyValuePair<int, string>(_dictionary.KeyArray[_currentIndex], _dictionary.ValueArray[_currentIndex]);
            }
        }

        public bool MoveNext()
        {
            _currentIndex++;
            return _currentIndex < _dictionary.currentIndex;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }
    }
}

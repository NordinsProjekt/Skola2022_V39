using System;
using System.Collections;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gen = System.Collections.Generic;

namespace Lists
{
    public sealed class HashTable<K,T> : IEnumerable
    {
        private Gen.LinkedList<KeyValuePair<K, T>>[] _table = new Gen.LinkedList<KeyValuePair<K, T>>[16];
        private int count = 0;

        public HashTable(int n = 16)
        {
            Init(n);
        }
        private void Init(int n = 16)
        {
            count = 0;
            _table = new Gen.LinkedList<KeyValuePair<K, T>>[n];
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i] = new Gen.LinkedList<KeyValuePair<K, T>>();
            }
        }
        public void Add(K key,T value)
        {
            CheckIfNeededToResize();
            int temp = key.GetHashCode();
            temp = temp % _table.Length;
            KeyValuePair<K, T> item = new KeyValuePair<K, T>(key,value);
            if (Find(key) == null)
            {
                _table[temp].AddLast(item);
                count++;
            }
        }
        public T Find(K key)
        {
            int temp = key.GetHashCode();
            temp = temp % _table.Length;
            var linky = _table[temp];
            foreach (var item in linky)
            {
                if (item.Key.Equals(key))
                    return item.Value;
            }
            return default(T);
        }
        public Gen.LinkedList<KeyValuePair<K,T>> this[int index]
        {
            get
            {
                Gen.LinkedList<KeyValuePair<K,T>> list = new Gen.LinkedList<KeyValuePair<K,T>>();
                foreach (var item in _table[index])
                {
                    list.AddLast(item);
                }
                return list;
            }
        }
        public void Remove(K key)
        {
            int temp = key.GetHashCode();
            temp = temp % _table.Length;
            Gen.LinkedList<KeyValuePair<K,T>> linky = _table[temp];
            KeyValuePair<K,T> item = linky.FirstOrDefault(x => x.Key.Equals(key));
            linky.Remove(item);
            count--;
        }

        public int Count
        {
            get { return count; }
        }

        public List<K> Keys
        {
            get
            { 
                List<K> list = new List<K>();
                foreach (var item in _table)
                {
                    var temp = item.First;
                    while(temp != null)
                    {
                        list.Add(temp.Value.Key);
                        temp = temp.Next;
                    }
                }
                return list;
            }
        }

        public void Clear()
        {
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>KeyValuePair<K,T></returns>

        private void CheckIfNeededToResize()
        {
            if ((Count/_table.Length)*100 >= 75)
                ResizeMe();
        }

        private void ResizeMe()
        {
            int size = _table.Length * 2;
            HashTable<K,T> list = new HashTable<K, T>(size);
            foreach (var item in _table)
            {
                KeyValuePair<K, T> temp = item.FirstOrDefault();
                list.Add(temp.Key, temp.Value);
            }
            Init(size);
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i] = list[i];
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                foreach (var item in _table[i])
                {
                    yield return new KeyValuePair<K, T>(item.Key, item.Value);
                }
            }
        }
    }
}

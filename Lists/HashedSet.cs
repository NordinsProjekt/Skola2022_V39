using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    public sealed class HashedSet<T> :IEnumerable<T>
    {
        private HashTable<int, T> hashTable = new HashTable<int, T>();
        private int hashCount = 0;

        public void Add(T obj)
        {
            if (GetIndex(obj) == -1)
                hashTable.Add(hashCount++, obj);
        }

        /// <summary>
        /// Return index of Objekt
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>(int)Index of Object
        /// -1 if it doesn´t exist</returns>
        public int Find(T obj)
        {
            return GetIndex(obj);
        }

        public void Remove( T obj)
        {
            int index = GetIndex(obj);
            if (index >=0)
            {
                hashTable.Remove(index);
                hashCount--;
            }
        }

        private int GetIndex(T obj)
        {
            foreach (KeyValuePair<int, T> item in hashTable)
            {
                if (item.Value.Equals(obj))
                    return item.Key;
            }
            return -1;
        }

        public int Count
        {
            get { return hashCount; }
        }

        public void Clear()
        {
            hashTable.Clear();
            hashCount = 0;
        }

        public bool IsEmpty
        {
            get
            {
                if (hashCount == 0)
                    return true;
                return false;
            }
        }

        public HashedSet<T> Union(HashedSet<T> hs)
        {
            HashedSet<T> result = new HashedSet<T>();
            foreach (T item in hs)
            {
                if (Find(item) == -1)
                    result.Add(item);
            }
            foreach (var item in hashTable.Keys)
            {
                result.Add(hashTable.Find(item));
            }
            return result;
        }

        public HashedSet<T> Intersect(HashedSet<T> hs)
        {
            HashedSet<T> result = new HashedSet<T>();
            foreach (T item in hs)
            {
                if (Find(item) != -1)
                    result.Add(item);
            }
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (dynamic item in hashTable)
            {
               yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new Exception();
        }
    }
}

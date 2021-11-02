using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.TaktikaTestTask.Pool
{
    public class Pool<T>
    where T: MonoBehaviour
    {
        private const int EmptyPoolRefillCount = 10;
        
        private readonly Stack<T> _pool = new Stack<T>();
        private readonly T _prefab;
        private readonly Transform _parent;

        private int _index;

        public Pool(int startingSize, T prefab, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            FillPool(startingSize);
        }

        public IEnumerator GetEnumerator()
        {
            return _pool.GetEnumerator();
        }
        
        public T GetFromPool()
        {
            if (_pool.Count == 0)
            {
                FillPool(EmptyPoolRefillCount);
            }
            return _pool.Pop();
        }

        public void ReturnToPool(T poolObject)
        {
            if (_pool.Contains(poolObject)) return;
            
            poolObject.gameObject.SetActive(false);
            _pool.Push(poolObject);
        }

        private void FillPool(int startingSize)
        {
            for (int i = 0; i < startingSize; i++)
            {
                CreatePoolObject();
            }
        }

        private void CreatePoolObject()
        {
            var newPoolObject = Object.Instantiate(_prefab, _parent, true);
            newPoolObject.gameObject.name = $"{newPoolObject.gameObject.name}: {_index++}"; 
            newPoolObject.gameObject.SetActive(false);
            _pool.Push(newPoolObject);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework3
{
    internal class BoundBuffer<T>
    {
        private const long MaxSize = Constants.ArraySize;
        private  Queue<T> _queue = new Queue<T>();
        private  List<T> _list = new List<T>();

        //List
        public void Add(T item)
        {
            lock (_list)
            {
                while (_list.Count >= MaxSize)
                {
                    Monitor.Wait(_list);
                }
                _list.Add(item);
            Monitor.Pulse(_list);
            }
        }

        public void SetList(List<T> l)
        {
            lock (_list)
                _list = l;
        }

        public List<T> GetList()
        {
            return _list;
        }

        //Queue
        public void Enqueue(T item)
        {
            lock (_queue)
            {
                while (_queue.Count >= MaxSize)
                {
                    Monitor.Wait(_queue);
                }
                _queue.Enqueue(item);
                Monitor.Pulse(_queue);
            }
        }

        public T Dequeue()
        {
            lock (_queue)
            {
                while (_queue.Count == 0)
                {
                    Monitor.Wait(_queue);
                }
                var item = _queue.Dequeue();
                Monitor.Pulse(_queue);
                return item;
            }
        }

        public void SetQueue(Queue<T> q)
        {
            lock (_queue)
                _queue = q;
        }

        public Queue<T> GetQueue()
        {
            return _queue;
        }
    }
}
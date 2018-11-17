using System;
using System.Collections;
using System.Collections.Generic;

namespace QueueTask
{
    /// <summary>
    /// Represents a first-in, first-out collection of objects.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public class Queue<T> : IEnumerable<T>
    {
        private const int QueueSize = 10;
        private int _head;
        private int _tail;
        private T[] _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class that is empty and has the default initial capacity.
        /// </summary>
        public Queue()
        {
            _items = new T[QueueSize];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="length">The initial number of elements that the <see cref="Queue{T}"/> can contain.</param>
        /// <exception cref="ArgumentException">The length of queue must be greater than 1</exception>
        public Queue(int length)
        {
            if (length < 2)
            {
                throw new ArgumentException("The length of queue must be greater than 1");
            }

            _items = new T[length];
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="Queue{T}"/>.
        /// </summary>
        public int Count
        {
            get
            {
                if (_head == 0)
                {
                    return _tail;
                }

                if (_tail > _head)
                {
                    return _tail - _head;
                }

                return _items.Length - _head + _tail;
            }
        }

        /// <summary>
        /// Adds an object to the end of the <see cref="Queue{T}"/>.
        /// </summary>
        /// <param name="data">The object to add to the <see cref="Queue{T}"/>.</param>
        public void Enqueue(T data)
        {
            if (Count == _items.Length && _head == 0)
            {
                Array.Resize(ref _items, _items.Length + (_items.Length / 2));
            }
            else if (Count == _items.Length && _tail == _head)
            {
                T[] newArray = new T[_items.Length + (_items.Length / 2)];
                Array.Copy(_items, _head, newArray, 0, _items.Length - _head);
                Array.Copy(_items, 0, newArray, _items.Length - _head, _tail);
                _head = 0;
                _tail = _items.Length;
                _items = newArray;
            }

            if (_head > 0)
            {
                if (_tail >= _items.Length)
                {
                    _tail = 0;
                }
            }

            _items[_tail] = data;
            _tail++;
        }

        /// <summary>
        /// Removes and returns the object at the beginning of the <see cref="Queue{T}" />.
        /// </summary>
        /// <returns>The object that is removed from the beginning of the <see cref="Queue{T}" />.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty</exception>
        public T Dequeue()
        {
            T result;
            if (Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            if (Count == 1)
            {
                result = _items[_head];
                _items[_head] = default(T);
                _tail = 0;
                _head = 0;
                return result;
            }

            if (_head < _items.Length - 1)
            {
                result = _items[_head];
                _items[_head] = default(T);
                _head++;
                return result;
            }

            _head = 0;
            result = _items[_items.Length - 1];
            _items[_items.Length - 1] = default(T);
            return result;
        }

        /// <summary>
        /// Returns the object at the beginning of the <see cref="Queue{T}" /> without removing it.
        /// </summary>
        /// <returns>The object at the beginning of the<see cref="Queue{T}" />.</returns>
        /// <exception cref="Exception">Queue is empty</exception>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            if (Count == 1)
            {
                return _items[_head];
            }

            if (_head < _items.Length - 1)
            {
                return _items[_head];
            }

            return _items[_items.Length - 1];
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Enumerator : IEnumerator<T>
        {
            private const int StartIndex = -1;
            private const int FinishIndex = -2;
            private readonly Queue<T> _queue;
            private readonly bool _headGreater;
            private int _index = StartIndex;
            private bool _wrapped;

            public Enumerator(Queue<T> queue)
            {
                _queue = queue;
                _headGreater = _queue._head >= _queue._tail;
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <exception cref="InvalidOperationException"></exception>
            public T Current
            {
                get
                {
                    if (_index < 0)
                    {
                        throw new InvalidOperationException();
                    }

                    return _queue._items[_index];
                }
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            object IEnumerator.Current => Current;

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            ///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                if (_queue.Count == 0 || _index == FinishIndex)
                {
                    _index = FinishIndex;
                    return false;
                }

                if (_headGreater)
                {
                    if (_index == StartIndex)
                    {
                        _index = _queue._head;
                        return true;
                    }

                    if (!_wrapped)
                    {
                        if (_index < _queue._items.Length - 1)
                        {
                            _index++;
                            return true;
                        }

                        if (_index == _queue._items.Length - 1)
                        {
                            _index = 0;
                            _wrapped = true;
                            return true;
                        }
                    }

                    if (_index < _queue._tail - 1)
                    {
                        _index++;
                        return true;
                    }

                    _index = FinishIndex;
                    return false;
                }

                if (_index == StartIndex)
                {
                    _index = _queue._head;
                    return true;
                }

                if (_index < _queue._tail - 1)
                {
                    _index++;
                    return true;
                }

                _index = FinishIndex;
                return false;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                _index = StartIndex;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;

namespace BinarySearchTreeTask
{
    /// <summary>
    /// Represents a collection of objects that is maintained in sorted order.
    /// </summary>
    public class BinarySearchTree<T> : IEnumerable<T>
    {
        private readonly IComparer<T> _comparer;
        private Node<T> _root;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer. If <see langword="null"/>, <see cref="Comparer{T}.Default"/> will be used.</param>
        public BinarySearchTree(IComparer<T> comparer = null)
        {
            _comparer = comparer ?? Comparer<T>.Default;
        }

        /// <summary>
        /// Adds an element to the tree.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Insert(T item)
        {
            if (_root == null)
            {
                _root = new Node<T>
                {
                    Key = item
                };
            }
            else
            {
                Insert(item, _root);
            }

            void Insert(T key, Node<T> node)
            {
                var compareResult = _comparer.Compare(key, node.Key);
                if (compareResult > 0)
                {
                    if (node.RightChild == null)
                    {
                        node.RightChild = new Node<T>
                        {
                            Key = key,
                            Parent = node
                        };
                    }
                    else
                    {
                        Insert(key, node.RightChild);
                    }
                }
                else if (compareResult < 0)
                {
                    if (node.LeftChild == null)
                    {
                        node.LeftChild = new Node<T>
                        {
                            Key = key,
                            Parent = node
                        };
                    }
                    else
                    {
                        Insert(key, node.LeftChild);
                    }
                }
            }
        }

        /// <summary>
        /// Removes the specified <see cref="item"/>.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void Remove(T item)
        {
            if (_root != null)
            {
                Remove(item, _root);
            }

            void Remove(T key, Node<T> node)
            {
                var compareResult = _comparer.Compare(key, node.Key);
                if (compareResult > 0)
                {
                    if (node.RightChild != null)
                    {
                        Remove(key, node.RightChild);
                    }
                }
                else if (compareResult < 0)
                {
                    if (node.LeftChild != null)
                    {
                        Remove(key, node.LeftChild);
                    }
                }
                else
                {
                    if (node.RightChild == null && node.LeftChild == null)
                    {
                        GetParentChildRef() = null;
                    }
                    else if (node.RightChild == null)
                    {
                        GetParentChildRef() = node.LeftChild;
                        node.LeftChild.Parent = node.Parent;
                    }
                    else if (node.LeftChild == null)
                    {
                        GetParentChildRef() = node.RightChild;
                        node.RightChild.Parent = node.Parent;
                    }
                    else if (node.RightChild.LeftChild == null)
                    {
                        node.RightChild.LeftChild = node.LeftChild;
                        node.LeftChild.Parent = node;
                        GetParentChildRef() = node.RightChild;
                        node.RightChild.Parent = node.Parent;
                    }
                    else
                    {
                        var m = node.RightChild;
                        while (m.LeftChild != null)
                        {
                            m = m.LeftChild;
                        }

                        m.Parent.LeftChild = null;
                        node.Key = m.Key;
                    }
                }

                ref Node<T> GetParentChildRef()
                {
                    if (ReferenceEquals(node.Parent, null))
                    {
                        return ref _root;
                    }

                    if (ReferenceEquals(node.Parent.RightChild, node))
                    {
                        return ref node.Parent.RightChild;
                    }

                    return ref node.Parent.LeftChild;
                }
            }
        }
        
        /// <summary>
        /// Determines whether the tree contains a specific element.
        /// </summary>
        public bool Contains(T item)
        {
            if (_root == null)
            {
                return false;
            }

            return Contains(item, _root);

            bool Contains(T key, Node<T> node)
            {
                var compareResult = _comparer.Compare(key, node.Key);

                if (compareResult > 0)
                {
                    return node.RightChild != null && Contains(key, node.RightChild);
                }

                if (compareResult < 0)
                {
                    return node.LeftChild != null && Contains(key, node.LeftChild);
                }

                return true;
            }
        }

        /// <summary>
        /// Gets the <see cref="IEnumerable{T}"/> that performs in-order tree traversal.
        /// </summary>
        public IEnumerable<T> GetInOrderEnumerable()
        {
            return Enumerate(_root);

            IEnumerable<T> Enumerate(Node<T> node)
            {
                if (node == null)
                {
                    yield break;
                }

                foreach (var n in Enumerate(node.LeftChild))
                {
                    yield return n;
                }

                yield return node.Key;

                foreach (var n in Enumerate(node.RightChild))
                {
                    yield return n;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="IEnumerable{T}"/> that performs pre-order tree traversal.
        /// </summary>
        public IEnumerable<T> GetPreOrderEnumerable()
        {
            return Enumerate(_root);

            IEnumerable<T> Enumerate(Node<T> node)
            {
                if (node == null)
                {
                    yield break;
                }

                yield return node.Key;

                foreach (var n in Enumerate(node.LeftChild))
                {
                    yield return n;
                }

                foreach (var n in Enumerate(node.RightChild))
                {
                    yield return n;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="IEnumerable{T}"/> that performs post-order tree traversal.
        /// </summary>
        public IEnumerable<T> GetPostOrderEnumerable()
        {
            return Enumerate(_root);

            IEnumerable<T> Enumerate(Node<T> node)
            {
                if (node == null)
                {
                    yield break;
                }

                foreach (var n in Enumerate(node.LeftChild))
                {
                    yield return n;
                }

                foreach (var n in Enumerate(node.RightChild))
                {
                    yield return n;
                }

                yield return node.Key;
            }
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => GetInOrderEnumerable().GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
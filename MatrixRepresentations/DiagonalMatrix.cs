using System;
using System.Collections.Generic;

namespace MatrixRepresentations
{
    public class DiagonalMatrix<T> : Matrix<T>
    {
        private readonly T[] _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagonalMatrix{T}"/> class.
        /// </summary>
        /// <param name="length">The length of matrix.</param>
        public DiagonalMatrix(int length)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            this._items = new T[length];
        }

        /// <summary>
        /// Gets the size of the matrix.
        /// </summary>
        public override int Size => this._items.Length;

        /// <summary>
        /// Gets or sets the element of the matrix from row <paramref name="i"/> and column <paramref name="j"/>.
        /// </summary>
        public override T this[int i, int j]
        {
            get
            {
                if (i > this.Size)
                {
                    throw new ArgumentException();
                }

                if (j > this.Size)
                {
                    throw new ArgumentException();
                }

                if (j != i)
                {
                    return default(T);
                }

                return this._items[i];
            }

            set
            {
                if (i > this.Size)
                {
                    throw new ArgumentException();
                }

                if (j > this.Size)
                {
                    throw new ArgumentException();
                }

                if (j == i)
                {
                    if (!EqualityComparer<T>.Default.Equals(this._items[i], value))
                    {
                        this._items[i] = value;
                        this.OnElementChanged(new MatrixElementChangedEventArgs(i, j));
                    }
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}

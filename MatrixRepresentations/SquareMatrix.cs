using System;
using System.Collections.Generic;

namespace MatrixRepresentations
{
    public class SquareMatrix<T> : Matrix<T>
    {
        private readonly T[,] _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareMatrix{T}"/> class.
        /// </summary>
        /// <param name="length">The length of matrix.</param>
        public SquareMatrix(int length)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            this._items = new T[length, length];
        }

        /// <summary>
        /// Gets the size of the matrix.
        /// </summary>
        public override int Size => this._items.GetLength(0);

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

                return this._items[i, j];
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

                if (!EqualityComparer<T>.Default.Equals(this._items[i, j], value))
                {
                    this._items[i, j] = value;
                    this.OnElementChanged(new MatrixElementChangedEventArgs(i, j));
                }
            }
        }
    }
}

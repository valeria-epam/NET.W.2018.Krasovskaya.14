using System;
using System.Collections.Generic;

namespace MatrixRepresentations
{
    public class SymmetricMatrix<T> : Matrix<T>
    {
        private readonly T[][] _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricMatrix{T}"/> class.
        /// </summary>
        /// <param name="length">The length of matrix.</param>
        public SymmetricMatrix(int length)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            this._items = new T[length][];
            for (int i = 0; i < length; i++)
            {
                this._items[i] = new T[i + 1];
            }
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

                if (j > i)
                {
                    return this._items[j][i];
                }

                return this._items[i][j];
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

                if (j > i)
                {
                    if (!EqualityComparer<T>.Default.Equals(this._items[j][i], value))
                    {
                        this._items[j][i] = value;
                        this.OnElementChanged(new MatrixElementChangedEventArgs(i, j));
                        this.OnElementChanged(new MatrixElementChangedEventArgs(j, i));
                    }
                }
                else
                {
                    if (!EqualityComparer<T>.Default.Equals(this._items[i][j], value))
                    {
                        this._items[i][j] = value;
                        this.OnElementChanged(new MatrixElementChangedEventArgs(i, j));
                        if (i != j)
                        {
                            this.OnElementChanged(new MatrixElementChangedEventArgs(j, i));
                        }
                    }
                }
            }
        }
    }
}

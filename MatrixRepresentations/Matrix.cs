using System;
using System.Collections.Generic;

namespace MatrixRepresentations
{
    public abstract class Matrix<T> : IEquatable<Matrix<T>>
    {
        /// <summary>
        /// Occurs when matrix element changed.
        /// </summary>
        public event EventHandler<MatrixElementChangedEventArgs> ElementChanged;

        /// <summary>
        /// Gets the size of the matrix.
        /// </summary>
        public abstract int Size { get; }

        /// <summary>
        /// Gets or sets the element of the matrix from row <paramref name="i"/> and column <paramref name="j"/>.
        /// </summary>
        public abstract T this[int i, int j] { get; set; }

        /// <summary>
        /// Performs addition of two matrices of the same size.
        /// The type <typeparamref name="T"/> must have operator + defined.
        /// </summary>
        public static Matrix<T> Add(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if (matrix1.Size != matrix2.Size)
            {
                throw new InvalidOperationException();
            }

            var newMatrix = new SquareMatrix<T>(matrix1.Size);
            for (int i = 0; i < matrix1.Size; i++)
            {
                for (int j = 0; j < matrix1.Size; j++)
                {
                    newMatrix[i, j] = (dynamic)matrix1[i, j] + (dynamic)matrix2[i, j];
                }
            }

            return newMatrix;
        }

        /// <inheritdoc />
        public bool Equals(Matrix<T> other)
        {
            if (other?.Size != Size)
            {
                return false;
            }

            for (int i = 0; i < other.Size; i++)
            {
                for (int j = 0; j < other.Size; j++)
                {
                    if (!EqualityComparer<T>.Default.Equals(other[i, j], this[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is Matrix<T> other && this.Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    if (this[i, j] != null)
                    {
                        hash += this[i, j].GetHashCode();
                    }
                }
            }

            return hash;
        }

        /// <summary>
        /// Raises the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MatrixElementChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnElementChanged(MatrixElementChangedEventArgs e) => ElementChanged?.Invoke(this, e);
    }
}

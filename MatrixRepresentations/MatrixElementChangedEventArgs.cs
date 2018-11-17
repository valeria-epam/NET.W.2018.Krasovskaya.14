using System;

namespace MatrixRepresentations
{
    public class MatrixElementChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixElementChangedEventArgs"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public MatrixElementChangedEventArgs(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        /// <summary>
        /// Gets the row.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Gets the column.
        /// </summary>
        public int Column { get; }
    }
}

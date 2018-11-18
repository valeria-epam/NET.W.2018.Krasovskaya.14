using System;
using MatrixRepresentations;
using NUnit.Framework;

namespace MatrixRepresentation.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void SymmetricMatrixTest()
        {
            var matrix = new SymmetricMatrix<int>(3)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [0, 2] = 3,
                [1, 1] = 4,
                [1, 2] = 5,
                [2, 2] = 6
            };
            Assert.AreEqual(matrix[0, 2], matrix[2, 0]);
        }

        [Test]
        public void DiagonalMatrixTest()
        {
            var matrix = new DiagonalMatrix<int>(3)
            {
                [0, 0] = 1,
                [1, 1] = 4,
                [2, 2] = 6
            };
            Assert.Throws<InvalidOperationException>(() => matrix[1, 0] = 0);
        }

        [Test]
        public void AdditionMatrixTest()
        {
            var matrix1 = new DiagonalMatrix<int>(2)
            {
                [0, 0] = 1,
                [1, 1] = 4,
            };

            var matrix2 = new SquareMatrix<int>(2)
            {
                [0, 0] = 0,
                [0, 1] = 1,
                [1, 0] = 2,
                [1, 1] = 3

            };

            var expected = new SquareMatrix<int>(2)
            {
                [0, 0] = 1,
                [0, 1] = 1,
                [1, 0] = 2,
                [1, 1] = 7

            };

            Assert.AreEqual(expected, Matrix<int>.Add(matrix1, matrix2));
        }
    }
}

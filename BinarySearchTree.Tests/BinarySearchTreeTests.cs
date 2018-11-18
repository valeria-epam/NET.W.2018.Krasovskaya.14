using System;
using System.Collections.Generic;
using System.Linq;
using BinarySearchTreeTask;
using NUnit.Framework;

namespace BinarySearchTree.Tests
{
    [TestFixture]
    public class BinarySearchTreeTests
    {
        [Test]
        public void IntInOrder()
        {
            var tree = new BinarySearchTree<int>();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(13);
            tree.Insert(8);
            tree.Insert(6);
            tree.Insert(20);
            Assert.AreEqual(new[] { 1, 2, 6, 8, 13, 20 }, tree);
        }

        [Test]
        public void IntInOrderWithComparer()
        {
            var tree = new BinarySearchTree<int>(new AbsComparer());
            tree.Insert(1);
            tree.Insert(-2);
            tree.Insert(13);
            tree.Insert(8);
            tree.Insert(-6);
            tree.Insert(20);
            Assert.AreEqual(new[] { 1, -2, -6, 8, 13, 20 }, tree);
        }

        [Test]
        public void StringPreOrder()
        {
            var tree = new BinarySearchTree<string>();
            tree.Insert("f");
            tree.Insert("b");
            tree.Insert("g");
            tree.Insert("l");
            tree.Insert("e");
            tree.Insert("a");
            Assert.AreEqual(new[] { "f", "b", "a", "e", "g", "l" }, tree.GetPreOrderEnumerable());
        }

        [Test]
        public void StringPreOrderWithComparer()
        {
            var tree = new BinarySearchTree<string>(StringComparer.OrdinalIgnoreCase);
            tree.Insert("f");
            tree.Insert("b");
            tree.Insert("G");
            tree.Insert("l");
            tree.Insert("E");
            tree.Insert("a");
            Assert.AreEqual(new[] { "f", "b", "a", "E", "G", "l" }, tree.GetPreOrderEnumerable());
        }

        [Test]
        public void BookPostOrder()
        {
            var book1 = new Book()
            {
                Name = "CLR",
                Pages = 800
            };
            var book2 = new Book()
            {
                Name = "Algorithms",
                Pages = 600
            };
            var book3 = new Book()
            {
                Name = "Harry Potter",
                Pages = 1000
            };
            var book4 = new Book()
            {
                Name = "Patterns",
                Pages = 350
            };
            var tree = new BinarySearchTree<Book>();
            tree.Insert(book1);
            tree.Insert(book2);
            tree.Insert(book3);
            tree.Insert(book4);
            Assert.AreEqual(new[] { book2, book4, book3, book1 }, tree.GetPostOrderEnumerable());
        }

        [Test]
        public void BookPostOrderWithComparer()
        {
            var book1 = new Book()
            {
                Name = "CLR",
                Pages = 800
            };
            var book2 = new Book()
            {
                Name = "Algorithms",
                Pages = 600
            };
            var book3 = new Book()
            {
                Name = "Harry Potter",
                Pages = 1000
            };
            var book4 = new Book()
            {
                Name = "Patterns",
                Pages = 350
            };
            var tree = new BinarySearchTree<Book>(new PagesRelationalComparer());
            tree.Insert(book1);
            tree.Insert(book2);
            tree.Insert(book3);
            tree.Insert(book4);
            Assert.AreEqual(new[] { book4, book2, book3, book1 }, tree.GetPostOrderEnumerable());
        }

        [Test]
        public void PointPostOrderWithComparer()
        {
            var point1 = new Point(2, 2);
            var point2 = new Point(4, 2);
            var point3 = new Point(3, 2);
            var point4 = new Point(1, 2);

            var tree = new BinarySearchTree<Point>(new XRelationalComparer());
            tree.Insert(point1);
            tree.Insert(point2);
            tree.Insert(point3);
            tree.Insert(point4);
            Assert.AreEqual(new[] { point4, point3, point2, point1 }, tree.GetPostOrderEnumerable());
        }

        [Test]
        public void Remove([Values(10, 2, 13, 8, 6, 20, 50)] int delete)
        {
            var tree = new BinarySearchTree<int>();
            tree.Insert(10);
            tree.Insert(2);
            tree.Insert(13);
            tree.Insert(8);
            tree.Insert(6);
            tree.Insert(20);
            tree.Remove(delete);
            Assert.AreEqual(new[] { 2, 6, 8, 10, 13, 20 }.Where(x => x != delete), tree);
        }

        [Test]
        public void Contains([Values(10, 2, 13, 8, 6, 20, 50)] int item)
        {
            var tree = new BinarySearchTree<int>();
            tree.Insert(10);
            tree.Insert(2);
            tree.Insert(13);
            tree.Insert(8);
            tree.Insert(6);
            tree.Insert(20);
            Assert.AreEqual(new[] { 2, 6, 8, 10, 13, 20 }.Contains(item), tree.Contains(item));
        }

        private struct Point
        {
            public Point(int x, int y) : this()
            {
                X = x;
                Y = y;
            }

            public int X { get; }

            public int Y { get; }
        }

        private class AbsComparer : IComparer<int>
        {
            public int Compare(int x, int y) => Math.Abs(x).CompareTo(Math.Abs(y));
        }

        private class Book : IComparable<Book>
        {
            public string Name { get; set; }

            public int Pages { get; set; }

            public int CompareTo(Book other)
            {
                if (ReferenceEquals(this, other))
                {
                    return 0;
                }

                if (ReferenceEquals(null, other))
                {
                    return 1;
                }

                return string.Compare(Name, other.Name, StringComparison.Ordinal);
            }
        }

        private sealed class PagesRelationalComparer : IComparer<Book>
        {
            public int Compare(Book x, Book y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }

                if (ReferenceEquals(null, y))
                {
                    return 1;
                }

                if (ReferenceEquals(null, x))
                {
                    return -1;
                }

                return x.Pages.CompareTo(y.Pages);
            }
        }

        private sealed class XRelationalComparer : IComparer<Point>
        {
            public int Compare(Point x, Point y)
            {
                return x.X.CompareTo(y.X);
            }
        }
    }
}
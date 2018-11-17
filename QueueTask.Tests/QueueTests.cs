using NUnit.Framework;

namespace QueueTask.Tests
{
    [TestFixture]
    public class QueueTests
    {
        [Test]
        public void QueueTest()
        {
            Queue<int> queue = new Queue<int>(3);
            queue.Enqueue(5);
            queue.Enqueue(2);
            Assert.AreEqual(5, queue.Dequeue());
        }

        [Test]
        public void QueueTest1()
        {
            Queue<int> queue = new Queue<int>(3);
            queue.Enqueue(5);
            queue.Enqueue(2);
            queue.Enqueue(4);
            queue.Enqueue(6);
            Assert.AreEqual(5, queue.Dequeue());
        }

        [Test]
        public void QueueTest2()
        {
            Queue<int> queue = new Queue<int>(4);
            queue.Enqueue(5);
            queue.Enqueue(2);
            queue.Enqueue(4);
            queue.Enqueue(6);
            Assert.AreEqual(new[] { 5, 2, 4, 6 }, queue);
        }

        [Test]
        public void QueueTest3()
        {
            Queue<int> queue = new Queue<int>(3);
            queue.Enqueue(5);
            queue.Enqueue(2);
            queue.Enqueue(4);
            queue.Dequeue();
            queue.Enqueue(6);
            Assert.AreEqual(new[] { 2, 4, 6 }, queue);
        }

        [Test]
        public void QueueTest4()
        {
            Queue<int> queue = new Queue<int>(3);
            
            Assert.AreEqual(new int[0], queue);
        }

        [Test]
        public void QueueTest5()
        {
            Queue<int> queue = new Queue<int>(3);
            queue.Enqueue(5);
            queue.Enqueue(2);
            queue.Enqueue(4);
            queue.Peek();
            Assert.AreEqual(5, queue.Dequeue());
        }
    }
}

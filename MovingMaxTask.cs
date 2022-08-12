using System;
using System.Collections.Generic;

namespace yield
{
    public static class MovingMaxTask
    {
        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            if (windowWidth > 150)
                windowWidth = 150;
            var deque = new DequeMovingMax<double>(windowWidth);
            foreach (var point in data)
            {
                deque.EnqueueLast(point.OriginalY);
                yield return point.WithMaxY(deque.DequeueFirst());
            }
        }
    }

    internal class DequeMovingMax<T>
        where T : IComparable<T>
    {
        private LinkedList<T> dataValues = new LinkedList<T>();
        private LinkedList<T> maxValues = new LinkedList<T>();
        private int windowWidth;

        public DequeMovingMax(int windowWidth)
        {
            this.windowWidth = windowWidth;
        }

        public void EnqueueLast(T value)
        {
            dataValues.AddLast(value);
            while (maxValues.Count > 0 && value.CompareTo(maxValues.Last.Value) > 0)
                maxValues.RemoveLast();
            maxValues.AddLast(value);
        }

        public T DequeueFirst()
        {
            var temp = maxValues.First.Value;
            if (dataValues.First.Value.CompareTo(maxValues.First.Value) == 0
                && dataValues.Count == windowWidth)
            {
                dataValues.RemoveFirst();
                maxValues.RemoveFirst();
            }
            else if (dataValues.Count == windowWidth)
                dataValues.RemoveFirst();
            return temp;
        }
    }
}
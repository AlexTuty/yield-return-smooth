using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
    public static class MovingAverageTask
    {
        public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
        {
            if (windowWidth > 20)
                windowWidth = 20;
            var queue = new Queue<double>(windowWidth);
            var avgSmoothedY = 0d;
            foreach (var point in data)
            {
                if (queue.Count == 0)
                    avgSmoothedY = point.OriginalY;
                queue.Enqueue(point.OriginalY);
                if (queue.Count > windowWidth)
                    avgSmoothedY =
                        avgSmoothedY - (queue.Dequeue() / windowWidth) + (point.OriginalY / windowWidth);
                else
                    avgSmoothedY =
                        avgSmoothedY - (avgSmoothedY / queue.Count) + (point.OriginalY / queue.Count);
                yield return point.WithAvgSmoothedY(avgSmoothedY);
            }
        }
    }
}
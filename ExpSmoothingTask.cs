using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
    public static class ExpSmoothingTask
    {
        public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
        {
            if (alpha >= 0 && alpha <= 1)
            {
                var expSmoothedY = 0d;
                var isPrime = true;
                foreach (var point in data)
                {
                    if (isPrime)
                    {
                        expSmoothedY = alpha == 0d ? 1d : point.OriginalY;
                        isPrime = false;
                    }
                    expSmoothedY = alpha * point.OriginalY + (1 - alpha) * expSmoothedY;
                    yield return point.WithExpSmoothedY(expSmoothedY);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Source.Domain.Utilities
{
    public static class PointUtility
    {
        public static List<Vector2> ResamplingPoints(List<Vector2> originPoints, int finalPointCount)
        {
            if (finalPointCount <= 0)
                throw new ArgumentException("Final points count must be greater than zero.");
            
            float totalLenght = 0;
            for (var i = 0; i < originPoints.Count - 1; i++)
                totalLenght += Vector2.Distance(originPoints[i], originPoints[i+1]);
            float step = totalLenght/ (finalPointCount - 1);

            float accumDistance = 0;
            List<Vector2> sampledPoints = new List<Vector2>(finalPointCount);
            sampledPoints.Add(originPoints[0]);
            for (int i = 1; i < originPoints.Count; i++)
            {
                float distance = Vector2.Distance(originPoints[i - 1], originPoints[i]);
                if (accumDistance + distance >= step)
                {
                    float t = (step - accumDistance) / distance;
                    var newPoint = originPoints[i - 1] + t * (originPoints[i] - originPoints[i - 1]);
                    
                    sampledPoints.Add(newPoint);
                    accumDistance = 0;
                    originPoints.Insert(i, newPoint);
                }
                else
                {
                    accumDistance += distance;
                }

            }
            if (sampledPoints.Count < finalPointCount)
                sampledPoints.Add(originPoints[^1]);
            
            return sampledPoints;
        }

        public static void SquareScaling1X1(List<Vector2> originPoints)
        {
            float minX = int.MaxValue;
            float maxX = int.MinValue;
            float  minY = int.MaxValue;
            float maxY = int.MinValue;
            foreach (var point in originPoints)
            {
                minX = Mathf.Min(minX, point.x);
                maxX = Mathf.Max(maxX, point.x);
                minY = Mathf.Min(minY, point.y);
                maxY = Mathf.Max(maxY, point.y);
            }

            float width = Mathf.Max(maxX - minY, 0.000001f);
            float height = Mathf.Max(maxY - minX, 0.000001f);

            for (var i = 0; i < originPoints.Count; i++)
            {
                var p = originPoints[i];
                float x = (p.x - minX) / width;
                float y = (p.y - minY) / height;

                originPoints[i] = new Vector2(x, y);
            }
        }

        public static void TranslateToCenter(List<Vector2> originPoints)
        {
            float xSum = 0;
            float ySum = 0;
            originPoints.ForEach(point =>
            {
                xSum += point.x;
                ySum  += point.y;
            });
            var center = new Vector2(xSum, ySum) / originPoints.Count;
            for (var i = 0; i < originPoints.Count; i++)
            {
                originPoints[i] -= center;
            }
        }
    }
    
}
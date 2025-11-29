using System.Collections.Generic;
using UnityEngine;

namespace _Game.Source.Domain
{
    public class Figure
    {
        public List<Vector2> Points { get; private set; }
        public string ID { get; private set; }
        public int BendsCount { get; private set; }

        public Figure(List<Vector2> points, string id, int bendsCount)
        {
            Points = points;
            ID = id;
            BendsCount = bendsCount;
        }
    }
}
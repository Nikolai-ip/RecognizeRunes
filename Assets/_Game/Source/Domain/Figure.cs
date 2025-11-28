using System.Collections.Generic;
using UnityEngine;

namespace _Game.Source.Domain
{
    public class Figure
    {
        public List<Vector2> Points { get; private set; }
        public string ID { get; private set; }

        public Figure(List<Vector2> points, string id)
        {
            Points = points;
            ID = id;
        }
    }
}
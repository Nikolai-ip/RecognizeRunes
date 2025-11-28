using System.Collections.Generic;
using UnityEngine;

namespace _Game.Source.Domain
{
    public class Line
    {
        private readonly float _step;
        private Vector2 _previousPoint = new(float.MaxValue, float.MaxValue);
        private List<Vector2> _points = new();

        public Line(float step)
        {
            _step = step;
        }

        public bool TryAddPoint(Vector2 mousePos)
        {
            if (Vector2.Distance(mousePos, _previousPoint) >= _step)
            {
                _previousPoint = mousePos;
                _points.Add(_previousPoint);
                return true;
            }
            return false;
        }
    }
}
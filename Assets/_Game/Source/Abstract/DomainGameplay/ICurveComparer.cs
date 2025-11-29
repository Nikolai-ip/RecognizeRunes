using System.Collections.Generic;
using UnityEngine;

namespace _Game.Source.Abstract.DomainGameplay
{
    public interface ICurveComparer
    {
        float CompareCurves(List<Vector2> a, List<Vector2> b);
    }
}
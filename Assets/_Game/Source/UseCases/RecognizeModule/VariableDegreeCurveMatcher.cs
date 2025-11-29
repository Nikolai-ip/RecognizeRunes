using System.Collections.Generic;
using _Game.Source.Abstract.DomainGameplay;
using UnityEngine;

namespace _Game.Source.UseCases.RecognizeModule
{
    public class VariableDegreeCurveMatcher: ICurveComparer
    {
        private readonly float _sqrtPow = 5f;
        private const float SQRT_2 = 1.4142f;
        
        public float CompareCurves(List<Vector2> a, List<Vector2> b)
        {
            if (a.Count != b.Count) return 1;
            float count = a.Count;
            float error = 0;
            float totalDistanceDiff = 0;
            for (int i = 0; i < count; i++)
            {
                float d = Vector2.Distance(a[i], b[i]);
                totalDistanceDiff += d*d*d*d*d;
            }

            error = totalDistanceDiff / count;
            error = Mathf.Pow(error, 1f / _sqrtPow);
            error /= SQRT_2;
            
            return error;
        }

    }
}
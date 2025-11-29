using System.Collections.Generic;
using UnityEngine;

namespace _Game.Source.UseCases.RecognizeModule
{
    public class BendsCounter
    {
        private readonly float _angleThresholdRad;

        public BendsCounter(float angleThresholdRad)
        {
            _angleThresholdRad = angleThresholdRad;
        }

        public int CountBends(List<Vector2> pts)
        {
            int bends = 0;

            for (int i = 1; i < pts.Count - 1; i++)
            {
                Vector2 v1 = pts[i] - pts[i - 1];
                Vector2 v2 = pts[i + 1] - pts[i];

                float mag1 = v1.magnitude;
                float mag2 = v2.magnitude;
                if (mag1 < 0.0001f || mag2 < 0.0001f)
                    continue;

                float cos = Vector2.Dot(v1, v2) / (mag1 * mag2);
                cos = Mathf.Clamp(cos, -1f, 1f);

                float angle = Mathf.Acos(cos);

                if (angle > _angleThresholdRad)
                    bends++;
            }

            return bends;
        }
    }
}
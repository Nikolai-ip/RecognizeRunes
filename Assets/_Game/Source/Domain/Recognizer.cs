using System;
using System.Collections.Generic;
using _Game.Source.Domain.Utilities;
using UnityEngine;

namespace _Game.Source.Domain
{
    public class Recognizer
    {
        private const float SQRT_2 = 1.4142f;
        
        private readonly int _figureDotCount;
        private readonly float _minErrorValueToDetectFigure;
        private readonly IRepository<Figure> _figureRepository;
        private FindFigureResult _findFigureResult;
        public event Action<FindFigureResult> OnFindFigureResultChanged;

        public Recognizer(float minErrorValueToDetectFigure, int figureDotCount, IRepository<Figure> figureRepository)
        {
            _minErrorValueToDetectFigure = minErrorValueToDetectFigure;
            _figureRepository = figureRepository;
            _figureDotCount = figureDotCount;
        }

        public FindFigureResult FindFigureByPoints(List<Vector2> rawPoints)
        {
            var points = PointUtility.ResamplingPoints(rawPoints, _figureDotCount);
            PointUtility.SquareScaling1X1(points);
            PointUtility.TranslateToCenter(points);
            float minError = float.MaxValue;
            Figure closestFigure = null;
            foreach (var figure in _figureRepository)
            {
                float error = CompareCurves(points, figure.Points);
                Debug.Log(figure.ID + ": " + error);
                if (error < minError)
                {
                    closestFigure = figure;
                    minError = error;
                }
            }

            bool figureHasFound = FigureHasFound(closestFigure, minError);
            string id = figureHasFound && closestFigure !=null ? closestFigure.ID : "";
            _findFigureResult = new FindFigureResult(id, figureHasFound, minError);
            OnFindFigureResultChanged?.Invoke(_findFigureResult);
            return _findFigureResult;
        }

        private bool FigureHasFound(Figure closestFigure, float minError)
        {
            return closestFigure != null && minError <= _minErrorValueToDetectFigure;
        }

        private float CompareCurves(List<Vector2> a, List<Vector2> b)
        {
            if (a.Count != b.Count) return 1;
            float count = a.Count;
            float error = 0;
            float totalDistanceDiff = 0;
            for (int i = 0; i < count; i++)
            {
                float d = Vector2.Distance(a[i], b[i]);
                totalDistanceDiff += d*d;
            }

            error = totalDistanceDiff / count;
            error = Mathf.Sqrt(error);
            error /= SQRT_2;
            
            return error;
        }
    }
}
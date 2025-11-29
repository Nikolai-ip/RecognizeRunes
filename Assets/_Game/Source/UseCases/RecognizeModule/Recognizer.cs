using System;
using System.Collections.Generic;
using _Game.Source.Abstract.DomainGameplay;
using _Game.Source.Domain;
using _Game.Source.UseCases.Utilities;
using UnityEngine;

namespace _Game.Source.UseCases.RecognizeModule
{
    public class Recognizer
    {
        private readonly int _figureDotCount;
        private readonly float _minErrorValueToDetectFigure;
        private readonly IRepository<Figure> _figureRepository;
        private readonly ICurveComparer _curveComparer;
        private FindFigureResult _findFigureResult;
        public event Action<FindFigureResult> OnFindFigureResultChanged;

        public Recognizer(float minErrorValueToDetectFigure, int figureDotCount, IRepository<Figure> figureRepository, ICurveComparer curveComparer)
        {
            _minErrorValueToDetectFigure = minErrorValueToDetectFigure;
            _figureRepository = figureRepository;
            _figureDotCount = figureDotCount;
            _curveComparer = curveComparer;
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
                float error = _curveComparer.CompareCurves(points, figure.Points);
                Debug.Log(figure.ID + " : " + error);
                if (error < minError)
                {
                    closestFigure = figure;
                    minError = error;
                }
            }

            bool figureHasFound = FigureHasFound(closestFigure, minError);
            string id = figureHasFound && closestFigure != null ? closestFigure.ID : "";
            _findFigureResult = new FindFigureResult(id, figureHasFound, minError);
            
            OnFindFigureResultChanged?.Invoke(_findFigureResult);
            return _findFigureResult;
        }

        private bool FigureHasFound(Figure closestFigure, float minError)
        {
            return closestFigure != null && minError <= _minErrorValueToDetectFigure;
        }
    }
}
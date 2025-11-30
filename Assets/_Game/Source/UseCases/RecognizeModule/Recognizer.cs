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
        private readonly float _bendCountWeight;
        private readonly IRepository<Figure> _figureRepository;
        private readonly ICurveComparer _curveComparer;
        private readonly BendsCounter _bendsCounter;
        private FindFigureResult _findFigureResult;
        public event Action<FindFigureResult> OnFindFigureResultChanged;

        public Recognizer(float minErrorValueToDetectFigure, int figureDotCount, IRepository<Figure> figureRepository, ICurveComparer curveComparer, float bendCountWeight, BendsCounter bendsCounter)
        {
            _minErrorValueToDetectFigure = minErrorValueToDetectFigure;
            _figureRepository = figureRepository;
            _figureDotCount = figureDotCount;
            _curveComparer = curveComparer;
            _bendCountWeight = bendCountWeight;
            _bendsCounter = bendsCounter;
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
                float compareCurvesValue = _curveComparer.CompareCurves(points, figure.Points);
                int bandsCount = _bendsCounter.CountBends(points);
                int div = figure.BendsCount >= 1? figure.BendsCount : 1;
                float bandsError = (float)Mathf.Abs(bandsCount - figure.BendsCount) / div;
                bandsError *= _bendCountWeight;
                float error = compareCurvesValue + bandsError;
                Debug.Log("CompareCurves: " + compareCurvesValue);
                Debug.Log("bandsError: " + bandsError);
                Debug.Log("ID: " + figure.ID + " error: " + error);
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
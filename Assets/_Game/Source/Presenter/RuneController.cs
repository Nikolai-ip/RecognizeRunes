using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Source.Domain;
using _Game.Source.Domain.Utilities;
using _Game.Source.Infrastructure;
using Plugins.MVP;
using UnityEngine;

namespace _Game.Source.Application
{
    public class RuneController: MonoBehaviour
    {
        private Line _line;
        private IView<LineViewData> _lineView;
        private Camera _camera;
        private Recognizer _recognizer;
        [SerializeField] private List<LineRenderer> _debugLines;
        [SerializeField] private List<LineRenderer> _targetFigures;
        private IRepository<Figure> _repository;
        private IValidator<DrawValidationContext> _validator;
        
        private void Awake()
        {
            _lineView = ServiceLocator.Container.Resolve<IView<LineViewData>>();        
            _line = ServiceLocator.Container.Resolve<Line>();
            _camera = ServiceLocator.Container.Resolve<Camera>();
            _recognizer = ServiceLocator.Container.Resolve<Recognizer>();
            _repository = ServiceLocator.Container.Resolve<IRepository<Figure>>();
            _validator = ServiceLocator.Container.Resolve<IValidator<DrawValidationContext>>();         
        }

        private void Start()
        {
            for (int i = 0; i < _repository.Count(); i++)
            {
                DrawPoints(_repository.ElementAt(i).Points, _targetFigures[i]);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                if (_validator.IsValid(new DrawValidationContext(Input.mousePosition)) && _line.TryAddPoint(mousePos))
                {
                    _lineView.SetData(new LineViewData().OnPointAdded(mousePos));
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _lineView.SetData(new LineViewData().OnLineCleared());
                var copy = new List<Vector2>(_line.Points);
                copy = PointUtility.ResamplingPoints(copy, 64);
                DrawPoints(copy, _debugLines[0]);
                PointUtility.SquareScaling1X1(copy);
                DrawPoints(copy, _debugLines[1]);
                PointUtility.TranslateToCenter(copy);
                DrawPoints(copy, _debugLines[2]);
  

                var result = _recognizer.FindFigureByPoints(copy);
                Debug.Log(result);
                _line.Points.Clear();
            }
        }

        private void DrawPoints(List<Vector2> points, LineRenderer lineRenderer)
        {
            lineRenderer.positionCount = 0;
            for (var i = 0; i < points.Count; i++)
            {
                var point = points[i];
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(i, point);
            }
        }
    }
    
}
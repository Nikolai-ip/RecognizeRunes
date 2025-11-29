using System.Collections.Generic;
using System.Linq;
using _Game.Source.Domain;
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
        private IValidator<DrawValidationContext> _validator;
        
        [SerializeField] private List<LineRenderer> _targetFigures;
        private IRepository<Figure> _repository;
        
        private void Awake()
        {
            _lineView = ServiceLocator.Container.Resolve<IView<LineViewData>>();        
            _line = ServiceLocator.Container.Resolve<Line>();
            _camera = ServiceLocator.Container.Resolve<Camera>();
            _recognizer = ServiceLocator.Container.Resolve<Recognizer>();
            _validator = ServiceLocator.Container.Resolve<IValidator<DrawValidationContext>>();         
            _repository = ServiceLocator.Container.Resolve<IRepository<Figure>>();

        }
        private void Start()
        {
            for (int i = 0; i < _repository.Count(); i++)
            {
                DrawPoints(_repository.ElementAt(i).Points, _targetFigures[i]);
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
                _recognizer.FindFigureByPoints(_line.Points);
                _line.Points.Clear();
            }
        }
    }
    
}
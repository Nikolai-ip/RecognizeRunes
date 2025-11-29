using _Game.Source.Abstract.DomainGameplay;
using _Game.Source.Domain;
using _Game.Source.Infrastructure;
using _Game.Source.UseCases;
using _Game.Source.UseCases.RecognizeModule;
using Plugins.MVP;
using UnityEngine;

namespace _Game.Source.Presentation
{
    public class DrawRuneController: MonoBehaviour
    {
        private Line _line;
        private IView<LineViewData> _lineView;
        private Camera _camera;
        private Recognizer _recognizer;
        private IValidator<DrawValidationContext> _validator;
        
        
        private void Awake()
        {
            _lineView = ServiceLocator.Container.Resolve<IView<LineViewData>>();        
            _line = ServiceLocator.Container.Resolve<Line>();
            _camera = ServiceLocator.Container.Resolve<Camera>();
            _recognizer = ServiceLocator.Container.Resolve<Recognizer>();
            _validator = ServiceLocator.Container.Resolve<IValidator<DrawValidationContext>>();         
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
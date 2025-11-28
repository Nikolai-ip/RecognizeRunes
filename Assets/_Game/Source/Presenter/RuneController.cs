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

        private void Awake()
        {
            _lineView = ServiceLocator.Container.Resolve<IView<LineViewData>>();
            _line = ServiceLocator.Container.Resolve<Line>();
            _camera = ServiceLocator.Container.Resolve<Camera>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                if (_line.TryAddPoint(mousePos))
                {
                    _lineView.SetData(new LineViewData().OnPointAdded(mousePos));
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _lineView.SetData(new LineViewData().OnLineCleared());
            }
        }
    }
}
using System.Collections.Generic;
using Plugins.MVP;
using UnityEngine;

namespace _Game.Source.Presentation
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineView: MonoBehaviour, IView<LineViewData>
    {
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetData(LineViewData data)
        {
            switch (data.ViewAction)
            {
                case LineViewData.Action.AddPoint:
                    AddPointView(data.NewPoint);
                    break;
                case LineViewData.Action.ClearLine:
                    ClearLine();
                    break;
                case LineViewData.Action.RenderLine:
                    RenderLine(data.Points);
                    break;
            }    
        }

        private void RenderLine(List<Vector2> line)
        {
            ClearLine();
            for (int i = 0; i < line.Count; i++)
            {
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(i,  line[i]);
            }
        }

        private void ClearLine()
        {
            _lineRenderer.positionCount = 0;
        }

        private void AddPointView(Vector2 point)
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, point);
        }
    }

    public struct LineViewData
    {
        public enum Action
        {
            None,
            AddPoint,
            RenderLine,
            ClearLine
        }

        public Action ViewAction { get; private set; }
        public Vector2 NewPoint { get; private set; }
        public List<Vector2> Points { get; set; }

        public LineViewData OnPointAdded(Vector2 point)
        {
            NewPoint = point;
            ViewAction =  Action.AddPoint;
            return this;
        }

        public LineViewData OnLineCleared()
        {
            ViewAction = Action.ClearLine;
            return this;
        }

        public LineViewData OnRenderLine(List<Vector2> points)
        {
            Points = points;
            ViewAction = Action.RenderLine;
            return this;
        }

    }
}
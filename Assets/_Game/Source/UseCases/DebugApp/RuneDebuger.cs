using _Game.Source.Infrastructure.Data.StaticData;
using _Game.Source.Presentation;
using UnityEngine;

namespace _Game.Source.UseCases.DebugApp
{
    public class RuneDebuger: MonoBehaviour
    {
        [SerializeField] private LineView _lineView;
        [SerializeField] private Figure_SO _rune;
        [SerializeField] private bool _execute;
        private void OnValidate()
        {
            if (_execute)
            {
                _execute = false;
                _lineView.SetData(new LineViewData().OnRenderLine(_rune.GetFigure(64).Points));
            }
        }
        
    }
}
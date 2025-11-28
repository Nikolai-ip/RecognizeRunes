using System.Collections.Generic;
using System.Linq;
using _Game.Source.Domain;
using _Game.Source.Domain.Utilities;
using UnityEngine;

namespace _Game.Source.Data.StaticData
{
    [CreateAssetMenu(fileName = "Figure", menuName = "StaticData/Figure")]
    public class Figure_SO: ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private List<Vector2> _points;
        [SerializeField] private bool _needResampling = true;
        [SerializeField] private bool _needScaling = true;
        [SerializeField] private bool _needCentation = true;

        public Figure GetFigure(int figureDotCount)
        {
            var copiedPoints = new List<Vector2>(_points);
            if (_needResampling)
                copiedPoints = PointUtility.ResamplingPoints(copiedPoints, figureDotCount);
            if (_needScaling)
                PointUtility.SquareScaling1X1(copiedPoints);
            if (_needCentation)
                PointUtility.TranslateToCenter(copiedPoints);
            return new Figure(copiedPoints, _id);
        }
        
    }
}
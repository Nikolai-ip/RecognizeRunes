using System.Collections.Generic;
using System.Linq;
using _Game.Source.Domain;
using UnityEngine;

namespace _Game.Source.Infrastructure.Data.StaticData
{
    [CreateAssetMenu(fileName = "FigureRepository", menuName = "StaticData/FigureRepository")]
    public class FigureRepository_SO: ScriptableObject
    {
        [SerializeField] private List<Figure_SO> _figures;

        public FigureRepository GetRepository(int figureDotCount)
        {
            return new FigureRepository(_figures.Select(fSo => fSo.GetFigure(figureDotCount)).ToList());
        }
    }
}
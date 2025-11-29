using System.Collections.Generic;
using System.Linq;
using _Game.Source.Abstract.DomainGameplay;
using _Game.Source.Domain;
using _Game.Source.Infrastructure;
using _Game.Source.UseCases.RecognizeModule;
using NUnit.Framework;
using UnityEngine;

namespace _Game.Source
{
    public class Test: MonoBehaviour
    {
        [SerializeField] private GameObject _point;
        [SerializeField] private List<Vector2> _testFigure;
        private void Start()
        {
            var repository = ServiceLocator.Container.Resolve<IRepository<Figure>>();
            var recognizer = ServiceLocator.Container.Resolve<Recognizer>();
            var result = recognizer.FindFigureByPoints(_testFigure);
            Debug.Log(result.FigureID);
            Debug.Log(result.ErrorValue);
            // var test = new List<Vector2>()
            // {
            //     new Vector2(0, 0),
            //     new Vector2(10, 0)
            // };
            // test = PointUtility.ResamplingPoints(test, 5);
            // PointUtility.SquareScaling1X1(test);
            // test = PointUtility.ResamplingPoints(test, 5);
            // PointUtility.SquareScaling1X1(test);
        }

        private void ShowLine(List<Vector2> points, Color color, float size)
        {
            foreach (var p in points)
            {
                var point = Instantiate(_point, transform);
                point.transform.localScale = new Vector3(size, size, size);
                point.GetComponent<SpriteRenderer>().color = color;
                point.transform.position = p;
            }
        }   
    }
}
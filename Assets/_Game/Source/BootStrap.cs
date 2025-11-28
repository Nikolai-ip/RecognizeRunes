using _Game.Source.Application;
using _Game.Source.Data.StaticData;
using _Game.Source.Domain;
using _Game.Source.Infrastructure;
using Plugins.MVP;
using UnityEngine;

namespace _Game.Source
{
    [DefaultExecutionOrder(0)]
    public class BootStrap: MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private FigureRepository_SO _figureRepository_SO;
        [SerializeField] private float _minErrorValueToDetectFigure;
        [SerializeField] private int _figureDotCount = 64;

        [Header("UI")]
        [SerializeField] private LineView _lineView;
        [SerializeField] private float _lineDrawStep;
        [SerializeField] private Camera _camera;
        private void Awake()
        {
            RegisterCore();
            RegisterUI();
        }

        private void RegisterCore()
        {
            ServiceLocator.Container
                .RegisterSingle<IRepository<Figure>>(_figureRepository_SO.GetRepository(_figureDotCount));
            
            ServiceLocator.Container.RegisterSingle(c =>
                    new Recognizer(_minErrorValueToDetectFigure, _figureDotCount, c.Resolve<IRepository<Figure>>()));
        }

        private void RegisterUI()
        {
            ServiceLocator.Container.RegisterSingle<IView<LineViewData>>(_lineView);
            ServiceLocator.Container.RegisterSingle(new Line(_lineDrawStep));
            ServiceLocator.Container.RegisterSingle<Camera>(_camera);
        }
    }

    // public class EntryPoint
    // {
    //     [RuntimeInitializeOnLoadMethod]
    //     private static void Enter()
    //     {
    //         SceneManager.LoadScene("BootstrapScene");
    //     }
    // }
}
using System;
using System.Collections.Generic;
using _Game.Source.Abstract.DomainGameplay;
using _Game.Source.Domain;
using _Game.Source.Infrastructure;
using _Game.Source.Infrastructure.Data.StaticData;
using _Game.Source.Presentation;
using _Game.Source.Presentation.RuneList;
using _Game.Source.Presentation.RuneList.View;
using _Game.Source.UseCases;
using _Game.Source.UseCases.RecognizeModule;
using Plugins.MVP;
using UnityEngine;

namespace _Game.Source.Installers
{
    [DefaultExecutionOrder(0)]
    public class BootStrap: MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private FigureRepository_SO _figureRepository_SO;
        [SerializeField] private float _minErrorValueToDetectFigure;
        [SerializeField] private int _figureDotCount = 64;

        [Header("UI")] 
        [SerializeField] private RectTransform _drawArea;
        [SerializeField] private LineView _lineView;
        [SerializeField] private float _lineDrawStep;
        [SerializeField] private Camera _camera;
        [Space]
        [SerializeField] private RuneListView _availableRuneListView;
        [SerializeField] private RuneListView _drawnRuneListView;
        
        
        private readonly List<IDisposable> _disposables = new();
        private readonly List<IInitializable> _initializables = new();
        private void Awake()
        {
            RegisterCore();
            RegisterUI();
            RegisterApplication();
        }

        private void Start()
        {
            _initializables.ForEach(i => i.Initialize());
        }

        private void OnDestroy()
        {
            _disposables.ForEach(i => i.Dispose());
        }

        private void RegisterCore()
        {
            ServiceLocator.Container.RegisterSingle<ICurveComparer>(new SquareCurveMatcher());
            
            ServiceLocator.Container
                .RegisterSingle<IRepository<Figure>>(_figureRepository_SO.GetRepository(_figureDotCount));
            
            ServiceLocator.Container.RegisterSingle(c =>
                    new Recognizer(
                        _minErrorValueToDetectFigure, 
                        _figureDotCount, 
                        c.Resolve<IRepository<Figure>>(),
                        c.Resolve<ICurveComparer>()));
        }

        private void RegisterApplication()
        {
            ServiceLocator.Container.RegisterSingle<IValidator<DrawValidationContext>>(
                new DrawPointClickValidator(_drawArea));
        }

        private void RegisterUI()
        {
            ServiceLocator.Container.RegisterSingle<IView<LineViewData>>(_lineView);
            ServiceLocator.Container.RegisterSingle(new Line(_lineDrawStep));
            ServiceLocator.Container.RegisterSingle(_camera);

            var availableRuneListPresenter =
                new AvailableRuneListPresenter(ServiceLocator.Container.Resolve<IRepository<Figure>>(),
                    _availableRuneListView);

            var drawnRuneListPresenter =
                new DrawnRuneListPresenter(ServiceLocator.Container.Resolve<Recognizer>(), _drawnRuneListView);
            
            _initializables.Add(availableRuneListPresenter);
            _initializables.Add(drawnRuneListPresenter);
            _disposables.Add(drawnRuneListPresenter);
        }
    }
    
}
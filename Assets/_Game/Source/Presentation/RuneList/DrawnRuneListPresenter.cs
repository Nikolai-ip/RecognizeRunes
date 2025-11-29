using System;
using System.Collections.Generic;
using _Game.Source.Domain;
using _Game.Source.Infrastructure;
using _Game.Source.Presentation.RuneList.View;
using _Game.Source.UseCases.RecognizeModule;
using Plugins.MVP;

namespace _Game.Source.Presentation.RuneList
{
    public class DrawnRuneListPresenter: IInitializable, IDisposable
    {
        private readonly Recognizer _recognizer;
        private readonly List<string> _drawnRuneList = new();
        private readonly IView<RuneListViewData> _runeListView;
        private readonly IViewInteractable _resetButton;

        public DrawnRuneListPresenter(Recognizer recognizer, IView<RuneListViewData> runeListView, IViewInteractable resetButton)
        {
            _recognizer = recognizer;
            _runeListView = runeListView;
            _resetButton = resetButton;
        }

        public void Initialize()
        {
            _recognizer.OnFindFigureResultChanged += UpdateView;
            _resetButton.Callback += ResetList;
        }
        
        private void UpdateView(FindFigureResult findFigureResult)
        {
            if (!findFigureResult.HasFound) return;
            if (!_drawnRuneList.Contains(findFigureResult.FigureID))
            {
                _drawnRuneList.Add(findFigureResult.FigureID);
                _runeListView.SetData(new RuneListViewData(_drawnRuneList));
            }
        }
        
        private void ResetList()
        {
            _drawnRuneList.Clear();
            _runeListView.SetData(new RuneListViewData(_drawnRuneList));
        }

        public void Dispose()
        {
            _recognizer.OnFindFigureResultChanged -= UpdateView;
            _resetButton.Callback -= ResetList;
        }
    }
}
using System;
using System.Collections.Generic;
using _Game.Source.Domain;
using _Game.Source.Infrastructure;
using _Game.Source.Presenter.RuneList.View;
using Plugins.MVP;

namespace _Game.Source.Application.RuneList
{
    public class DrawnRuneListPresenter: IInitializable, IDisposable
    {
        private readonly Recognizer _recognizer;
        private readonly List<string> _drawnRuneList = new();
        private readonly IView<RuneListViewData> _runeListView;

        public DrawnRuneListPresenter(Recognizer recognizer, IView<RuneListViewData> runeListView)
        {
            _recognizer = recognizer;
            _runeListView = runeListView;
        }

        public void Initialize()
        {
            _recognizer.OnFindFigureResultChanged += UpdateView;
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

        public void Dispose()
        {
            _recognizer.OnFindFigureResultChanged -= UpdateView;
        }
    }
}
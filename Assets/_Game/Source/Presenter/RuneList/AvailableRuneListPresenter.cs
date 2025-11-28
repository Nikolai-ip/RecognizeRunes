using System.Linq;
using _Game.Source.Domain;
using _Game.Source.Infrastructure;
using _Game.Source.Presenter.RuneList.View;
using Plugins.MVP;

namespace _Game.Source.Application.RuneList
{
    public class AvailableRuneListPresenter: IInitializable
    {
        private readonly IRepository<Figure> _runeRepository;
        private readonly IView<RuneListViewData> _runeListView;

        public AvailableRuneListPresenter(IRepository<Figure> runeRepository, IView<RuneListViewData> runeListView)
        {
            _runeRepository = runeRepository;
            _runeListView = runeListView;
        }

        public void Initialize()
        {
            _runeListView.SetData(new RuneListViewData(_runeRepository.Select(rune=> rune.ID).ToList()));
        }
    }
}
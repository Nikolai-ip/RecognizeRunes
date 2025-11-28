using System;
using System.Collections.Generic;
using _Game.Source.Data.StaticData.UI;
using Plugins.MVP;
using UnityEngine;

namespace _Game.Source.Presenter.RuneList.View
{
    public class RuneListView: MonoBehaviour, IView<RuneListViewData>
    {
        [SerializeField] private RuneSpritesCatalogue_SO  _runeSpritesCatalogue_so;
        private Dictionary<string, Sprite> _runeSpritesCatalogue;
        [SerializeField] private List<RuneView> _runeViews;

        private void Awake()
        {
            _runeSpritesCatalogue = _runeSpritesCatalogue_so.GetRuneSprites();
            DisableAllRuneViews();
        }

        private void DisableAllRuneViews() => _runeViews.ForEach(rune => rune.Hide());

        public void SetData(RuneListViewData data)
        {
            DisableAllRuneViews();
            if (data.RuneIds.Count > _runeViews.Count)
                throw new InvalidOperationException(
                    $"Too many runes: {data.RuneIds.Count}, but view supports only {_runeViews.Count}");
                
            
            for (int i = 0; i < data.RuneIds.Count; i++)
            {
                _runeViews[i].Show();
                if (_runeSpritesCatalogue.TryGetValue(data.RuneIds[i], out Sprite runeSprite))
                    _runeViews[i].SetData(new RuneViewData(runeSprite));
                else
                    throw new KeyNotFoundException($"Rune sprites with id: {data.RuneIds[i]} is not found");
                
            }
        }
    }

    public struct RuneListViewData
    {
        public List<string> RuneIds { get; private set; }

        public RuneListViewData(List<string> runeIds)
        {
            RuneIds = runeIds;
        }
    }
}
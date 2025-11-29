using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Source.Infrastructure.Data.StaticData.UI;
using Plugins.MVP;
using UnityEngine;

namespace _Game.Source.Presentation.RuneList.View
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

        private void DisableAllRuneViews() => _runeViews.ForEach(rune => rune.DisableObject());

        public void SetData(RuneListViewData data)
        {
            if (data.RuneIds.Count > _runeViews.Count)
                throw new InvalidOperationException(
                    $"Too many runes: {data.RuneIds.Count}, but view supports only {_runeViews.Count}");

            int visibleElementCount = _runeViews.Count(e => e.IsVisible);
            int diff = data.RuneIds.Count - visibleElementCount;
            if (diff > 0)
            {
                for (int i = visibleElementCount; i < visibleElementCount + diff; i++)
                    ShowRuneView(data.RuneIds[i], i);
            }
            else if (diff < 0)
            {
                for (int i = visibleElementCount + diff; i < _runeViews.Count ; i++)
                {
                    _runeViews[i].Hide();
                }
            }
        }

        private void ShowRuneView(string id, int i)
        {
            _runeViews[i].Show();
            if (_runeSpritesCatalogue.TryGetValue(id, out Sprite runeSprite))
                _runeViews[i].SetData(new RuneViewData(runeSprite));
            else
                throw new KeyNotFoundException($"Rune sprites with id: {id} is not found");
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
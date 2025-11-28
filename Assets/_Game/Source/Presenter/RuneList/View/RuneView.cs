using Plugins.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Source.Presenter.RuneList.View
{
    public class RuneView : MonoBehaviour, IViewEnableable<RuneViewData>
    {
        [SerializeField] private Image _image;
        public void SetData(RuneViewData data)
        {
            _image.sprite = data.RuneSprite;
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }

    public struct RuneViewData
    {
        public Sprite RuneSprite { get; private set; }

        public RuneViewData(Sprite runeSprite)
        {
            RuneSprite = runeSprite;
        }
    }
}
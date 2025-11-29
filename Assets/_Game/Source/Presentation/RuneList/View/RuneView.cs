using System;
using Plugins.DOTweenFramework;
using Plugins.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Source.Presentation.RuneList.View
{
    public class RuneView : MonoBehaviour, IViewEnableable<RuneViewData>
    {
        [SerializeField] private Image _image;
        [SerializeField] private TweenAnimation _appearAnimation;
        [SerializeField] private TweenAnimation _disappearAnimation;
        public bool IsVisible { get; private set; }
        public void SetData(RuneViewData data)
        {
            _image.sprite = data.RuneSprite;
        }

        private void OnEnable()
        {
            _disappearAnimation.OnFinished += DisableObject;
        }
        private void OnDisable()
        {
            _disappearAnimation.OnFinished -= DisableObject;
        }

        public void Show()
        {
            IsVisible  = true;
            gameObject.SetActive(true);
            _appearAnimation.Play();
        }

        public void Hide()
        {
            IsVisible  = false;
            _disappearAnimation.Play();
        }
        public void DisableObject()
        {
            gameObject.SetActive(false);
        }
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
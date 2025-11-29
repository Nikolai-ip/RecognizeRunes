using System;
using Plugins.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Source.Presentation.CommonViews
{
    public class ButtonView: MonoBehaviour, IViewInteractable
    {
        public event Action Callback;
        private Button _button;

        private void Awake() => _button = GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);

        private void OnButtonClick() => Callback?.Invoke();

        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);
    }
}
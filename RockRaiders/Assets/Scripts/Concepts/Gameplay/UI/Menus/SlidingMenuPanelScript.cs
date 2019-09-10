using System;
using Assets.Scripts.Concepts.Gameplay.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Concepts.Gameplay.UI.Menus
{
    class SlidingMenuPanelScript : MonoBehaviour, IOpenableInterface
    {
        public Button[] ChildButtons { get; set; }
        public bool IsOpenByDefault = false;
        public bool IsOpen { get; set; }
        public bool IsOpening { get; private set; }
        public bool IsClosing { get; private set; }

        public const float MenuSlideSpeed = 400;

        public const float DefaultXOffset = 35;
        public const float DefaultYOffset = 15;

        private Vector2 _showPosition;
        private Vector2 _hidePosition;

        void Start()
        {
            ChildButtons = gameObject.transform.GetComponentsInChildren<Button>();
            RectTransform = gameObject.GetComponent<RectTransform>();
            RectTransform.anchoredPosition = _hidePosition = new Vector2((RectTransform.sizeDelta.x / 2) + DefaultXOffset, -(RectTransform.sizeDelta.y / 2) - DefaultYOffset);
            _showPosition = new Vector2(-(RectTransform.sizeDelta.x / 2) - DefaultXOffset, -(RectTransform.sizeDelta.y / 2) - DefaultYOffset);
            if (IsOpenByDefault) Show();
            else DisableButtons();
        }

        public RectTransform RectTransform { get; set; }

        void Update()
        {
            if (IsOpening)
            {
                RectTransform.anchoredPosition = Vector2.MoveTowards(RectTransform.anchoredPosition, _showPosition, Time.deltaTime * MenuSlideSpeed);
                if (RectTransform.anchoredPosition == _showPosition) IsOpening = false;
            }
            else if (IsClosing)
            {
                RectTransform.anchoredPosition = Vector2.MoveTowards(RectTransform.anchoredPosition, _hidePosition, Time.deltaTime * MenuSlideSpeed);
                if (RectTransform.anchoredPosition == _hidePosition) IsClosing = false;
            }
        }

        public void Show()
        {
            IsClosing = false;
            IsOpening = IsOpen = true;
            EnableButtons();

        }

        private void EnableButtons()
        {
            foreach (var childButton in ChildButtons) childButton.interactable = true;
        }

        public void Hide()
        {
            IsClosing = true;
            IsOpening = IsOpen = false;
            DisableButtons();
        }

        private void DisableButtons()
        {
            foreach (var childButton in ChildButtons) childButton.interactable = false;
        }
    }
}

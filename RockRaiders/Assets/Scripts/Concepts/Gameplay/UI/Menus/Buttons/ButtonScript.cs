using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Concepts.Gameplay.UI.Menus.Buttons
{
    class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public Button Button { get; set; }
        public Image ButtonImage { get; set; }
        public static float ScaleFactor = 0.9f;
        private Vector3? _cachedScale;

        void Start()
        {
            Button = gameObject.GetComponent<Button>();
            ButtonImage = gameObject.transform.GetComponentsInChildren<Image>().FirstOrDefault();
            ButtonImageTransform = ButtonImage.transform.GetChild(0);
        }

        public Transform ButtonImageTransform { get; set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            _cachedScale = ButtonImageTransform.localScale;
            ButtonImageTransform.localScale *= ScaleFactor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_cachedScale != null)
            {
                ButtonImageTransform.localScale = _cachedScale.Value;
                _cachedScale = null;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_cachedScale != null)
            {
                ButtonImageTransform.localScale = _cachedScale.Value;
                _cachedScale = null;
            }
        }
    }
}

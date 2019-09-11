using Assets.Scripts.Concepts.Gameplay.Building.BuildingType;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Concepts.Gameplay.UI.Menus.Buttons
{
    class OpenMenuButtonScript : MonoBehaviour, IPointerClickHandler
    {
        public GameObject MenuToOpen;
        private SlidingMenuPanelScript _localSlidingMenuPanelScript;
        private SlidingMenuPanelScript _targetSlidingMenuPanelScript;

        void Start()
        {
            _localSlidingMenuPanelScript = gameObject.transform.parent.GetComponent<SlidingMenuPanelScript>();
            _targetSlidingMenuPanelScript = MenuToOpen?.GetComponent<SlidingMenuPanelScript>();
        }

        public virtual void OnPointerClick(PointerEventData pointerEventData)
        {
            if (_localSlidingMenuPanelScript != null) _localSlidingMenuPanelScript.Hide();
            if (_targetSlidingMenuPanelScript != null) _targetSlidingMenuPanelScript.Show();
        }
    }
}
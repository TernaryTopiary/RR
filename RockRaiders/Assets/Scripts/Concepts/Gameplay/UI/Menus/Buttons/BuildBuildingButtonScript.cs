using Assets.Scripts.Concepts.Gameplay.Building.BuildingType;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Concepts.Gameplay.UI.Menus.Buttons
{
    class BuildBuildingButtonScript : MonoBehaviour
    {
        public BuildingType BuildingTypeToBuild;
        public Scripts.Map Map = Scripts.Map.GetInstance();
        public MapInteractor MapInteractor => Map.MapInteractor;

        void Start()
        {
            var buttonScript = gameObject.GetComponent<ButtonScript>();
            buttonScript.OnMouseUp += OnPointerClick;
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            // TODO: Change Mouse Cursor State
            MapInteractor.StartBuildingPlacementMode(BuildingTypeToBuild);
        }
    }
}
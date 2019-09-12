using Assets.Scripts.Concepts.Gameplay.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{

    public class TileInteractor : MonoBehaviour
    {
        public Tile TileDefinition { get; internal set; }
        public GameObject TilePhysicality { get; internal set; }
        public MapInteractor MapInteractor { get; set; }
        public TileAppearanceManager TileAppearanceManager { get; internal set; }
        public bool IsSelected { get; private set; }

        public TileInteractor(Tile tileDefinition)
        {
            TileDefinition = tileDefinition;
        }

        void OnMouseUp()
        {
            var isOverObject = EventSystem.current?.IsPointerOverGameObject();
            if (isOverObject == true) return;
            MapInteractor.MouseUp();
        }

        public void Select()
        {
            if (IsSelected) return;
            IsSelected = true;
            if (TileDefinition.TileType.GetType().GetInterfaces().Contains(typeof(ISelectable)))
            {
                if (TileDefinition.IsGround)
                {
                    TileAppearanceManager.SetTileOverlay(TileOverlayType.Selected);
                }
                if (TileDefinition.IsActiveWall)
                {
                    // Add reinforce/dynamite/mine/etc.
                    TileAppearanceManager.SetTileOverlay(TileOverlayType.Selected);
                }
            }
        }

        public void Unselect()
        {
            IsSelected = false;
            if (TileDefinition.IsGround)
            {
                TileAppearanceManager.SetTileOverlay(null);
            }
            if (TileDefinition.IsActiveWall)
            {
                // Add reinforce/dynamite/mine/etc.
                //TileAppearanceManager.SetOverlayMaterial(TileOverlayType.Selected);
            }
        }
    }
}

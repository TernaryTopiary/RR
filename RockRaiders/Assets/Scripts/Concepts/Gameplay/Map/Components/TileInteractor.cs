using Assets.Scripts.Concepts.Gameplay.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{

    public class TileInteractor : MonoBehaviour
    {
        public event Action<TileInteractor, Tile> OnClickEvent, OnMouseEnterEvent, OnMouseExitEvent;

        public Tile TileDefinition { get; internal set; }
        public GameObject TilePhysicality { get; internal set; }
        public MapInteractor MapInteractor { get; private set; }
        public TileAppearanceManager TileAppearanceManager { get; internal set; }

        public TileInteractor(Tile tileDefinition)
        {
            TileDefinition = tileDefinition;
        }

        //public MapScript MapReference;

        // Use this for initialization
        void Start()
        {
            MapInteractor = MapInteractor.GetInstance();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseEnter()
        {
        }

        void OnMouseExit()
        {
        }

        void OnMouseUp()
        {
            OnClickEvent?.Invoke(this, TileDefinition);
            MapInteractor.SelectTile(this);
        }

        public void Select()
        {
            if (TileDefinition.IsGround)
            {
                TileAppearanceManager.SetTileOverlay(TileOverlay.Selected);
            }
            if (TileDefinition.IsActiveWall)
            {
                // Add reinforce/dynamite/mine/etc.
                //TileAppearanceManager.SetOverlayMaterial(TileOverlay.Selected);
            }
        }

        private void Unselect()
        {
            if (TileDefinition.IsGround)
            {
                TileAppearanceManager.SetTileOverlay(null);
            }
            if (TileDefinition.IsActiveWall)
            {
                // Add reinforce/dynamite/mine/etc.
                //TileAppearanceManager.SetOverlayMaterial(TileOverlay.Selected);
            }
        }
    }
}

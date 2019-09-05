using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    class TileInteractor : MonoBehaviour
    {
        public event Action<Tile, TileInteractor> OnClickEvent, OnMouseEnterEvent, OnMouseExitEvent;

        public Tile TileDefinition { get; }

        public TileInteractor(Tile tileDefinition)
        {
            TileDefinition = tileDefinition;
        }

        //public MapScript MapReference;

        // Use this for initialization
        void Start()
        {

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
            //OnClickEvent()
            if (this.name.Contains("selectableMapTile"))
            {
                TileClick();
            }
        }

        private void TileClick()
        {
            //if (MapReference != null) MapReference.TileClickEvent(gameObject);
        }

        //private void Unselect()
        //{
        //    var mr = gameObject.GetComponent<SkinnedMeshRenderer>();
        //    var mats = mr.materials.ToList();
        //    mr.materials = mats.Except(mats.Where(mat => mat.name.Contains(MapConstants.TintSelected.name))).ToArray();
        //}
    }
}

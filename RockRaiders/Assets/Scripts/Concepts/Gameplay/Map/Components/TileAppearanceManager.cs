using UnityEngine;
using EnumsNET;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public class TileAppearanceManager : MonoBehaviour
    {
        public SkinnedMeshRenderer MeshRenderer { get; internal set; }
        public IEnumerable<Material> ActiveMaterials => MeshRenderer.materials;
        public List<Material> OverlayMaterials = new List<Material>();

        public void SetTileOverlay(TileOverlay? overlay)
        {
            if(!overlay.HasValue)
            {
                MeshRenderer.materials = ActiveMaterials.Except(OverlayMaterials).ToArray();
            }
            if(overlay.Value.HasFlag(TileOverlay.Selected))
            {
                if (!ActiveMaterials.Contains(MaterialManager.Constants.Gameplay.Map.TintSelected))
                {
                    MeshRenderer.materials = ActiveMaterials.Concat(MaterialManager.Constants.Gameplay.Map.TintSelected).ToArray();
                    OverlayMaterials.Add(MaterialManager.Constants.Gameplay.Map.TintSelected);
                }
            }
        }
    }
}

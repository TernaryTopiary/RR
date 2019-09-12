using UnityEngine;
using EnumsNET;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using System;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public class TileAppearanceManager : MonoBehaviour
    {
        public Tile TileDefinition { get; internal set; }
        public SkinnedMeshRenderer MeshRenderer { get; internal set; }
        public Material[] DefaultMaterials { get; set; }
        public Material[] ActiveMaterials => MeshRenderer.materials;
        public List<string> OverlayMaterialNames = new List<string>();  // Use the names as we can't take the reference and guarantee it's held.
        
        public void SetTileOverlay(TileOverlayType? overlay)
        {
            if (DefaultMaterials == null) DefaultMaterials = (Material[])ActiveMaterials.Clone();
            if (!overlay.HasValue)
            {   
                // Unity makes a copy of the material, so we'll filter them by name instead.
                MeshRenderer.materials = ActiveMaterials.Where(mat => OverlayMaterialNames.All(name => !mat.name.StartsWith(name))).ToArray();
                return;
            }
            if (overlay.Value.HasFlag(TileOverlayType.Selected))
            {
                if (!ActiveMaterials.Contains(MaterialManager.Constants.Gameplay.Map.TintSelected))
                {
                    MeshRenderer.materials = ActiveMaterials.Concat(MaterialManager.Constants.Gameplay.Map.TintSelected).ToArray();
                    OverlayMaterialNames.Add(MaterialManager.Constants.Gameplay.Map.TintSelected.name);
                }
            }
            if (overlay.Value.HasFlag(TileOverlayType.Foundation))
            {
                if (!ActiveMaterials.Contains(MaterialManager.Constants.Gameplay.Map.BuildingFoundation))
                {
                    MeshRenderer.materials = ActiveMaterials.Except(DefaultMaterials).Concat(MaterialManager.Constants.Gameplay.Map.BuildingFoundation).ToArray();
                    OverlayMaterialNames.Add(MaterialManager.Constants.Gameplay.Map.BuildingFoundation.name);
                }
            }
        }
    }
}

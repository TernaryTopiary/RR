using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Concepts.Gameplay.Map.Components;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public static class TileExtensions
    {
        public static GameObject ToGameObject(this Tile tile)
        {
            var gameObject = new GameObject { name = "mapTile_" };

            var scriptManager = gameObject.AddComponent<TileScriptManager>();
            scriptManager.Physicality = gameObject;
            scriptManager.RegisterBehavior(gameObject.AddComponent<TileInteractor>());

            //// Add reference for the map to the map script.
            //scriptManager.MapReference = MapRoot.GetComponent<MapScript>();

            if (!(gameObject.AddComponent(typeof(SkinnedMeshRenderer)) is SkinnedMeshRenderer meshRenderer))
                throw new InvalidCastException("Unable to cast renderer to SkinnedMeshRenderer.");
            meshRenderer.updateWhenOffscreen = true;
            meshRenderer.material = tile.GetMaterial();

            var verts = tile.Verticies.ToArray();
            var indicies = tile.Indicies.ToArray();
            // The verticies of the mesh with no Y components. 
            var uvs = verts.Select(vert => new Vector2(vert.x, vert.z)).ToArray();

            var mesh = new Mesh
            {
                vertices = verts,
                triangles = indicies,
                uv = uvs
            };
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            // Set up game object with mesh;
            meshRenderer.sharedMesh = mesh;
            return gameObject;
        }
    }
}

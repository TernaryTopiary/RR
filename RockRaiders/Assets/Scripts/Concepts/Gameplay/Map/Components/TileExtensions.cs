using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public static class TileExtensions
    {

        public static void SetVertexHeightsFromNeighbors(this Tile tile, AdjoiningTilesGrid9 neighbors)
        {
            var color = UnityEngine.Random.ColorHSV();

            foreach (var cornerOrientation in Enum.GetValues(typeof(CornerOrientation)).OfType<CornerOrientation>())
            {
                var vert = tile.GetVertexAt(cornerOrientation);

                if (tile.IsCeiling)
                {
                    tile.SetVertexAt(cornerOrientation, new Vector3(vert.x, vert.y + Tile.DefaultTileVerticalHeight, vert.z));
                    continue;
                }
                if (tile.IsGround) continue;

                float GetTileHeight(Tile t)
                {
                    if (t == null) return 0;
                    return t.IsCeiling ? t.OriginalTileHeight + Tile.DefaultTileVerticalHeight : t.OriginalTileHeight;
                }

                var decomposed = cornerOrientation.ToCandidateOrientations().Where(or => Enum.TryParse<CompassAxisOrientation>(or.ToString(), out _));//.Except(CompassOrientation.None);
                var cornerTiles = decomposed.ToDictionary(or => or, or => neighbors[or]);
                if (cornerTiles.Values.Any(t => t.IsCeiling)) tile.SetVertexAt(cornerOrientation, new Vector3(vert.x, vert.y + Tile.DefaultTileVerticalHeight, vert.z));
                if (cornerTiles.Values.Count(t => t.IsActiveWall) >= 2 && !neighbors[cornerOrientation.ToCompassOrientation()].IsGround) tile.SetVertexAt(cornerOrientation, new Vector3(vert.x, vert.y + Tile.DefaultTileVerticalHeight, vert.z));

                if (false) // Annotate map.
                {
                    var compassOr = cornerOrientation.ToCompassOrientation();
                    var nudge = -compassOr.ToOffsetVector3().normalized;

                    Debug.DrawLine(vert, vert + (nudge * 0.2f), color, 20);
                    Assets.Scripts.Map.DrawTextAtLocation($"{cornerOrientation.ToPrefix()}: {vert.y + Tile.DefaultTileVerticalHeight.ToString()}",
                        vert + (nudge * 0.2f), color);

                }
            }
        }

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

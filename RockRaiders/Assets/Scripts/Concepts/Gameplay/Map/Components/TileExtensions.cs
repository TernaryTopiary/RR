using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Cosmic.Space;

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
                var cornerQuad = neighbors[cornerOrientation];

                float GetTileHeight(Tile t)
                {
                    if (t == null) return 0;
                    return t.IsCeiling ? t.OriginalTileHeight + Tile.DefaultTileVerticalHeight : t.OriginalTileHeight;
                }

                var quadTileHeights = cornerQuad.Tiles.Select(GetTileHeight).ToArray();
                var averageTileHeight = quadTileHeights.Average(f => f);
                var averageCornerTileHeight = averageTileHeight;
                tile.SetVertexAt(cornerOrientation, new Vector3(vert.x, averageCornerTileHeight, vert.z));

                //if (yes < 4)
                {
                    var compassOr = cornerOrientation.ToCompassOrientation();
                    var nudge = -compassOr.ToOffsetVector3().normalized;

                    Debug.DrawLine(vert, vert + nudge, color, 20);
                    DrawTextAtLocation($"{cornerOrientation.ToPrefix()}: {averageCornerTileHeight.ToString()}",
                        tile.GetVertexAt(cornerOrientation)/* + (nudge.normalized * 0.2f)*/, color);

                    //yes = yes + 1;
                }
            }
        }

        public static int yes = 0;

        public static void DrawTextAtLocation(string str, Vector3 position, Color? color = null)
        {
            var textObject = new GameObject();
            textObject.transform.position = position;

            var textMesh = textObject.AddComponent<TextMesh>();
            textMesh.text = str;
            textMesh.characterSize = .05f;
            if (color.HasValue) textMesh.color = color.Value;
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

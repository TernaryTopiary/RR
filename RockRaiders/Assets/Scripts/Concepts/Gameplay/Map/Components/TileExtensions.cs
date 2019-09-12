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
            var lowerRightNeighbors = neighbors[CornerOrientation.SouthEast];

            foreach (var cornerOrientation in Enum.GetValues(typeof(CornerOrientation)).OfType<CornerOrientation>())
            {
                var vert = tile.GetVertexAt(cornerOrientation);

                var decomposed = cornerOrientation.ToCandidateOrientations().ToArray();
                var cornerTiles = decomposed.ToDictionary(or => or, or => neighbors[or]);

                tile.SetVertexAt(cornerOrientation, new Vector3(vert.x, lowerRightNeighbors[cornerOrientation].OriginalTileHeight, vert.z));

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

                decomposed = cornerOrientation.ToCandidateOrientations().Where(or => Enum.TryParse<CompassAxisOrientation>(or.ToString(), out _)).ToArray();//.Except(CompassOrientation.None);
                var axialTiles = decomposed.ToDictionary(or => or, or => neighbors[or]);
                if (axialTiles.Values.Any(t => t.IsCeiling)) tile.SetVertexAt(cornerOrientation, new Vector3(vert.x, vert.y + Tile.DefaultTileVerticalHeight, vert.z));
                if (axialTiles.Values.Count(t => t.IsActiveWall) >= 2 && !neighbors[cornerOrientation.ToCompassOrientation()].IsGround) tile.SetVertexAt(cornerOrientation, new Vector3(vert.x, vert.y + Tile.DefaultTileVerticalHeight, vert.z));

                if (MaterialManager.Constants.Gameplay.Debug.ShowDebugAnnotations) // Annotate map.
                {
                    var compassOr = cornerOrientation.ToCompassOrientation();
                    var nudge = -compassOr.ToOffsetVector3().normalized;

                    Debug.DrawLine(vert, vert + (nudge * 0.2f), color, 20);
                    Assets.Scripts.Map.DrawTextAtLocation($"{cornerOrientation.ToPrefix()}: {vert.y + Tile.DefaultTileVerticalHeight.ToString()}",
                        vert + (nudge * 0.2f), color);

                }
            }
        }

        public static GameObject ToGameObject(this Tile tile, MapInteractor mapInteractor)
        {
            var tilePhysicality = new GameObject { name = $"{Tile.TileGameObjectNamePrefix}_" };

            var meshCollider = tilePhysicality.AddComponent<MeshCollider>();    // Ok, because tiles are simple.
            var meshRenderer = tilePhysicality.AddComponent<SkinnedMeshRenderer>();
            meshRenderer.updateWhenOffscreen = true;
            meshRenderer.material = tile.GetMaterial();
            
            var scriptManager = tilePhysicality.AddComponent<TileScriptManager>();
            scriptManager.Physicality = tilePhysicality;
            var tileApperanaceManager = tilePhysicality.AddComponent<TileAppearanceManager>();
            tileApperanaceManager.TileDefinition = tile;
            tileApperanaceManager.MeshRenderer = meshRenderer;
            var interactor = tilePhysicality.AddComponent<TileInteractor>();
            interactor.TileDefinition = tile;
            interactor.TilePhysicality = tilePhysicality;
            interactor.TileAppearanceManager = tileApperanaceManager;
            interactor.MapInteractor = mapInteractor;
            scriptManager.RegisterBehavior(interactor);
            scriptManager.RegisterBehavior(tileApperanaceManager);

            var verts = tile.Verticies.ToArray();
            var indicies = tile.Indicies.ToArray();
            // The verticies of the mesh with no Y components. 
            Vector2[] uvs;

            // Rotate texture to face tile orientation.
            if (tile.Orientation.HasValue && tile.Orientation != CompassAxisOrientation.South)
            {

                // First vert is the central vert. Skip it, otherwise there are problems.
                uvs = verts.Select(vert => new Vector2(vert.x, vert.z)).ToArray();
                var firstUv = uvs.First();
                uvs = uvs.Skip(1).ToArray();

                var orientation = tile.Orientation.Value;
                while (orientation != CompassAxisOrientation.South)
                {
                    orientation = orientation.Rotate(RotationalOrientation.Clockwise);
                    uvs = uvs.Spin(RotationalOrientation.Clockwise);
                }
                uvs = firstUv.Concat(uvs).ToArray();
            }
            else
            {
                uvs = verts.Select(vert => new Vector2(vert.x, vert.z)).ToArray();
            }

            var mesh = new Mesh
            {
                vertices = verts,
                triangles = indicies,
                uv = uvs
            };
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            // Set up game object with mesh;
            meshCollider.sharedMesh = meshRenderer.sharedMesh = mesh;
            return tilePhysicality;
        }
    }
}

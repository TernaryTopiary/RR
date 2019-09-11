using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Constants;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Concepts.Gameplay.Building.Effects
{
    public class BuildingTeleportFire : MonoBehaviour
    {
        public bool Visible { get; private set; }
        public float Opacity = 0f;
        public const float MinOpacity = 0f;
        public const float MaxOpacity = 1f;
        public static string TextureNameBase = "magicbarrier";
        public static int TextureCountSigFigs = 3;
        public static int TextureTilesCount = 64;
        public static List<string> TextureNames = Enumerable.Repeat(TextureNameBase, TextureTilesCount).Select((str, index) => string.Join("", str, index.ToString().PadLeft(TextureCountSigFigs, '0'))).ToList();
        public List<Texture2D> Textures => MaterialManager.Constants.Gameplay.Buildings.TeleportFireTextures;
        public Material TeleportFireMaterial => MaterialManager.Constants.Gameplay.Buildings.TeleportFire;

        public GameObject Physicality { get; private set; }
        public SkinnedMeshRenderer MeshRenderer { get; private set; }
        public Material ActiveMaterial { get; private set; }

        public static float EdgeDistance = 0.1f;
        public static float FireHeight = 0.5f;
        public readonly CompassOrientation DefaultDirection = CompassOrientation.South;
        public static readonly List<Vector3> DefaultVerticies = new List<Vector3>
        {
            new Vector3(0, 0, 0) ,
            new Vector3(0, FireHeight, 0) ,
            new Vector3(Constants.Constants.TileScale, FireHeight, 0),
            new Vector3(Constants.Constants.TileScale, 0, 0)
        };

        public static int[] DefaultTileIndicies = {
            0, 1, 2,
            2, 3, 0
        };

        public static Vector2[] DefaultUvs = DefaultVerticies.Select(vert => new Vector2(vert.x, vert.y > 0 ? 1 : 0)).ToArray();

        private int TextureIndex = 0;
        private float elapsedTime = 0.0f;
        public float TextureDurationSeconds { get; set; } = 0.03f;

        void Start()
        {
            if(MaterialManager.IsLoaded) Create(DefaultVerticies.ToArray(), DefaultTileIndicies, DefaultUvs);
            Show();
            ActiveMaterial = MeshRenderer.material;
            InvokeRepeating(nameof(IncrementTexture), 0.0f, TextureDurationSeconds);
        }

        void Update()
        {
            if (Visible)
            {
                if (Opacity < MaxOpacity)
                {

                }
            }
        }

        private void IncrementTexture()
        {
            if (!Visible) return;
            ActiveMaterial.mainTexture = Textures[TextureIndex];
            TextureIndex += 1;
            if (TextureIndex >= TextureTilesCount) TextureIndex = 0;
        }

        public GameObject Create(Vector3[] verts, int[] indicies, Vector2[] uvs)
        {
            var tilePhysicality = Physicality = new GameObject { name = "teleportFire_" };
            var meshRenderer = MeshRenderer = tilePhysicality.AddComponent<SkinnedMeshRenderer>();
            meshRenderer.updateWhenOffscreen = true;
            meshRenderer.material = TeleportFireMaterial;

            var mesh = new Mesh
            {
                vertices = verts,
                triangles = indicies,
                uv = uvs
            };
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            meshRenderer.sharedMesh = mesh;
            return tilePhysicality;
        }

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }
    }
}

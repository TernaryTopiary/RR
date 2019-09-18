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
        public Material TeleportFireMaterial => MaterialManager.Constants.Gameplay.Buildings.TeleportFire;

        public GameObject Physicality { get; private set; }
        public SkinnedMeshRenderer MeshRenderer { get; private set; }
        public FadeScript FadeScript { get; set; }
        public Material ActiveMaterial => MeshRenderer.material;

        public bool IsStarted { get; set; } = false;
        public Action Started;

        public static float FireHeight = 0.5f;
        public static readonly List<Vector3> DefaultVerticies = new List<Vector3>
        {
            new Vector3(0, 0, 0) ,
            new Vector3(0, FireHeight, 0) ,
            new Vector3(Constants.Constants.TileScale, FireHeight, 0),
            new Vector3(Constants.Constants.TileScale, 0, 0)
        };

        public static int[] DefaultTileIndicies = {
            0, 1, 2,
            2, 3, 0,
        };

        public static Vector2[] DefaultUvs = DefaultVerticies.Select(vert => new Vector2(vert.x, vert.y > 0 ? 1 : 0)).ToArray();

        void Start()
        {
            IsStarted = true;
            Started?.Invoke();
        }

        public GameObject Create (Vector3 vStart, Vector3 vEnd)
        {
            var v1 = vStart;
            var v2 = new Vector3(vStart.x, vStart.y + FireHeight, vStart.z);
            var v3 = new Vector3(vEnd.x, vEnd.y + FireHeight, vEnd.z);
            var v4 = vEnd;
            return Create(new[] {v1, v2, v3, v4}, DefaultTileIndicies, DefaultUvs);
        }

        private GameObject Create(Vector3[] verts, int[] indicies, Vector2[] uvs)
        {
            var tilePhysicality = Physicality = new GameObject { name = "teleportFire_" };
            var meshRenderer = MeshRenderer = tilePhysicality.AddComponent<SkinnedMeshRenderer>();
            meshRenderer.updateWhenOffscreen = true;
            FadeScript = tilePhysicality.AddComponent<FadeScript>();
            FadeScript.Material = meshRenderer.sharedMaterial = meshRenderer.material = TeleportFireMaterial;
            var fireTextureScript = tilePhysicality.AddComponent<TeleportFireTextureScript>();

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
            if (FadeScript.IsStarted) FadeScript.FadeIn();
            else FadeScript.Started += FadeScript.FadeIn;
        }

        public void Hide()
        {
            FadeScript.FadeOut();
        }
    }
}

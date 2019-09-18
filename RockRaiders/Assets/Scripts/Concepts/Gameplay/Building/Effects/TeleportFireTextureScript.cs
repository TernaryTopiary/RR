using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Constants;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Concepts.Gameplay.Building.Effects
{
    public class TeleportFireTextureScript : MonoBehaviour
    {
        private int TextureIndex = 0;
        private float elapsedTime = 0.0f;
        public float TextureDurationSeconds { get; set; } = 0.03f;
        public SkinnedMeshRenderer MeshRenderer { get; private set; }
        public FadeScript FadeScript { get; set; }
        public List<Texture2D> Textures => MaterialManager.Constants.Gameplay.Buildings.TeleportFireTextures;
        public static List<string> TextureNames = Enumerable.Repeat(TextureNameBase, TextureTilesCount).Select((str, index) => string.Join("", str, index.ToString().PadLeft(TextureCountSigFigs, '0'))).ToList();
        public static string TextureNameBase = "magicbarrier";
        public static int TextureCountSigFigs = 3;
        public static int TextureTilesCount = 64;
        void Start()
        {
            FadeScript = gameObject.GetComponent<FadeScript>();
            MeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
            MeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            MeshRenderer.receiveShadows = false;
            InvokeRepeating(nameof(IncrementTexture), 0.0f, TextureDurationSeconds);
        }

        private void IncrementTexture()
        {
            if (FadeScript?.IsFaded == true) return;
            MeshRenderer.material.mainTexture = Textures[TextureIndex];
            TextureIndex += 1;
            if (TextureIndex >= TextureTilesCount) TextureIndex = 0;
        }
    }
}
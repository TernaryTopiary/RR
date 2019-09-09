using Assets.Scripts.Concepts.Gameplay.Map.Components;
using UnityEngine;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Ground;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;

namespace Assets.Scripts
{
    public class MaterialAnimator : MonoBehaviour
    {
        private Material _waterTileMaterial;
        private Material _lavaTileMaterial;

        public float offset = 0.0f;
        public float scrollSpeed = .25f;

        private ITileBiome _biome;
        public ITileBiome Biome { get => _biome;
            internal set
            {
                _biome = value;
                var waterType = TileGroundWater.GetInstance();
                waterType.Biome = _biome;
                var lavaType = TileGroundLava.GetInstance();
                lavaType.Biome = _biome;
                _waterTileMaterial = MaterialManager.GetMaterialForTile(new Tile() { TileType = waterType });
                _lavaTileMaterial = MaterialManager.GetMaterialForTile(new Tile() { TileType = lavaType });
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Scroll the lava and water textures.
            offset += (Time.deltaTime * scrollSpeed) / 10.0f;
            _waterTileMaterial?.SetTextureOffset("_MainTex", new Vector2(2 * offset, 2 * offset));
            _lavaTileMaterial?.SetTextureOffset("_MainTex", new Vector2(offset, offset));
        }
    }
}
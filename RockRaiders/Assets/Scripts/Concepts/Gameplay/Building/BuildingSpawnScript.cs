using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public class BuildingSpawnScript : MonoBehaviour
    {
        public const float BuildingInitialSpawnOpacity = 0;
        public const float BuildingDefaultOpacity = 1;
        public const float DefaultSpawnDurationSeconds = 5;
        public const float BuildingChunkSpawnDelay = .5f;

        public BuildingType.BuildingType BuildingType;
        public bool IsSpawned = false;
        private bool _isSpawning, _isUnspawning;
        public float ElapsedTime { get; set; }

        public IEnumerable<BuildingNodeHelper> CompositeNodes { get; set; }
        public ParticleSystem[] ParticleSystems { get; set; }
        public Light[] Lights { get; set; }
        public TeleportFireManager TeleportFireManager { get; set; }
        public Vector2 Center { get; set; }

        public Action SpawnComplete;
        public Action UnspawnComplete;

        private void Start()
        {
            TeleportFireManager = gameObject.AddComponent<TeleportFireManager>();
            TeleportFireManager.SpawnScript = this;
            CompositeNodes = gameObject.GetComponentsInChildren<BuildingNodeHelper>();
            ParticleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
            Lights = gameObject.GetComponentsInChildren<Light>();

            ToggleLights(false);
            ToggleParticleSystems(false);
        }

        private void Update()
        {
            if (_isSpawning)
            {
                ElapsedTime += Time.deltaTime;
                if (CompositeNodes.All(component => component.IsSpawned))
                {
                    ElapsedTime = 0;
                    IsSpawned = true;
                    _isSpawning = false;
                    SpawnComplete?.Invoke();
                    ToggleLights(true);
                    ToggleParticleSystems(true);
                }
                else
                {
                    if (!TeleportFireManager.IsSpawned) TeleportFireManager.Spawn();
                    foreach (var component in CompositeNodes) if (!component.IsSpawning && component.SpawnStartDelaySeconds <= ElapsedTime) component.Spawn();
                }
            }
            else if (_isUnspawning)
            {
                ElapsedTime += Time.deltaTime;
                if (CompositeNodes.All(component => !component.IsSpawned))
                {
                    ElapsedTime = 0;
                    IsSpawned = false;
                    _isUnspawning = false;
                    UnspawnComplete?.Invoke();
                }
                else foreach (var component in CompositeNodes) if (!component.IsUnspawning) component.Unspawn();
            }
        }

        private void ToggleLights(bool enable)
        {
            foreach (var light in Lights) light.enabled = enable;
        }

        private void ToggleParticleSystems(bool enable)
        {
            if (enable) foreach (var system in ParticleSystems) system.Play();
            else foreach (var system in ParticleSystems) system.Pause();
        }

        public void Spawn()
        {
            _isSpawning = true;
            _isUnspawning = false;
        }

        public void Unspawn()
        {
            ToggleLights(false);
            ToggleParticleSystems(false);
            _isSpawning = false;
            _isUnspawning = true;
        }
    }
}
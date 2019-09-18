using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Concepts.Cosmic.Array;
using Assets.Scripts.Concepts.Cosmic.Space;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public class BuildingSpawnScript : MonoBehaviour
    {
        public const float BuildingInitialSpawnOpacity = 0;
        public const float BuildingDefaultOpacity = 1;
        public const float DefaultSpawnDurationSeconds = 5;
        public const float BuildingChunkSpawnDelay = .5f;

        public bool IsSpawned = false;
        private bool _isSpawning, _isUnspawning;
        public float ElapsedTime { get; set; }

        public IEnumerable<BuildingNodeHelper> CompositeNodes { get; set; }
        public ParticleSystem[] SpawnParticleSystems { get; set; }
        public ParticleSystem[] ParticleSystems { get; set; }
        public Light[] SpawnLights { get; set; }
        public Light[] Lights { get; set; }
        public TeleportFireManager TeleportFireManager { get; set; }
        public LightFlickerScript LightFlickerScript { get; set; }
        public Vector2 Center { get; set; }
        public AdjoiningGrid9<IBuildingTileLayout> Plan { get; set; }

        public Action Started;
        public Action SpawnComplete;
        public Action UnspawnComplete;

        private void Start()
        {
            TeleportFireManager = gameObject.AddComponent<TeleportFireManager>();
            TeleportFireManager.SpawnScript = this;
            LightFlickerScript = gameObject.AddComponent<LightFlickerScript>();
            CompositeNodes = gameObject.GetComponentsInChildren<BuildingNodeHelper>();
            var children = gameObject.transform.GetComponentsInChildren<Transform>().Select(child => child.gameObject);

            var particleCollection = children.FirstOrDefault(child => child.name.StartsWith("Particles"));
            var particleChildren = particleCollection.transform.GetComponentsInChildren<Transform>().Select(child => child.gameObject).ToArray();
            var spawnParticleCollection = particleChildren.FirstOrDefault(child => child.name.StartsWith("Spawn"));
            var normalParticleCollection = particleChildren.FirstOrDefault(child => child.name.StartsWith("Normal"));
            SpawnParticleSystems = spawnParticleCollection.GetComponentsInChildren<ParticleSystem>();
            ParticleSystems = normalParticleCollection.GetComponentsInChildren<ParticleSystem>();

            var lightCollection = children.FirstOrDefault(child => child.name.StartsWith("Lights"));
            var lightChildren = lightCollection.transform.GetComponentsInChildren<Transform>().Select(child => child.gameObject).ToArray();
            var spawnLightCollection = lightChildren.FirstOrDefault(child => child.name.StartsWith("Spawn"));
            var normalLightCollection = lightChildren.FirstOrDefault(child => child.name.StartsWith("Normal"));
            SpawnLights = spawnLightCollection.GetComponentsInChildren<Light>();
            Lights = normalLightCollection.GetComponentsInChildren<Light>();
            LightFlickerScript.Lights = Lights.Select(light => new Tuple<Light, float>(light, light.intensity)).ToArray();

            ToggleLights(false);
            ToggleParticleSystems(false);
            ToggleSpawnLights(false);
            ToggleSpawnParticleSystems(false);

            Started?.Invoke();
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
                    ToggleSpawnLights(false);
                    ToggleSpawnParticleSystems(false, false);
                    LightFlickerScript.FlickerLightsForDuration(.35f);
                    TeleportFireManager.HideAll();
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

        private void ToggleSpawnLights(bool enable)
        {
            foreach (var light in SpawnLights) light.enabled = enable;
        }

        private void ToggleParticleSystems(bool enable)
        {
            if (enable) foreach (var system in ParticleSystems) system.Play();
            else
                foreach (var system in ParticleSystems)
                {
                    system.Stop();
                    system.Clear();
                }
        }

        private void ToggleSpawnParticleSystems(bool enable, bool clear = true)
        {
            if (enable) foreach (var system in SpawnParticleSystems) system.Play();
            else
                foreach (var system in SpawnParticleSystems)
                {
                    system.Stop();
                    if(clear) system.Clear();
                }
        }

        public void Spawn()
        {
            ToggleLights(false);
            ToggleParticleSystems(false);
            ToggleSpawnLights(true);
            ToggleSpawnParticleSystems(true);
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
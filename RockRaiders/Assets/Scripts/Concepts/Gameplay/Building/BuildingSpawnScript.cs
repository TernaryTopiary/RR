using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Concepts.Cosmic.Space;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public class BuildingSpawnScript : MonoBehaviour
    {
        public static readonly Vector3 BuildingInitialSpawnOffset = new Vector3(0, 5, 0);
        public const float BuildingInitialSpawnOpacity = 0;
        public const float BuildingDefaultOpacity = 1;
        public const float DefaultSpawnDurationSeconds = 5;

        public float SpawnDurationSeconds = DefaultSpawnDurationSeconds;
        public BuildingType.BuildingType BuildingType;
        public bool IsSpawned = false;
        private bool _isSpawning, _isUnspawning;

        public IEnumerable<BuildingNodeHelper> CompositeNodes { get; set; }
        public Dictionary<BuildingNodeHelper, MeshRenderer[]> CompositeModels { get; set; }
        public ParticleSystem[] ParticleSystems { get; set; }
        public Light[] Lights { get; set; }

        public TeleportFireManager TeleportFireManager { get; set; }

        void Start()
        {
            TeleportFireManager = gameObject.AddComponent<TeleportFireManager>();
            CompositeNodes = gameObject.GetComponentsInChildren<BuildingNodeHelper>();
            CompositeModels = CompositeNodes.ToDictionary(node => node, node => node.transform.gameObject.GetComponentsInChildren<MeshRenderer>());
            ParticleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
            Lights = gameObject.GetComponentsInChildren<Light>();

            //if (!IsSpawned)
            //{
            //    foreach (var compositeModel in CompositeModels)
            //    {
            //        foreach (var model in compositeModel.Value)
            //        {
            //            model.transform.position = model.transform.position + BuildingInitialSpawnOffset;
            //        }
            //    }
            //    foreach (var system in ParticleSystems) system.Pause();
            //    foreach (var light in Lights) light.enabled = false;
            //}
        }

        public void Spawn()
        {

        }

        public void Unspawn()
        {

        }
    }

    public class TeleportFireManager : MonoBehaviour
    {
        public BuildingType.BuildingType BuildingType;

        void Start()
        {

        }


    }
}

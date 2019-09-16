using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Concepts.Cosmic.Space;
using UnityEngine;

using Assets.Scripts.Concepts.Cosmic.Space;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public class BuildingNodeHelper : MonoBehaviour
    {
        public static readonly Vector3 BuildingInitialSpawnOffset = new Vector3(0, 5, 0);
        public CompassOrientation NodeLocation;
        public float SpawnStartDelaySeconds = 0;
        public bool IsSpawned = false;
        public bool IsSpawning;
        public bool IsUnspawning;
        public float ElapsedTime { get; set; }
        public BuildingModelHelper[] ModelComponents { get; set; }

        public Action<GameObject> ModelLanded;
        public Action SpawnComplete;
        public Action UnspawnComplete;

        void Start()
        {
            ModelComponents = gameObject.GetComponentsInChildren<BuildingModelHelper>();
            foreach(var model in ModelComponents) model.SpawnComplete += () => ModelLanded?.Invoke(model.gameObject);
        }

        void Update()
        {
            if (IsSpawning)
            {
                ElapsedTime += Time.deltaTime;
                if (ModelComponents.All(component => component.IsSpawned))
                {
                    ElapsedTime = 0;
                    IsSpawned = true;
                    IsSpawning = false;
                    SpawnComplete?.Invoke();
                }
                else foreach (var modelComponent in ModelComponents) if (!modelComponent.IsSpawning && !modelComponent.IsSpawned && modelComponent.SpawnStartDelaySeconds <= ElapsedTime) modelComponent.Spawn();
            }
            else if (IsUnspawning)
            {
                ElapsedTime += Time.deltaTime;
                if (ModelComponents.All(component => !component.IsSpawned))
                {
                    ElapsedTime = 0;
                    IsSpawned = false;
                    IsUnspawning = false;
                    UnspawnComplete?.Invoke();
                }
                else foreach (var modelComponent in ModelComponents) if (!modelComponent.IsUnspawning) modelComponent.Unspawn();
            }
        }

        public void Spawn()
        {
            IsSpawning = true;
            IsUnspawning = false;
        }

        public void Unspawn()
        {
            IsSpawning = false;
            IsUnspawning = true;
        }
    }
}
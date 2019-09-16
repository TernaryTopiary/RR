using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Building.BuildingType;
using Assets.Scripts.Concepts.Gameplay.Building.Effects;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public class TeleportFireManager : MonoBehaviour
    {
        public const float DefaultFadeDurationSeconds = .5f;
        public const float DefaultSpawnDurationSeconds = 5;
        public BuildingType.BuildingType BuildingType;
        public bool IsSpawned = false;
        public bool IsSpawning;
        
        public List<BuildingTeleportFire> FireList = new List<BuildingTeleportFire>();
        public BuildingSpawnScript SpawnScript { get; set; }
        public Vector2 Center => SpawnScript.Center;
        public Scripts.Map Map = Scripts.Map.GetInstance();

        void Start()
        {
        }

        void Update()
        {
            //if (_isSpawning)
            //{
            //    ElapsedTime += Time.deltaTime;
            //    if (CompositeNodes.All(component => component.IsSpawned))
            //    {
            //        ElapsedTime = 0;
            //        IsSpawned = true;
            //        _isSpawning = false;
            //        SpawnComplete?.Invoke();
            //        ToggleLights(true);
            //        ToggleParticleSystems(true);
            //    }
            //    else foreach (var component in CompositeNodes) if (!component.IsSpawning && component.SpawnStartDelaySeconds <= ElapsedTime) component.Spawn();
            //}
        }

        public void Spawn()
        {
            IsSpawned = IsSpawning = true;

            var buildingMap = BuildingTypeHelper.BuildingTypeLookup[BuildingType].DefaultTileLayout;
            foreach (var kv in buildingMap)
            {
                if (kv.Value.Node != null)
                {
                    foreach (var targetAxisOrientation in Enum.GetValues(typeof(CompassAxisOrientation)).OfType<CompassAxisOrientation>())
                    {
                        var offsetOrientation = kv.Key.Add(targetAxisOrientation);
                        if (!offsetOrientation.HasValue || (!buildingMap.ContainsKey(offsetOrientation.Value) || buildingMap[offsetOrientation.Value].Node == null))
                        {
                            // If the building blueprint ends or there is no bounding building node in the neighbor space, put a firewall.
                            var firewall = gameObject.AddComponent<BuildingTeleportFire>();
                            var corners = targetAxisOrientation.Opposite().ToEdgeCorners();
                            var tile = Map.GetTileAtPosition(Center + offsetOrientation.Value.ToOffsetVector2(), false);
                            firewall.Create(tile.GetVertexAt(corners.First()), tile.GetVertexAt(corners.Last()));
                            FireList.Add(firewall);
                            firewall.Show();

                            firewall = gameObject.AddComponent<BuildingTeleportFire>();
                            corners = corners.Reverse().ToArray();
                            firewall.Create(tile.GetVertexAt(corners.First()), tile.GetVertexAt(corners.Last()));
                            FireList.Add(firewall);
                            firewall.Show();
                        }
                    }
                }
            }
        }


    }
}
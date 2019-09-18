using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Building.Effects;
using Assets.Scripts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public class TeleportFireManager : MonoBehaviour
    {
        public const float DefaultFadeDurationSeconds = .5f;
        public const float DefaultSpawnDurationSeconds = 5;
        public const float DefaultEdgeOffset = .1f;
        public bool IsSpawned = false;
        public bool IsSpawning;

        public List<BuildingTeleportFire> FireList = new List<BuildingTeleportFire>();
        public BuildingSpawnScript SpawnScript { get; set; }
        public Vector2 Center => SpawnScript.Center;
        public Scripts.Map Map = Scripts.Map.GetInstance();

        private void Start()
        {
        }

        private void Update()
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

            foreach (var kv in SpawnScript.Plan)
            {
                if (kv.Value.Node != null)
                {
                    foreach (var targetAxisOrientation in Enum.GetValues(typeof(CompassAxisOrientation)).OfType<CompassAxisOrientation>())
                    {
                        var offsetOrientation = kv.Key.Add(targetAxisOrientation);
                        if (!offsetOrientation.HasValue || (!SpawnScript.Plan.ContainsKey(offsetOrientation.Value) || SpawnScript.Plan[offsetOrientation.Value].Node == null))
                        {
                            // If the building blueprint ends or there is no bounding building node in the neighbor space, put a firewall.
                            var firewall = gameObject.AddComponent<BuildingTeleportFire>();
                            var corners = targetAxisOrientation.Opposite().ToEdgeCorners();
                            var tile = Map.GetTileAtPosition(Center + offsetOrientation.Value.ToOffsetVector2(), false);

                            var v1Orientation = corners.First();
                            var v2Orientation = corners.Last();
                            var v1 = tile.GetVertexAt(v1Orientation);
                            var v2 = tile.GetVertexAt(v2Orientation);

                            // Add an offset from the edge.
                            var relevantSides = Enum.GetValues(typeof(CompassAxisOrientation)).OfType<CompassAxisOrientation>().Except(new CompassAxisOrientation[] { targetAxisOrientation, targetAxisOrientation.Opposite() });
                            foreach (var side in relevantSides)
                            {
                                var tileLocation = kv.Key.Add(side);
                                var isFlushOnSide = tileLocation.HasValue &&
                                                    SpawnScript.Plan.ContainsKey(tileLocation.Value) &&
                                                    SpawnScript.Plan[tileLocation.Value].Node != null;
                                if (!isFlushOnSide)
                                {
                                    if (v1Orientation.ToCandidateOrientations().Contains(side.ToCompassOrientation())) v1 = v1 - (side.ToOffsetVector3() * DefaultEdgeOffset);
                                    else v2 = v2 - (side.ToOffsetVector3() * DefaultEdgeOffset);
                                    v1 = v1 - (targetAxisOrientation.ToOffsetVector3() * DefaultEdgeOffset / 2);
                                    v2 = v2 - (targetAxisOrientation.ToOffsetVector3() * DefaultEdgeOffset / 2);
                                }
                            }

                            firewall.Create(v1, v2);
                            FireList.Add(firewall);
                            firewall.Started += firewall.Show;

                            firewall = gameObject.AddComponent<BuildingTeleportFire>();
                            firewall.Create(v2, v1);
                            FireList.Add(firewall);
                            firewall.Started += firewall.Show;

                        }
                    }
                }
            }
        }

        public void HideAll()
        {
            foreach(var fire in FireList)
            {
                fire.Hide();
            }
        }
    }
}
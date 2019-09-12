using Assets.Scripts.Concepts.Cosmic.Space;
using System;
using Assets.Scripts.Concepts.Cosmic.Time;
using Assets.Scripts.Concepts.Gameplay.Resource;
using Assets.Scripts.Concepts.Gameplay.Shared;
using System.Collections.Generic;
using Assets.Scripts.Concepts.Gameplay.Building.BuildingType;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Building.Components
{
    public class Building : IDamageable, ICreatable, IRepairable, IExpensive, IDeactivatable, IUpgradable,
        ITakeTimeToCreateable, ITooltipInformationDisplayable
    {
        private Building()
        {

        }

        public int CurrentHitpoints { get; set; }
        public int MaxHitpoints { get; set; }
        public Dictionary<ResourceType, int> Cost { get; set; }
        public Boolean IsDeactivated { get; set; }
        public Seconds TimeToCreate { get; set; }
        public Vector2 CenterLocation { get; set; }
        public String TooltipText { get; set; }
        public Boolean IsTooltipVocalized { get; set; }
        public BuildingSpawnScript SpawnScript { get; set; }
        public GameObject BuildingRootObject { get; set; }
        public CompassAxisOrientation DefaultOrientation { get; } = CompassAxisOrientation.South;
        public CompassAxisOrientation CurrentOrientation { get; set; } = CompassAxisOrientation.South;


        public static Building FromType(BuildingType.BuildingType type)
        {
            var buildingType = BuildingTypeHelper.BuildingTypeLookup[type];
            var newBuilding = new Building();
            newBuilding.Cost = buildingType.Cost;
            newBuilding.CurrentHitpoints = newBuilding.MaxHitpoints = buildingType.DefaultHitpoints;
            newBuilding.TimeToCreate = buildingType.TimeToCreate;
            newBuilding.TooltipText = buildingType.TooltipText;
            newBuilding.IsTooltipVocalized = buildingType.IsTooltipVocalized;
            return newBuilding;
        }
    }
}
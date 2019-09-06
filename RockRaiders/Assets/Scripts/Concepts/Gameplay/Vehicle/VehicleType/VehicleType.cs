using System;
using System.Collections.Generic;
using Assets.Scripts.Concepts.Cosmic.Time;
using Assets.Scripts.Concepts.Gameplay.Resource;
using Assets.Scripts.Concepts.Gameplay.Shared;

namespace Assets.Scripts.Concepts.Gameplay.Vehicle.VehicleType
{
    public class VehicleTypeHoverScout : IVehicleTypeGround, ILightVehicleType, ITransportable
    {
        public string TooltipText { get; set; } = "Hover Scout";
        public bool IsTooltipVocalized { get; set; } = true;
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 1 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeSmallDigger : IVehicleTypeGround, ILightVehicleType, ITransportable
    {
        public string TooltipText { get; set; } = "Small Digger";
        public bool IsTooltipVocalized { get; set; } = true;
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 1 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeSmallTransportTruck : IVehicleTypeGround, ILightVehicleType, ITransportable
    {
        public string TooltipText { get; set; } = "Small Transport Truck";
        public bool IsTooltipVocalized { get; set; } = true;
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 2 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeRapidRider : IVehicleTypeWater, ILightVehicleType
    {
        public string TooltipText { get; set; } = "Rapid Rider";
        public bool IsTooltipVocalized { get; set; } = true;
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 2 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeSmallMobileLaserCutter : IVehicleTypeGround, ILightVehicleType, ITransportable
    {
        public string TooltipText { get; set; } = "Small Mobile Laser Cutter";
        public bool IsTooltipVocalized { get; set; } = true;
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 3 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeTunnelScout : IVehicleTypeGround, ILightVehicleType
    {
        public string TooltipText { get; set; } = "Tunnel Scout";
        public bool IsTooltipVocalized { get; set; } = true;
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 3 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeLoaderDozer : IVehicleTypeGround, IHeavyVehicleType
    {
        public string TooltipText { get; set; } = "Loader Dozer";
        public bool IsTooltipVocalized { get; set; }
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 4 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeGraniteGrinder : IVehicleTypeGround, IHeavyVehicleType
    {
        public string TooltipText { get; set; } = "Granite Grinder";
        public bool IsTooltipVocalized { get; set; }
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 3 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeLargeMobileLaserCutter : IVehicleTypeGround, IHeavyVehicleType
    {
        public string TooltipText { get; set; } = "Large Mobile Laser Cutter";
        public bool IsTooltipVocalized { get; set; }
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 4 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeChromeCrusher : IVehicleTypeGround, IHeavyVehicleType
    {
        public string TooltipText { get; set; } = "Chrome Crusher";
        public bool IsTooltipVocalized { get; set; }
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 5 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeCargoCarrier : IVehicleTypeWater, IHeavyVehicleType
    {
        public string TooltipText { get; set; } = "Cargo Carrier";
        public bool IsTooltipVocalized { get; set; }
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int> { { typeof(ResourceTypeCrystal), 4 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }

    public class VehicleTypeTunnelTransport : IVehicleTypeAir, IHeavyVehicleType
    {
        public string TooltipText { get; set; } = "Tunnel Transport";
        public bool IsTooltipVocalized { get; set; }
        public Dictionary<Type, int> Cost { get; set; }
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
        public int DefaultSpeed { get; set; }
        public int Capacity { get; set; } = 1;
    }
}
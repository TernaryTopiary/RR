using Assets.Scripts.Concepts.Cosmic.Array;
using Assets.Scripts.Concepts.Gameplay.Shared;

namespace Assets.Scripts.Concepts.Gameplay.Building.BuildingType
{
    public interface IBuildingType : ISelectable, ITooltipInformationDisplayable, IExpensive, ITakeTimeToCreateable, IDamageableDefinition, IRepairable, IUpgradable
    {
        BuildingType BuildingType { get; }

        AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; }
    }
}

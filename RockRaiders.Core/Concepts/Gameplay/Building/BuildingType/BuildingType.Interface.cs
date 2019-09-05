using Assets.Scripts.Concepts.Gameplay.Shared;

namespace Assets.Scripts.Concepts.Gameplay.Building.BuildingType
{
    public interface IBuildingType : ISelectable, ITooltipInformationDisplayable, IExpensive, ITakeTimeToCreateable, IDamageable, IRepairable, IUpgradable
    {
        //BuildingTileLayout[,] DefaultTileLayout { get; set; }
    }
}

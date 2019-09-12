using Assets.Scripts.Concepts.Gameplay.Shared;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public interface IBuildingType : ISelectable, ITooltipInformationDisplayable, IExpensive, ITakeTimeToCreateable, IDamageableDefinition, IRepairable, IUpgradable
    {
        //IBuildingTileLayout[,] DefaultTileLayout { get; set; }
        Vector3 BuildingPivotCoordinates { get; set; }
    }

    public static class BuildingTypeExtensions
    {
        //public static int BuildingWidth(this IBuildingType type) => type.DefaultTileLayout.GetLength(0);

        //public static int BuildingHeight(this IBuildingType type) => type.DefaultTileLayout.GetLength(1);
    }
}
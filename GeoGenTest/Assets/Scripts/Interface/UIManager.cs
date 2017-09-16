using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
    public GameObject 
        CancelBuildingMenu, 
        RootMenu, 
        ActionMenu, 
        BuildingMaintenanceMenu, 
        BuildingMenu, 
        LargeVehiclesMenu, 
        PathAndFenceMenu, 
        RadarPanel,
        SmallVehiclesMenu, 
        TrainingMenu, 
        VehicleActionMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HideAllOpenMenus()
    {
        var menus = new[]
        {
            CancelBuildingMenu,
            RootMenu,
            ActionMenu,
            BuildingMaintenanceMenu,
            BuildingMenu,
            LargeVehiclesMenu,
            PathAndFenceMenu,
            RadarPanel,
            SmallVehiclesMenu,
            TrainingMenu,
            VehicleActionMenu
        };
        foreach (var menu in menus)
        {
            menu.GetComponent<InterfaceInterface>().HideMenu();
        }
    }

    public void HideAllOpenMenusExcept(GameObject skipMe)
    {
        var menus = new[]
        {
            CancelBuildingMenu,
            RootMenu,
            ActionMenu,
            BuildingMaintenanceMenu,
            BuildingMenu,
            LargeVehiclesMenu,
            PathAndFenceMenu,
            RadarPanel,
            SmallVehiclesMenu,
            TrainingMenu,
            VehicleActionMenu
        };
        foreach (var menu in menus.Where(item => item != skipMe))
        {
            menu.GetComponent<InterfaceInterface>().HideMenu();
        }
    }

    public void ShowMenu(GameObject menu)
    {
        HideAllOpenMenusExcept(menu);
        menu.GetComponent<InterfaceInterface>().ShowMenu();
    }

    public void ShowBuildingMenu()
    {
        ShowMenu(BuildingMenu);
    }

    public void ShowBuildingMaintenanceMenu()
    {
        ShowMenu(BuildingMaintenanceMenu);
    }

    public void ShowSmallVehiclesMenu()
    {
        ShowMenu(SmallVehiclesMenu);
    }

    public void ShowLargeVehiclesMenu()
    {
        ShowMenu(LargeVehiclesMenu);
    }

    public void ShowVehicleActionMenu()
    {
        ShowMenu(VehicleActionMenu);
    }

    public void ShowTrainingMenu()
    {
        ShowMenu(TrainingMenu);
    }

    public void ShowPathAndFenceMenu()
    {
        ShowMenu(PathAndFenceMenu);
    }

    public void ShowRadarPanel()
    {
        ShowMenu(RadarPanel);
    }

    public void ShowActionMenu()
    {
        ShowMenu(ActionMenu);
    }

    public void ShowRootMenu()
    {
        ShowMenu(RootMenu);
    }

    public void ShowCancelBuildingMenu()
    {
        ShowMenu(CancelBuildingMenu);
    }

    public void ShowWallMenu()
    {
        throw new System.NotImplementedException();
    }
}

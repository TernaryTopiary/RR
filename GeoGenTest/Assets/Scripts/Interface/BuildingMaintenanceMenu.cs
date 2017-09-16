using UnityEngine;
using UnityEngine.UI;

public class BuildingMaintenanceMenu : MonoBehaviour, InterfaceInterface 
{
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        //get the animator component
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowMenu()
    {
        anim.SetBool("IsOpen", true);
        foreach (var child in GetComponentsInChildren<Button>())
            child.interactable = true;
    }

    public void HideMenu()
    {
        foreach (var child in GetComponentsInChildren<Button>())
            child.interactable = false;
        anim.SetBool("IsOpen", false);
    }

    public bool IsOpen
    {
        get { return anim.GetBool("IsOpen"); }
        set
        {
            if (value) ShowMenu();
            else HideMenu();
        }
    }
}

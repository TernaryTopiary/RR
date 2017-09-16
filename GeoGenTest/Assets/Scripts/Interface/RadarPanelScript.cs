using UnityEngine;
using UnityEngine.UI;

public class RadarPanelScript : MonoBehaviour, InterfaceInterface
{

    private Animator anim;

	// Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
	
	}

    public void ToggleRadarPanel()
    {
        anim.SetBool("IsOpen", !anim.GetBool("IsOpen"));
        foreach (var child in GetComponentsInChildren<Button>())
            child.interactable = anim.GetBool("IsOpen");
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


	// Update is called once per frame
	void Update () {
	
	}
}

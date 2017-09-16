using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RootMenuScript : MonoBehaviour, InterfaceInterface
{
    private Animator anim;

    // Use this for initialization
    private void Start()
    {
        //get the animator component
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
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

    public Vector3 showPosition = new Vector3(-35, -92, 0);
    public Vector3 hidePosition = new Vector3(60, -92, 0);

    //Time you want it to take before it reaches the object
    public float moveDuration = 0.5f;

    private IEnumerator Tween(Vector3 targetPosition)
    {
        //Obtain the previous position (original position) of the gameobject this script is attached to
        var previousPosition = gameObject.transform.position;
        //Create a time variable
        var time = 0.0f;
        do
        {
            //Add the deltaTime to the time variable
            time += Time.deltaTime;
            //Lerp the gameobject's position that this script is attached to. Lerp takes in the original position, target position and the time to execute it in
            gameObject.transform.position = Vector3.Lerp(previousPosition, targetPosition, time / moveDuration);
            yield return 0;
            //Do the Lerp function while to time is less than the move duration.
        } while (time < moveDuration);
    }
}
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //var hitInfo = new RaycastHit();
        //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
        {
            transform.LookAt(Camera.main.ScreenPointToRay (Input.mousePosition).GetPoint (1000));//hitInfo.transform);
        }
    }
}
using UnityEngine;

public class EscMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public GameObject Menu; // Assign in inspector
	private bool _isShowing;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("escape"))
		{
			_isShowing = !_isShowing;
			Menu.SetActive(_isShowing);
		}

	}
}

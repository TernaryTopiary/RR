using System;
using UnityEngine;
using System.Collections;

public class BuildingPartTeleportScript : MonoBehaviour {
    private bool _teleportIn, _teleportOut;
    public float Speed = _speedReferenceValue;
    private static readonly float _speedReferenceValue = 5f;
    public static float HideHeight = 3f;
    private Vector3 _referencePosition;
    public Action<BuildingPartTeleportScript> TeleportComplete = delegate { }, TeleportInComplete = delegate { }, TeleportOutComplete = delegate { };

    // Use this for initialization
	void Start ()
	{
        _referencePosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
	}
	
	// Update is called once per frame
    void FixedUpdate() 
    {
        if (_teleportIn)
        {
            if (transform.localPosition.y <= 0f)
            {
                transform.localPosition = _referencePosition;
                _teleportIn = false;
                if (TeleportComplete != null) TeleportComplete(this);

            }
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _referencePosition, Speed * Time.deltaTime);

                //var currentAngle = transform.localRotation;
                //transform.localRotation = new Quaternion();
                //Mathf.LerpAngle(currentAngle.y, currentAngle.y + 360f, 
                //var time = (1f / HideHeight) * transform.localPosition.y;
                //transform.Rotate(new Vector3(0, 1, 0), 2, Space.World);
                //transform.rotation = Quaternion.LerpUnclamped(currentAngle, Quaternion.AngleAxis(360f, Vector3.up), time);
                    //currentAngle.x, 360f, currentAngle.z), time);
            }
        }
        #region 

        else if (_teleportOut)
        {
            var vector = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, HideHeight, transform.position.z), Speed * Time.deltaTime);
            if (vector.y >= 3f)
            {
                transform.position = BuildingModelScript.ZeroPos(transform.position);
                _teleportIn = false;
            }
            transform.position = vector;
        }

	    #endregion
	}

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            Vector3.Lerp(transform.rotation.eulerAngles, byAngles, t);
                //Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    public void TeleportIn()
    {
        if (_teleportIn) return;
        _teleportIn = true;
        //StartCoroutine(RotateMe(new Vector3(0,720,0), 5));
    }

    public void TeleportOut()
    {
        if (_teleportOut) return;
        _teleportOut = true;
    }
}

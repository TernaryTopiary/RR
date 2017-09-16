using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class TeleportFireEffectScript : MonoBehaviour
{

    public List<Sprite> FireImages;
    private Image _image;
    private int _index;
    private float _timeLeft;
    public float FrameDuration = 0.025f;
    private bool _run;
    private bool _showFlames, _hideFlames;
    private Vector3 _referencePosition;
    public Action<TeleportFireEffectScript> FlameShowComplete, FlameHideComplete;
    private float _speed = _speedReferenceValue;
    private static readonly float _speedReferenceValue = 1f;

    // Use this for initialization
	void Start ()
	{
	    _timeLeft = FrameDuration;
	    _referencePosition = transform.localPosition;
	    transform.localPosition -= new Vector3(0,-1,0);
	    _image = GetComponent<Image>();
	    FireImages = FireImages.OrderBy(sprite => sprite.name).ToList();
	    FlameHideComplete += e => { _run = false; };
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
	    if (_run)
	    {
	        _timeLeft -= Time.deltaTime;
	        if (_timeLeft < 0)
	        {
	            if (_index == FireImages.Count) _index = 0;

	            _image.sprite = FireImages[_index];
	            _index++;
	            _timeLeft = FrameDuration;
	        }
	    }

        if (_hideFlames)
	    {
	        if (transform.localPosition.y <= -1f)
	        {
                _hideFlames = false;
                if (FlameHideComplete != null) FlameHideComplete(this);
	        }
	        else
	        {
	            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(_referencePosition.x, _referencePosition.y-1, _referencePosition.z), 
	                _speed*Time.deltaTime);
	        }
	    }
	    else if (_showFlames)
	    {
	        if (transform.localPosition.y >= _referencePosition.y)
	        {
	            transform.localPosition = _referencePosition;
	            _showFlames = false;
	            if (FlameShowComplete != null) FlameShowComplete(this);
	        }
	        else
	        {
	            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _referencePosition, _speed*Time.deltaTime);
	        }
	    }
	}

    public void ShowFlames()
    {
        _run = true;
        _hideFlames = false;
        _showFlames = true;
    }

    public void HideFlames()
    {
        _run = true;
        _hideFlames = true;
        _showFlames = false;
    }
}

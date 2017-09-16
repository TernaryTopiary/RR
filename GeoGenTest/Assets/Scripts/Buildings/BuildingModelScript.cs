using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEditor;

public class BuildingModelScript : MonoBehaviour 
{
    private bool _teleportChildrenIn;
    public float speed = 1f;
    private Action startAction = delegate { };
    public Action TeleportInComplete = delegate {  };

    public IEnumerable<GameObject> Children
    {
        get
        {
            var childObjects = GetComponentsInChildren<MeshFilter>().ToList();
            childObjects = childObjects.Where(item => item.gameObject.GetInstanceID() != GetInstanceID()).ToList();
            return childObjects.OrderBy(o => GetLowestYValue(o.mesh.vertices)).Select(item => item.gameObject);
        }
    }

	// Use this for initialization
	void Start ()
	{
	    _ready = true;
	    _lights = GetComponentsInChildren<Light>();
        foreach (var light1 in _lights)
	    {
	        light1.enabled = false;
	    }
	    _particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (var system in _particleSystems)
	    {
	        system.Pause();
	    }

	    if (startAction != null) startAction();
	}

    private float GetLowestYValue(Vector3[] verticies)
    {
        if (verticies.Count() >= 1) return verticies.Select(vert => vert.y).Min();
        else return 0;
    }

    private int index;
    private float _timeLeft = _maxTimeLeft;
    private static float _maxTimeLeft = 0.2f;
    private IEnumerator<GameObject> _enumerator;
    private bool _ready;
    private Light[] _lights;
    private ParticleSystem[] _particleSystems;
    private GameObject sparkEffectGameObject;
    private LightFlicker _lightFlicker;

    // Update is called once per frame
	void FixedUpdate () 
    {
        if (_teleportChildrenIn && _enumerator != null)
	    {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft < 0)
            {
                var childTf = _enumerator.Current;
                var tpScript = childTf.gameObject.GetComponent<BuildingPartTeleportScript>();
                tpScript.TeleportIn();
                _timeLeft = _maxTimeLeft;

                if (!_enumerator.MoveNext())
                {
                    _teleportChildrenIn = false;
                    //_enumerator.Reset();
                }
            }
	    }
	}

    public static Vector3 ZeroPos(Vector3 location)
    {
        return new Vector3(location.x, 0f, location.z);
    }

    public void TeleportIn()
    {
        if (!_ready) startAction = TeleportIn;

        index = 0;

        if (sparkEffectGameObject == null)
        {
            var sparkeffect = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Effects/TeleportBuildSparkEffect.prefab",
                typeof (GameObject));
            sparkEffectGameObject = Instantiate(sparkeffect, transform.position, new Quaternion()) as GameObject;
            sparkEffectGameObject.transform.localPosition += new Vector3(0, 5, 0);
        }

        if (!Children.Any())
        {
            TeleportInComplete();
            return;
        }
        foreach (var child in Children)
        {
            if (child.tag != "GroundEffect")
            {
                var teleScript = child.gameObject.AddComponent<BuildingPartTeleportScript>();
                child.transform.position = new Vector3(child.transform.position.x, BuildingPartTeleportScript.HideHeight, child.transform.position.z);
                _teleportChildrenIn = true;
            }
        }
        Children.First().GetComponent<BuildingPartTeleportScript>().TeleportComplete += TriggerAudio;
        Children.Last().GetComponent<BuildingPartTeleportScript>().TeleportComplete += EnableModelEffects;
        Children.Last().GetComponent<BuildingPartTeleportScript>().TeleportComplete += (e) =>
        {
            TeleportInComplete();
        };
    }

    private void TriggerAudio(BuildingPartTeleportScript teleScript)
    {
        var size = teleScript.gameObject.GetComponent<Renderer>().bounds.size.magnitude;
        var aS = GetComponent<AudioSource>();
        //aS.Play();
        if(aS != null) AudioSource.PlayClipAtPoint(aS.clip, teleScript.gameObject.transform.position, .00125f * size);
    }

    private void EnableModelEffects(BuildingPartTeleportScript buildingPartTeleportScript)
    {
        foreach (var light1 in _lights)
        {
            light1.enabled = true;
        }
        foreach (var system in _particleSystems)
        {
            system.Play();
        }
        if(_lightFlicker == null ) _lightFlicker = gameObject.AddComponent<LightFlicker>();
        _lightFlicker.FlickerLightsForDuration(.5f);

        sparkEffectGameObject.GetComponent<ParticleSystem>().Stop();
        //sparkEffectGameObject.SetActive(false);
    }
}

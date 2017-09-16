using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LightFlicker : MonoBehaviour
{
    private bool _flickerForDuration;
    private float _durationRemaining;
    private IEnumerable<Tuple<Light, float>> _lights;

    public bool FlickerLights { get; set; }

    void Start()
    {
        _lights = GetComponentsInChildren<Light>().Select(item => new Tuple<Light, float>(item, item.intensity));
    }

    void Update()
    {
        if (FlickerLights || _flickerForDuration && _durationRemaining > 0f)
        {
            if (_flickerForDuration) _durationRemaining -= Time.deltaTime;

            foreach (var tuple in _lights)
            {
                tuple.Item1.intensity = tuple.Item2 + (Random.value - 0.5f) * 2f;
            }
        }
        if (_flickerForDuration && _durationRemaining <= 0f)
        {
            _flickerForDuration = false;
            foreach (var tuple in _lights)
            {
                tuple.Item1.intensity = tuple.Item2;
            }
        }
    }

    public void FlickerLightsForDuration(float duration)
    {
        _flickerForDuration = true;
        _durationRemaining = duration;
    }
}

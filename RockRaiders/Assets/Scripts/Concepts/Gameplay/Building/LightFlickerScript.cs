using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public class LightFlickerScript : MonoBehaviour
    {
        private bool _flickerForDuration;
        private float _flickerFrequency = 0.05f;
        private float _elapsedTime;
        private float _durationRemaining;
        public IEnumerable<Tuple<Light, float>> Lights { get; set; }

        public bool FlickerLights { get; set; }

        void Update()
        {
            if (FlickerLights || _flickerForDuration && _durationRemaining > 0f)
            {
                _elapsedTime += Time.deltaTime;
                if (_flickerForDuration) _durationRemaining -= Time.deltaTime;
                if (_flickerFrequency <= _elapsedTime)
                {
                    _elapsedTime = 0f;
                    foreach (var tuple in Lights)
                    {
                        tuple.Item1.intensity = tuple.Item2 * UnityEngine.Random.Range(0.3f, 1);
                    }
                }
            }
            if (_flickerForDuration && _durationRemaining <= 0f)
            {
                _flickerForDuration = false;
                _elapsedTime = 0f;
                foreach (var tuple in Lights)
                {
                    tuple.Item1.intensity = tuple.Item2;
                }
            }
        }

        public void FlickerLightsForDuration(float duration)
        {
            _flickerForDuration = true;
            _elapsedTime = 0f;
            _durationRemaining = duration;
        }
    }
}
using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public class FadeScript : MonoBehaviour
    {
        public float ElapsedTime { get; set; }
        public Material Material { get; set; }
        public Color NormalColor { get; set; }
        public float FadeInTimeSeconds { get; set; } = 1f;
        public float FadeOutTimeSeconds { get; set; } = 2f;

        public bool IsStarted { get; set; } = false;
        public bool IsFaded { get; set; }
        private bool _isFadingIn, _isFadingOut;
        public Action Started, FadeComplete, UnfadeComplete;

        private void Start()
        {
            var renderer = gameObject.GetComponent<SkinnedMeshRenderer>();
            Material = renderer?.material;
            NormalColor = Material.color;

            IsStarted = true;
            Started?.Invoke();
        }

        private void Update()
        {
            if (_isFadingIn)
            {
                if (ElapsedTime < FadeInTimeSeconds) ElapsedTime += Time.deltaTime;
                if (ElapsedTime > FadeInTimeSeconds)
                {
                    ElapsedTime = FadeInTimeSeconds;
                    _isFadingIn = IsFaded = false;
                    UnfadeComplete?.Invoke();
                }
                var fractionSpawnComplete = (ElapsedTime / FadeInTimeSeconds);
                Material.color = new Color(Material.color.r, Material.color.g, Material.color.b, NormalColor.a * fractionSpawnComplete);
            }
            if (_isFadingOut)
            {
                if (ElapsedTime < FadeOutTimeSeconds) ElapsedTime += Time.deltaTime;
                if (ElapsedTime > FadeOutTimeSeconds)
                {
                    ElapsedTime = FadeOutTimeSeconds;
                    _isFadingOut = false;
                    IsFaded = true;
                    FadeComplete?.Invoke();
                }
                var fractionSpawnComplete = (ElapsedTime / FadeOutTimeSeconds);
                Material.color = new Color(Material.color.r, Material.color.g, Material.color.b, NormalColor.a - (NormalColor.a * fractionSpawnComplete));
            }
        }

        public void SetFadeStatus(bool isFaded)
        {
            Material.color = new Color(Material.color.r, Material.color.g, Material.color.b, isFaded ? 0 : NormalColor.a);
        }

        public void FadeIn()
        {
            SetFadeStatus(true);
            ElapsedTime = 0f;
            _isFadingIn = true;
            _isFadingOut = false;
        }

        public void FadeOut()
        {
            SetFadeStatus(false);
            ElapsedTime = 0f;
            _isFadingIn = false;
            _isFadingOut = true;
        }
    }
}
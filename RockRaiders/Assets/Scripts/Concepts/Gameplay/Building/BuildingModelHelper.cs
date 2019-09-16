using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public class BuildingModelHelper : MonoBehaviour
    {
        public Vector3 SpawnOffset = new Vector3(0, 5, 0);
        public int SpawnSpinDegrees = 360;
        public float SpawnDurationSeconds = 5;
        private float TotalSpawnDurationSeconds => SpawnDurationSeconds + SpawnStartDelaySeconds;
        public float SpawnStartDelaySeconds = 0;
        public float ElapsedTime { get; set; }
        public bool IsSpawned = false;
        public bool IsSpawning;
        public bool IsUnspawning;
        public bool AudioOnLand = false;

        public Action SpawnComplete;
        public Action UnspawnComplete;
        private Quaternion _originalRotation;

        private float _audioAdjust = 0.125f;
        private float _audioVolumeRatio;

        void Start()
        {
            LocalAudioSource = gameObject.AddComponent<AudioSource>();
            FinalPosition = gameObject.transform.position;
            if (!IsSpawned) gameObject.transform.position = gameObject.transform.position + SpawnOffset;
            _originalRotation = gameObject.transform.rotation;

            var renderer = gameObject.GetComponent<Renderer>();
            _audioVolumeRatio = (renderer.bounds.size.magnitude / (2 * Constants.Constants.TileScale)) * _audioAdjust;
            if (_audioVolumeRatio > 1) _audioVolumeRatio = 1;
        }

        public AudioSource LocalAudioSource { get; set; }

        public Vector3 FinalPosition { get; set; }

        void Update()
        {
            var name = gameObject.name;
            if (IsSpawning)
            {
                if (ElapsedTime < TotalSpawnDurationSeconds) ElapsedTime += Time.deltaTime;
                if (ElapsedTime > TotalSpawnDurationSeconds)
                {
                    ElapsedTime = TotalSpawnDurationSeconds;
                    IsSpawning = false;
                    IsSpawned = true;
                    SpawnComplete?.Invoke();
                    if(AudioOnLand) LocalAudioSource.PlayOneShot(AudioManager.Constants.Audio.Buildings.Thud, _audioVolumeRatio);
                }
                var fractionSpawnComplete = (ElapsedTime / TotalSpawnDurationSeconds);
                gameObject.transform.position = new Vector3(
                    FinalPosition.x,
                    FinalPosition.y + (SpawnOffset.y - (SpawnOffset.y * fractionSpawnComplete)),
                    FinalPosition.z);
                gameObject.transform.rotation = _originalRotation;
                gameObject.transform.Rotate(0, 0, SpawnSpinDegrees - (SpawnSpinDegrees * fractionSpawnComplete));
            }
            else if (IsUnspawning)
            {
                if (ElapsedTime < SpawnDurationSeconds) ElapsedTime += Time.deltaTime;
                if (ElapsedTime > SpawnDurationSeconds)
                {
                    ElapsedTime = SpawnDurationSeconds;
                    IsUnspawning = IsSpawned = false;
                    UnspawnComplete?.Invoke();
                }
                var fractionSpawnComplete = (ElapsedTime / SpawnDurationSeconds);
                // No spin on unspawn.
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, SpawnOffset, fractionSpawnComplete);
            }
        }

        public void Spawn()
        {
            if (IsSpawning) return;
            ElapsedTime = 0f;
            IsSpawning = true;
            IsUnspawning = false;
        }

        public void Unspawn()
        {
            if (IsUnspawning) return;
            ElapsedTime = 0f;
            IsSpawning = false;
            IsUnspawning = true;
        }
    }
}
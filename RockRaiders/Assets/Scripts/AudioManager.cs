using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class AudioManager
    {
        public static class Constants
        {
            public static class Audio
            {
                public static class Buildings
                {
                    public static AudioClip Thud;
                }
            }
        }

        static AudioManager()
        {
            LoadData();
        }

        public static void LoadData()
        {
            if (IsLoaded) return;
            IsLoaded = true;
            LoadBuildingAudio();
        }

        public static bool IsLoaded { get; set; }

        private static void LoadBuildingAudio()
        {
            Constants.Audio.Buildings.Thud = Resources.Load<AudioClip>("Sounds/Buildings/BTHUD");
        }
    }
}

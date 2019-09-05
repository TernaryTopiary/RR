using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public class TileScriptManager : MonoBehaviour
    {
        public List<MonoBehaviour> Behaviours { get; private set; } = new List<MonoBehaviour>();
        public GameObject Physicality { get; set; }

        public TileScriptManager(List<MonoBehaviour> behaviours)
        {
            Behaviours = behaviours;
        }

        public TileScriptManager()
        {
        }

        public void RegisterBehavior(MonoBehaviour behaviour)
        {
            Behaviours.Add(behaviour);
        }

        public void UnregisterBehavior(MonoBehaviour behaviour)
        {
            Behaviours.Remove(behaviour);
        }

        public void UnregisterBehaviors<TBehaviour>()
        {
            Behaviours.Where(b => b is TBehaviour).ForEach(b => Behaviours.Remove(b));
        }
    }
}
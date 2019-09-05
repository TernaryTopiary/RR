using UnityEngine;

namespace Assets.Scripts
{
    public class MapConstants : MonoBehaviour
    {

        // Use this for initialization
        private void Start()
        {
            TintSelected = Resources.Load("Materials/TintColorSelected") as Material;
            TintSelected.name = "tintSelected";
            TintMine = Resources.Load("Materials/TintColorMineQueue") as Material;
            TintMine.name = "tintMine";
            TintReinforce = Resources.Load("Materials/TintColorReinforce") as Material;
            TintReinforce.name = "tintReinforced";
            TintDynamite = Resources.Load("Materials/TintColorDynamite") as Material;
            TintDynamite.name = "tintDynamite";
        }

        public static Material TintSelected;
        public static Material TintMine;
        public static Material TintReinforce;
        public static Material TintDynamite;
    }
}
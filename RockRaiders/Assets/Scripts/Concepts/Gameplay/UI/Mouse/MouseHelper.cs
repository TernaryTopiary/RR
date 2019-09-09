using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.UI.Mouse
{
    public class MouseHelper : MonoBehaviour
    {
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;

        void Start()
        {
            Cursor.SetCursor(MaterialManager.Constants.Gameplay.UI.CursorDefault, hotSpot, cursorMode);
        }
    }
}

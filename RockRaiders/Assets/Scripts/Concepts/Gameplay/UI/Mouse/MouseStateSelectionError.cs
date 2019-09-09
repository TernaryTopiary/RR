using Assets.Scripts.Miscellaneous;

namespace Assets.Scripts.Concepts.Gameplay.UI.Mouse
{
    public class MouseStateSelectionError : Singleton<MouseStateSelectionError>
    {
        public static object Duration { get; internal set; }
        public static object Audio { get; internal set; }
    }
}

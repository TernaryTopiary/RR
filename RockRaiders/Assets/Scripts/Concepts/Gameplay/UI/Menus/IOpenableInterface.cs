namespace Assets.Scripts.Concepts.Gameplay.UI.Menus
{
    internal interface IOpenableInterface
    {
        bool IsOpen { get; set; }

        void Show();

        void Hide();

    }
}
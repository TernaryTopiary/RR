using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Gameplay.Shared;
using Assets.Scripts.Concepts.Gameplay.UI.Mouse;
using Assets.Scripts.Concepts.Gameplay.Audio;
using Assets.Scripts.Miscellaneous;

namespace Assets.Scripts
{
    public class MapInteractor : Singleton<MapInteractor>
    {
        public MouseManager MouseManager = MouseManager.GetInstance();
        public AudioManager AudioManager = AudioManager.GetInstance();
        public TileInteractor SelectedMapTileInteractor { get; internal set; }

        public void UnselectTile()
        {
            if (SelectedMapTileInteractor == null) return;
        }

        public void SelectTile(TileInteractor tile)
        {
            if (!(tile.TileDefinition.TileType is ISelectable))
            {
                MouseManager.ShowMouseState<MouseStateSelectionError>(MouseStateSelectionError.Duration);
                AudioManager.PlaySound(MouseStateSelectionError.Audio);
            }
            else
            {
                UnselectTile();
                SelectedMapTileInteractor?.Unselect();
                SelectedMapTileInteractor = tile;
                tile.Select();
                //AudioManager.PlaySound(MouseClick);
            }
        }
    }
}
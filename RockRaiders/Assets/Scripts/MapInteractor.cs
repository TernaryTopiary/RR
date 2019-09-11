using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Gameplay.Shared;
using Assets.Scripts.Concepts.Gameplay.UI.Mouse;
using Assets.Scripts.Concepts.Gameplay.Audio;
using Assets.Scripts.Concepts.Gameplay.Building.BuildingType;
using Assets.Scripts.Miscellaneous;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Concepts.Cosmic.Space;

namespace Assets.Scripts
{
    public class MapInteractor : MonoBehaviour
    {
        public event Action<RaycastHit> OnObjectClicked;
        public event Action<RaycastHit> OnMouseMove;

        public bool IsInBuildingPlacementMode { get; set; } = false;
        public BuildingType BuildingToBuild { get; set; }

        public Map Map = Map.GetInstance();
        public MouseManager MouseManager = MouseManager.GetInstance();
        public AudioManager AudioManager = AudioManager.GetInstance();
        private Vector3 _lastMousePosition;
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

        private bool _isHighlightingTiles = false;
        private Vector3 _lastMouseClickPosition;

        void Start()
        {
            OnObjectClicked += ObjectClickedOnMap;
            OnMouseMove += MouseMove;
        }

        public void MouseUp()
        {
            _lastMouseClickPosition = Input.mousePosition;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(_lastMousePosition), out var hitInfo))
            {
                LastClickedObject = hitInfo.transform.gameObject;
                OnObjectClicked?.Invoke(hitInfo);
            }

        }

        public void MouseMove(RaycastHit hitInfo)
        {
            if (IsInBuildingPlacementMode && !_isHighlightingTiles)
            {
                if (hitInfo.transform.gameObject.name.StartsWith(Tile.TileGameObjectNamePrefix))
                {
                    _isHighlightingTiles = true;
                    HighlightTiles(Map.GetPosition(LastHoveredObject), BuildingToBuild);
                    _isHighlightingTiles = false;
                }
            }
        }

        private void HighlightTiles(Vector2 location, BuildingType buildingToBuild)
        {
            var buildingTypeDefinition = BuildingTypeHelper.BuildingTypeLookup[BuildingToBuild];
            var plan = buildingTypeDefinition.DefaultTileLayout;
            foreach (var kv in plan)
            {
                var targetLocation = location + kv.Key.ToOffsetVector2();
                if (!Map.IsValidPosition(targetLocation)) continue;

                // If there is a building, the drawn tile is green. If there isn't a building, it's yellow (just foundation).
                DrawHighlight(targetLocation, kv.Value.Nodes?.Any() == true ? TileOverlayType.PlaceBuilding : TileOverlayType.PlaceBuildingFoundation);
            }
        }

        private GameObject DrawHighlight(Vector2 targetLocation, TileOverlayType type)
        {
            if(Map.TileOverlays[(int)targetLocation.x, (int)targetLocation.y] == null) Map.TileOverlays[(int)targetLocation.x, (int)targetLocation.y] = new Dictionary<TileOverlayType,GameObject>();
            var dict = Map.TileOverlays[(int)targetLocation.x, (int)targetLocation.y];
            if (!dict.ContainsKey(type))
            {
                var tileGameObject = Map.Tiles2D[(int) targetLocation.x, (int) targetLocation.y];
                var verts = tileGameObject.Verticies.ToArray();

                var gameObject = new GameObject {name = "mapTile"};
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, .1f,
                    gameObject.transform.localPosition.y);
                var meshRenderer = gameObject.AddComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
                meshRenderer.updateWhenOffscreen = true;
                Material material = null;
                switch (type)
                {
                    case TileOverlayType.PlaceBuilding:
                        material = MaterialManager.Constants.Gameplay.Map.TintBuildingPlacementMaterial;
                        break;
                    case TileOverlayType.PlaceBuildingFoundation:
                        material = MaterialManager.Constants.Gameplay.Map.TintBuildingFoundationPlacementMaterial;
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
                meshRenderer.material = material;

                var uvs = verts.Select(vert => new Vector2(vert.x, vert.z)).ToArray();

                // Create the 3D mesh.
                var mesh = new Mesh {vertices = verts, triangles = tileGameObject.Indicies, uv = uvs.ToArray()};
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();

                // Set up game object with mesh;
                meshRenderer.sharedMesh = mesh;

                return gameObject;
            }
            else return dict[type];
        }

        public void ObjectClickedOnMap(RaycastHit hitInfo)
        {
            if (hitInfo.transform.gameObject.name.StartsWith(Tile.TileGameObjectNamePrefix))
            {
                var tileInteractor = hitInfo.transform.gameObject.GetComponent<TileInteractor>();
                SelectTile(tileInteractor);
            }
        }

        public GameObject LastHoveredObject { get; private set; }
        public GameObject LastClickedObject { get; private set; }

        // Update is called once per frame
        void Update()
        {
            // Only raycast on mouse move.
            if (_lastMousePosition != Input.mousePosition && Physics.Raycast(Camera.main.ScreenPointToRay(_lastMousePosition), out var hitInfo))
            {
                _lastMousePosition = Input.mousePosition;
                LastHoveredObject = hitInfo.transform.gameObject;
                OnMouseMove?.Invoke(hitInfo);
            }
        }

        public void StartBuildingPlacementMode(BuildingType buildingType)
        {
            IsInBuildingPlacementMode = true;
            BuildingToBuild = buildingType;
        }

        public void StopBuildingPlacementMode()
        {
            IsInBuildingPlacementMode = false;
            ClearBuildingHighlights();
        }

        private void ClearBuildingHighlights()
        {
            throw new NotImplementedException();
        }
    }
}
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
using Assets.Scripts.Extensions;

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
        private GameObject _tileMouseIsOver;

        private class HighlightInfo
        {
            public Vector2 Location { get; set; }
            public TileOverlayType Type { get; set; }
            public GameObject Physicality { get; set; }
        }

        private List<HighlightInfo> HighlightInformation = new List<HighlightInfo>();

        void Start()
        {
            OnObjectClicked += ObjectClickedOnMap;
            OnMouseMove += MouseMove;
            MapOverlayRootObject = GameObject.Find("Overlays");
        }

        public GameObject MapOverlayRootObject { get; set; }

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
                    _tileMouseIsOver = hitInfo.transform.gameObject;
                    _isHighlightingTiles = true;
                    HighlightTiles(Map.GetPosition(LastHoveredObject), hitInfo, BuildingToBuild);
                    _isHighlightingTiles = false;
                }
            }
            else _tileMouseIsOver = null;
        }

        private void HighlightTiles(Vector2 location, RaycastHit hitInfo, BuildingType buildingToBuild)
        {
            var buildingTypeDefinition = BuildingTypeHelper.BuildingTypeLookup[BuildingToBuild];
            var plan = buildingTypeDefinition.DefaultTileLayout.Clone();

            var mouseLoc = hitInfo.point;
            var currentTile = Map.Tiles2D[(int)location.x, (int)location.y];

            // Determine building orientation.
            var v1 = currentTile.GetVertexAt(CornerOrientation.NorthWest);
            var v3 = currentTile.GetVertexAt(CornerOrientation.SouthEast);
            var currentBuildingOrientation = CompassAxisOrientation.South;
            var distances = new Dictionary<CompassAxisOrientation, float>
            {
                {
                    CompassAxisOrientation.North, Vector3.Distance(mouseLoc,
                        new Vector3(mouseLoc.x, mouseLoc.y, new float[] {v1.z, v3.z}.Max()))
                },
                {
                    CompassAxisOrientation.East, Vector3.Distance(mouseLoc,
                        new Vector3(new float[] {v1.x, v3.x}.Max(), mouseLoc.y, mouseLoc.z))
                },
                {
                    CompassAxisOrientation.South, Vector3.Distance(mouseLoc,
                        new Vector3(mouseLoc.x, mouseLoc.y, new float[] {v1.z, v3.z}.Min()))
                },
                {
                    CompassAxisOrientation.West, Vector3.Distance(mouseLoc,
                        new Vector3(new float[] {v1.x, v3.x}.Min(), mouseLoc.y, mouseLoc.z))
                }
            };
            var minDistance = distances.OrderBy(kv => kv.Value).First();

            while (minDistance.Key != currentBuildingOrientation)
            {
                plan = plan.Rotate(RotationalOrientation.Clockwise);
                currentBuildingOrientation = currentBuildingOrientation.Rotate(RotationalOrientation.Clockwise);
            }

            // Draw
            var currentHighlights = new List<HighlightInfo>();
            foreach (var kv in plan)
            {
                var targetLocation = location + kv.Key.ToOffsetVector2();
                if (!Map.IsValidPosition(targetLocation)) continue;

                var isValidPlacementLocation = kv.Value.ValidTargetTileTypes.Contains(Map.Tiles2D[(int) targetLocation.x, (int) targetLocation.y].TileType);
                var tileHighlightType = isValidPlacementLocation
                    ? (kv.Value.Nodes?.Any() == true
                        ? TileOverlayType.PlaceBuilding
                        : TileOverlayType.PlaceBuildingFoundation)
                    : TileOverlayType.InvalidBuildingPlacement;

                currentHighlights.Add(DrawHighlight(targetLocation, tileHighlightType));
            }

            foreach (var highlight in HighlightInformation.Where(hi => !currentHighlights.Contains(hi)).ToArray())
            {
                ClearHighlight(highlight);
            }
        }

        private void ClearHighlight(Vector2? targetLocation = null)
        {
            if (targetLocation == null)
            {
                foreach (var highlight in HighlightInformation) GameObject.Destroy(highlight.Physicality);
                HighlightInformation.Clear();
            }
            else
            {
                var highlight = HighlightInformation.FirstOrDefault(item => item.Location == targetLocation);
                if (highlight != null) ClearHighlight(highlight);
            }
        }

        private void ClearHighlight(HighlightInfo highlightInfo)
        {
            GameObject.Destroy(highlightInfo.Physicality);
            HighlightInformation.Remove(highlightInfo);
        }

        private HighlightInfo DrawHighlight(Vector2 targetLocation, TileOverlayType type)
        {
            var existingHighlight = HighlightInformation.FirstOrDefault(item => item.Location == targetLocation);
            if (existingHighlight != null)
            {
                if (existingHighlight.Type != type)
                {
                    GameObject.Destroy(existingHighlight.Physicality);
                    HighlightInformation.Remove(existingHighlight);
                }
                // Highlight already present, don't draw again.
                else return existingHighlight;
            }

            var tileGameObject = Map.Tiles2D[(int) targetLocation.x, (int) targetLocation.y];

            // Don't draw highlights on ceilings. No point.
            if (tileGameObject.IsCeiling) return null;

            var verts = tileGameObject.Verticies.ToArray();
            var gameObject = new GameObject {name = "mapTile"};
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, .1f,gameObject.transform.localPosition.y);
            gameObject.transform.parent = MapOverlayRootObject.transform;
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
                case TileOverlayType.InvalidBuildingPlacement:
                    material = MaterialManager.Constants.Gameplay.Map.TintBuildingPlacementDeniedMaterial;
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

            var info = new HighlightInfo {Location = targetLocation, Physicality = gameObject, Type = type};
            HighlightInformation.Add(info);
            return info;
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
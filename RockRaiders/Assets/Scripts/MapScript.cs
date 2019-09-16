//using System.Linq;
//using Assets.Scripts.Concepts.Gameplay.Map.TileType;
//using UnityEngine;

//namespace Assets.Scripts
//{
//    public class MapScript : MonoBehaviour
//    {

//        private string selectionLock = string.Empty;

//        private GameObject selectedGameObject = null;
//        public static bool Working { get; set; }

//        // Use this for initialization
//        void Start () 
//        {
//            mousePos = Input.mousePosition;
//            _uiMgr = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
//        }

//        float offset = 0.0f;
//        float scrollSpeed = .5f;
//        public bool BuildingPlacementMode;
//        public IBuildingDefinition BuildingToBuild;
//        public GameObject BuildingCancelMenu;
//        private Color SelectedTintColor = new Color(81, 81, 81, 50);
//        private Color OriginalColor;

//        public void TileClickEvent(GameObject gameObject)
//        {
//            if (BuildingPlacementMode)
//            {
//                var target = Map.GetGameObjectCoords(gameObject);
//                var building = Map.CreateBuildingFoundation(target, BuildingToBuild);
//                if (building != null)
//                {
//                    Map.CreateBuilding(target, building);
//                    BuildingPlacementMode = false;
//                    Map.ClearBuildingHighlights();
//                    _uiMgr.ShowRootMenu();
//                }
//                return;
//            }
//            lock (selectionLock)
//            {
//                if (selectedGameObject != null) Unselect(selectedGameObject);
//                Select(gameObject);
//                //mr.material.color = SelectedTintColor;
//            }
//        }

//        private void Select(GameObject gameObject)
//        {
//            selectedGameObject = gameObject;
//            var mr = gameObject.GetComponent<SkinnedMeshRenderer>();
//            var mats = mr.materials.ToList();
//            if (!mats.Contains(MapConstants.TintSelected)) mats.Add(MapConstants.TintSelected);
//            mr.materials = mats.ToArray();
//            var aS = Camera.main.GetComponent<AudioSource>();
//            LocalAudioSource.PlayClipAtPoint(aS.clip, Camera.main.transform.position, .0125f);

//            var location = Map.GetGameObjectCoords(gameObject);//gameObject.GetComponent<TileScript>();
//            var tile = Map.Tiles2D[(int)location.X, (int)location.Y];
//            //var uiMgr = GameObject.FindGameObjectWithTag("UIManager");

//            if (tile.IsLowTileClass())  // Ground tile
//            {
//                _uiMgr.ShowPathAndFenceMenu();
//            }
//            else                        // Wall tile
//            {
//                _uiMgr.ShowWallMenu();
//            }
//            //if(Tile)

//        }

//        public static void Unselect(GameObject gameObject)
//        {
//            if (gameObject == null) return;
//            var mr = gameObject.GetComponent<SkinnedMeshRenderer>();
//            var mats = mr.materials.ToList();
//            var nuMats = mats.Where(mat => mat.name.Contains(MapConstants.TintSelected.name));
//            mr.materials = mats.Except(nuMats).ToArray();
//        }

//        public void Unselect()
//        {
//            if (selectedGameObject != null)
//            {
//                Unselect(selectedGameObject);
//                _uiMgr.ShowRootMenu();
//            }
//        }

//        public void StartBuildingPlacementMode(string buildingName)
//        {
//            Unselect(selectedGameObject);
//            Map.BuildingPlacementMode = BuildingPlacementMode = true;
//            BuildingToBuild = BuildingTemplates.GetClassFromName(buildingName);
//        }

//        public void StopBuildingPlacementMode()
//        {
//            Map.BuildingPlacementMode = BuildingPlacementMode = false;
//            Map.ClearBuildingHighlights();
//        }

//        private Vector3 mousePos;
//        private UIManager _uiMgr;

//        // Update is called once per frame
//        void Update () {
//            // Scroll the lava and water textures.
//            offset += (Time.deltaTime * scrollSpeed) / 10.0f;
//            MaterialManager.GetWaterTile(TileBiome.Rock).SetTextureOffset("_MainTex", new Vector2(2 * offset, 0));
//            MaterialManager.GetLavaTile(TileBiome.Rock).SetTextureOffset("_MainTex", new Vector2(offset, 0));
//            MaterialManager.GetWaterTile(TileBiome.Ice).SetTextureOffset("_MainTex", new Vector2(2 * offset, 0));
//            MaterialManager.GetLavaTile(TileBiome.Ice).SetTextureOffset("_MainTex", new Vector2(offset, 0));
//            MaterialManager.GetWaterTile(TileBiome.Lava).SetTextureOffset("_MainTex", new Vector2(2 * offset, 0));
//            MaterialManager.GetLavaTile(TileBiome.Lava).SetTextureOffset("_MainTex", new Vector2(offset, 0));

//            if (BuildingPlacementMode && Input.mousePosition != mousePos)
//            {
//                mousePos = Input.mousePosition;
//                var hitInfo = new RaycastHit();
//                if (!Working && Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hitInfo))//new Ray(Camera.main.ScreenToWorldPoint(mousePos), new Vector3(mousePos.x, -10, mousePos.z)), out hitInfo))
//                {
//                    Working = true;
//                    HighlightTiles(hitInfo, BuildingToBuild);
//                    Working = false;
//                }
//            }
//            //if (Input.GetMouseButtonDown(0))
//            //{
//            //    var hitInfo = new RaycastHit();
//            //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
//            //    {
//            //        //Debug.Log("Hit " + hitInfo.transform.gameObject.name);
//            //        if (hitInfo.transform.gameObject.name == "selectableMapTile")
//            //        {
//            //            if (selectedGameObject != null) Unselect(selectedGameObject);
//            //            selectedGameObject = hitInfo.transform.gameObject;
//            //            TileClickEvent(selectedGameObject);
//            //        }
//            //        else
//            //        {
//            //        }
//            //    }
//            //    else
//            //    {
//            //    }
//            //} 
//        }

//        private void HighlightTiles(RaycastHit hitInfo, IBuildingDefinition buildingToBuild)
//        {
//            Map.ShowBuildingHighlight(hitInfo, buildingToBuild);
//        }

//        public void CreatePowerPath()
//        {
//            if (selectedGameObject != null) Map.CreatePowerPath(selectedGameObject);

//        }

//        public void CreateRaider()
//        {
        
//        }
//    }
//}




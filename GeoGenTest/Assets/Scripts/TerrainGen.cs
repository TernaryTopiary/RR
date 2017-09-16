using System;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

[InitializeOnLoad]
public class TerrainGen : MonoBehaviour 
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TerrainGen"/> class.
	/// </summary>
	TerrainGen()
	{

	}

	const int Tilesize = 1;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
        audioClips = new[]
        {
            Resources.Load<AudioClip>("Sounds/Voices/Surfaces/dirt"),
            Resources.Load<AudioClip>("Sounds/Voices/Surfaces/looserock"),
            Resources.Load<AudioClip>("Sounds/Voices/Surfaces/hardrock"),
            Resources.Load<AudioClip>("Sounds/Voices/Surfaces/solidrock"),
            Resources.Load<AudioClip>("Sounds/Voices/Surfaces/encryseam"),
            Resources.Load<AudioClip>("Sounds/Voices/Surfaces/oreseam"),
            Resources.Load<AudioClip>("Sounds/Voices/Surfaces/recharge"),
        };

        TestMapImport(@"Assets\Resources\test.mcm");
	}

    float rotate = 0.0f;
    float groundHeight = -1.5f;
    private float _defaultCameraHeight = 2.5f;

    private AudioClip[] audioClips;

	// Update is called once per frame
	void Update () {

	}

    private Tuple<GameObject, int>[,] terrainObjects;
    //private Map _map;

    public Camera MainCamera;

    private void TestMapImport(string path)
	{
		var lines = File.ReadAllLines(path);

        MainCamera = Camera.main;

        foreach (var line in lines)
        {
            if (line.ToLower().Contains("camera"))
            {
                if (MainCamera != null)
                {
                    var camData = line.Split('|');
                    var xpos = Convert.ToInt32(camData[2])/32f;
                    var ypos = Convert.ToInt32(camData[3])/32f;
                    var angle = Convert.ToInt32(camData.Last());
                    var campos = new Vector3(xpos, _defaultCameraHeight, ypos);
                    MainCamera.transform.RotateAround(campos, new Vector3(0, 1, 0), angle + 90);
                    MainCamera.transform.localPosition = campos;
                }
            }
        }

	    //_map = new Map(lines);
        Map.GenerateMap(lines);

        var terrainMapGameObjects = Map.Tiles;

        for (var y = 0; y < Map.Dimensions.y; y++) 
		{
            for (var x = 0; x < Map.Dimensions.x; x++)
		    {
                var surroundingCellTiles = Map.GetSurroundingCells(x, y, true, true);
                var go = terrainMapGameObjects[x, y];
		        go.TileGameObject.name = "Tile: " + x + ", " + y;
                var meshRenderer = go.TileGameObject.GetComponent<SkinnedMeshRenderer>();
                var verts = GetHeightAdjustedVerts(surroundingCellTiles);
                var indicies = GetHeightAdjustedIndicies(verts);
                var matAndClass = GetHeightAdjustedMaterialAndClass(verts, surroundingCellTiles);
		        meshRenderer.material = matAndClass.Item1;
		        Map.Tiles[x,y].TileClass = matAndClass.Item2;
                if (Map.Tiles[x, y].IsSelectable() && !IsDiagonalLower(verts, surroundingCellTiles)) go.TileGameObject.name += " selectableMapTile";
                var skinnedMeshRenderer = go.TileGameObject.GetComponent<SkinnedMeshRenderer>();
		        skinnedMeshRenderer.sharedMesh.vertices = verts;
		        skinnedMeshRenderer.sharedMesh.triangles = indicies;
                skinnedMeshRenderer.sharedMesh.uv = GetHeightAdjustedUvs(verts).Select(vert => new Vector2(vert.x, vert.z)).ToArray();

                if (Map.Tiles[x, y].TileClass != Tile.RockClass.Soil && Map.Tiles[x, y].IsSelectable())
		        {
                    var audioSource = (AudioSource)go.TileGameObject.AddComponent(typeof(AudioSource));
                    audioSource.clip = GetClipForRockType(Map.Tiles[x, y].TileClass);

                    var trigger = go.TileGameObject.AddComponent<EventTrigger>();
                    var trig = new EventTrigger.TriggerEvent();
                    trig.AddListener(data => audioSource.Play());
                    trigger.triggers.Add(new EventTrigger.Entry { callback = trig, eventID = EventTriggerType.PointerEnter });
		        }
		    }
		}

        for (var y = 0; y < Map.Dimensions.y; y++)
        {
            for (var x = 0; x < Map.Dimensions.x; x++)
            {
                var go = terrainMapGameObjects[x, y];
                var skinnedMeshRenderer = go.TileGameObject.GetComponent<SkinnedMeshRenderer>();
                skinnedMeshRenderer.sharedMesh.vertices = GetFinalHeightOffsetVerticies(skinnedMeshRenderer.sharedMesh.vertices, Map.GetSurroundingCells(x, y, true, true));
                skinnedMeshRenderer.sharedMesh.RecalculateNormals();
                skinnedMeshRenderer.sharedMesh.RecalculateBounds();
                go.TileGameObject.AddComponent(typeof(MeshCollider));
                go.TileGameObject.GetComponent<MeshCollider>().sharedMesh = skinnedMeshRenderer.sharedMesh;
            }
        }

        //Map.GameMap = terrainMapGameObjects;
        Map.GameBuildings = new List<BuildingInstance>();
        Map.GameBuildingTileMap = new BuildingTileInstance[terrainMapGameObjects.GetLength(0), terrainMapGameObjects.GetLength(1)];
	}

    private AudioClip GetClipForRockType(Tile.RockClass tileClass)
    {
        switch (tileClass)
        {
            case Tile.RockClass.Dirt:
                return audioClips[0];
            case Tile.RockClass.Loose:
                return audioClips[1];
            case Tile.RockClass.Hard:
                return audioClips[2];
            case Tile.RockClass.Solid:
                return audioClips[3];
            case Tile.RockClass.OreSeam:
                return audioClips[0];
            case Tile.RockClass.EnergySeam:
                return audioClips[0];
            case Tile.RockClass.RegeneratorSeam:
                return audioClips[0];
            case Tile.RockClass.SlugHole:
                return audioClips[0];
            default:
                return audioClips[0];
        }
    }

    public Vector3[] GetFinalHeightOffsetVerticies(Vector3[] verts, Tile[,] surroundingCellTiles)
    {
        var newVerts = verts.Clone() as Vector3[];

        var nwTile = surroundingCellTiles[0, 0] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var nTile = surroundingCellTiles[1, 0] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var neTile = surroundingCellTiles[2, 0] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var wTile = surroundingCellTiles[0, 1] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var centreTile = surroundingCellTiles[1, 1];
        var eTile = surroundingCellTiles[2, 1] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var swTile = surroundingCellTiles[0, 2] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var sTile = surroundingCellTiles[1, 2] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var seTile = surroundingCellTiles[2, 2] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);

        newVerts[0].y += (nwTile.TileHeight + nTile.TileHeight + wTile.TileHeight + centreTile.TileHeight) / 4f;
        newVerts[1].y += (swTile.TileHeight + wTile.TileHeight + sTile.TileHeight + centreTile.TileHeight) / 4f;
        newVerts[2].y += (seTile.TileHeight + sTile.TileHeight + eTile.TileHeight + centreTile.TileHeight) / 4f;
        newVerts[3].y += (neTile.TileHeight + eTile.TileHeight + nTile.TileHeight + centreTile.TileHeight) / 4f;
        
        return newVerts;
    }

    private static bool IsDiagonalLower(Vector3[] verts, Tile[,] surroundingCellTiles)
    {
        if ((verts[3].y > verts[2].y && verts[1].y > verts[2].y && (surroundingCellTiles[2, 2].LowTileClass)) ||
            (verts[0].y > verts[1].y && verts[2].y > verts[1].y && (surroundingCellTiles[0, 2].LowTileClass)) ||
            (verts[3].y > verts[0].y && verts[1].y > verts[0].y && (surroundingCellTiles[0, 0].LowTileClass)) ||
            (verts[2].y > verts[3].y && verts[0].y > verts[3].y && (surroundingCellTiles[2, 0].LowTileClass))) 
            return true;
        else return false;
    }

    private static bool IsDiagonalUpper(Vector3[] verts, Tile[,] surroundingCellTiles)
    {
        if ((verts[0].y > verts[3].y && verts[0].y > verts[1].y && (surroundingCellTiles[2, 2]).LowTileClass) ||
            (verts[3].y > verts[2].y && verts[3].y > verts[0].y && (surroundingCellTiles[0, 2]).LowTileClass) ||
            (verts[2].y > verts[3].y && verts[2].y > verts[1].y && (surroundingCellTiles[0, 0]).LowTileClass) ||
            (verts[1].y > verts[2].y && verts[1].y > verts[0].y && (surroundingCellTiles[2, 0]).LowTileClass))
            return true;
        else return false;
    }

    private Tuple<Material, Tile.RockClass> GetHeightAdjustedMaterialAndClass(Vector3[] verts, Tile[,] surroundingTiles)
    {
        var surroundingDirt = 0;        
        //var mats = Map.TileMaterialLookup;
        var isValid = true;
        
        var nwTile = surroundingTiles[0, 0] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var nTile = surroundingTiles[1, 0] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var neTile = surroundingTiles[2, 0] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var wTile = surroundingTiles[0, 1] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var centreTile = surroundingTiles[1, 1];
        var eTile = surroundingTiles[2, 1] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var swTile = surroundingTiles[0, 2] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var sTile = surroundingTiles[1, 2] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var seTile = surroundingTiles[2, 2] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);

        foreach (var tile in surroundingTiles)
        {
            if (tile != null && (tile).LowTileClass) surroundingDirt++;
        }

        if (surroundingDirt == 0)
            return new Tuple<Material, Tile.RockClass>(MaterialManager.GetRoofTile(centreTile.TileBiome), centreTile.TileClass);
        

        // If there is a group of four tiles in the nine surrounding tiles, it's a valid tile. Otherwise not and should be dirt. 
        if (!(centreTile).LowTileClass)
        {
            if ((((nwTile).LowTileClass|| (nTile).LowTileClass || (wTile).LowTileClass) &&
                 ((nTile).LowTileClass || (neTile).LowTileClass || (eTile).LowTileClass) &&
                 ((seTile).LowTileClass || (sTile).LowTileClass || (eTile).LowTileClass) &&
                 ((swTile).LowTileClass || (wTile).LowTileClass || (sTile).LowTileClass)))
            {
                isValid = false;
            }
        }
        if (!isValid) return new Tuple<Material, Tile.RockClass>(MaterialManager.GetGroundTile(centreTile.TileBiome), Tile.RockClass.Soil);

        if (IsDiagonalUpper(verts, surroundingTiles))
        {
            return new Tuple<Material, Tile.RockClass>(Tile.GetExternalCornerMaterial(centreTile.TileClass, centreTile.TileBiome), centreTile.TileClass);
        }
        else if (IsDiagonalLower(verts, surroundingTiles))
        {
            return new Tuple<Material, Tile.RockClass>(Tile.GetInternalCornerMaterial(centreTile.TileClass, centreTile.TileBiome), centreTile.TileClass);
        }
        else
        {
            return new Tuple<Material, Tile.RockClass>(Tile.GetWallMaterial(centreTile.TileClass, centreTile.TileBiome), centreTile.TileClass);
        }
    }

    private static int[] GetHeightAdjustedIndicies(IList<Vector3> verts)
    {
        if ((verts[1].y == verts[2].y && verts[2].y == verts[3].y) || (verts[1].y == verts[0].y && verts[0].y == verts[3].y))
            return new[] { 0, 1, 2, 2, 3, 0 };
        return new[] { 3, 0, 1, 1, 2, 3 };
    }

    private static IEnumerable<Vector3> GetHeightAdjustedUvs(IList<Vector3> verts)
    {
        if (verts[0].y > verts[1].y && verts[3].y > verts[2].y)
        {
            return new[] { verts[2], verts[3], verts[0], verts[1] };
        }
        if ((verts[1].y > verts[2].y && verts[3].y > verts[2].y && verts[0].y > verts[2].y) ||
            (verts[0].y > verts[2].y && verts[0].y > verts[1].y && verts[0].y > verts[3].y))
        {
            return new[] { verts[1], verts[2], verts[3], verts[0] };        // fine
        }

        if (verts[3].y > verts[0].y && verts[2].y > verts[1].y)
        {
            return new[] { verts[3], verts[0], verts[1], verts[2] };
        }
        if ((verts[3].y > verts[1].y && verts[0].y > verts[1].y && verts[2].y > verts[1].y) ||
            (verts[3].y > verts[2].y && verts[3].y > verts[1].y && verts[3].y > verts[0].y))
        {
            return new[] { verts[2], verts[3], verts[0], verts[1] };
        }

        if (verts[1].y > verts[0].y && verts[2].y > verts[3].y)
        {
            return verts;
        }
        if ((verts[1].y > verts[0].y && verts[3].y > verts[0].y && verts[2].y > verts[0].y) ||
            (verts[2].y > verts[0].y && verts[2].y > verts[1].y && verts[2].y > verts[3].y))
        {
            return new[] { verts[3], verts[0], verts[1], verts[2] };        // fine
        }

        if (verts[1].y > verts[2].y && verts[0].y > verts[3].y)
        {
            return new[] { verts[1], verts[2], verts[3], verts[0] };
        }
        if ((verts[1].y > verts[3].y && verts[2].y > verts[3].y && verts[0].y > verts[3].y) ||
            (verts[1].y > verts[0].y && verts[1].y > verts[2].y && verts[1].y > verts[3].y))
        {
            return verts;                                                   // fine
        }
        return verts;
    }

    public Vector3[] GetHeightAdjustedVerts(Tile[,] surroundingTiles)
    {
        var newVerts = Map.DefaultVerts.Clone() as Vector3[];
        var tile = surroundingTiles[1, 1];

        if ((tile).LowTileClass)
        {
            return newVerts.Select(vert => new Vector3(vert.x, groundHeight, vert.z)).ToArray();
        }

        var nwTile = surroundingTiles[0, 0] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var nTile = surroundingTiles[1, 0] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var neTile = surroundingTiles[2, 0] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var wTile = surroundingTiles[0, 1] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var centreTile = surroundingTiles[1, 1];
        var eTile = surroundingTiles[2, 1] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var swTile = surroundingTiles[0, 2] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var sTile = surroundingTiles[1, 2] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);
        var seTile = surroundingTiles[2, 2] ?? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid);


        if ((nTile).LowTileClass)
        {
            newVerts[0].y = groundHeight;
            newVerts[3].y = groundHeight;
        }
        else
        {
            if ((nwTile).LowTileClass || (nTile).LowTileClass || (wTile).LowTileClass)
            {
                newVerts[0].y = groundHeight;
            }
            if ((neTile).LowTileClass || (nTile).LowTileClass || (eTile).LowTileClass)
            {
                newVerts[3].y = groundHeight;
            }
        }

        if ((wTile).LowTileClass)
        {
            newVerts[0].y = groundHeight;
            newVerts[1].y = groundHeight;
        }
        else
        {
            if ((nwTile).LowTileClass || (nTile).LowTileClass || (wTile).LowTileClass)
            {
                newVerts[0].y = groundHeight;
            }
            if ((swTile).LowTileClass || (sTile).LowTileClass || (wTile).LowTileClass)
            {
                newVerts[1].y = groundHeight;
            }
        }

        if ((sTile).LowTileClass)
        {
            newVerts[1].y = groundHeight;
            newVerts[2].y = groundHeight;
        }
        else
        {
            if ((swTile).LowTileClass || (sTile).LowTileClass || (wTile).LowTileClass)
            {
                newVerts[1].y = groundHeight;
            }
            if ((seTile).LowTileClass || (sTile).LowTileClass || (eTile).LowTileClass)
            {
                newVerts[2].y = groundHeight;
            }
        }

        if ((eTile).LowTileClass)
        {
            newVerts[2].y = groundHeight;
            newVerts[3].y = groundHeight;
        }
        else
        {
            if ((neTile).LowTileClass || (nTile).LowTileClass || (eTile).LowTileClass)
            {
                newVerts[3].y = groundHeight;
            }
            if ((seTile).LowTileClass || (sTile).LowTileClass || (eTile).LowTileClass)
            {
                newVerts[2].y = groundHeight;
            }
        }

        return newVerts;
    }

    public static int IntParseFast(char value)
    {
        var result = 0;
        result = 10 * result + (value - 48);
        return result;
    }
}

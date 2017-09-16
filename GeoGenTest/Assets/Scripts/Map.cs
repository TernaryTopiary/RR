using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Assets.Scripts;
using Object = UnityEngine.Object;
using Random = System.Random;

public static class Map
{
	public const int Tilesize = 1;

	private static GameObject MapRoot;

	#region Geometry Constants

	public static readonly Vector3[] DefaultVerts =
	{
		new Vector3(0, 0, 0),
		new Vector3(0, 0, Tilesize),
		new Vector3(Tilesize, 0, Tilesize),
		new Vector3(Tilesize, 0, 0)
	};

	private static readonly int[] _indices = {0, 1, 2, 2, 3, 0};

	#endregion

	#region Import

	public static int[] TileClassImportReference =
	{
		0, 2, 3, 4, 5, 26, 13, 41, 27, 28
	};

	public static Tile.RockClass GetClassFromImportReference(string s)
	{
		return (Tile.RockClass) TileClassImportReference[int.Parse(s)];
		//(Tile.RockClass)TileClassImportReference.First(item => item.Item1.Equals(s)).Item2;
	}

	#endregion

	public enum Orientation
	{
		North,
		East,
		South,
		West
	}

	public static Vector2 Dimensions
	{
		get
		{
			return new Vector2(Tiles.GetLength(0), Tiles.GetLength(1));
		}
		set
		{
			ResizeArray(Tiles, (int)value.x, (int)value.y);
		}
	}

	public static List<IDecoration>[] TileDecorations;

	//public static GameObject[,] GameMap;
	public static List<BuildingInstance> GameBuildings;
	public static BuildingTileInstance[,] GameBuildingTileMap;
	public static List<IResource> GameResourceMap;
	private static IBuildingDefinition BuildingToBuild;
	private static Orientation buildingToBuildOrientation;

	public static Tile[,] Tiles;
	public static int[,] HighlightBuildingTemplate;
	public static List<GameObject> Highlights;
	public static bool BuildingPlacementMode;

	private static GameObject GenerateMapTile(Tile tile, Vector3[] verts, int[] indicies, Material mat = null)
	{
		var gameObject = new GameObject {name = "mapTile"};
		var ts = gameObject.AddComponent<TileScript>();

		// Add reference for the map to the map script.
		ts.MapReference = MapRoot.GetComponent<MapScript>();

		var meshRenderer = gameObject.AddComponent(typeof (SkinnedMeshRenderer)) as SkinnedMeshRenderer;
		meshRenderer.updateWhenOffscreen = true;
		meshRenderer.material = mat ?? GetMaterial(tile);

		// The verticies of the mesh with no Y components. 
		var uvs = verts.Select(vert => new Vector2(vert.x, vert.z));

		// Create the 3D mesh.
		var mesh = new Mesh { vertices = verts, triangles = indicies, uv = uvs.ToArray() };
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		// Set up game object with mesh;
		meshRenderer.sharedMesh = mesh;
		return gameObject;
	}

	public static void GenerateMap(IList<string> lines)
	{
		MapRoot = GameObject.Find("GameMap");
		Highlights = new List<GameObject>();

		// TODO: Biome derived from map file.
		var biome = MaterialManager.Biome.ROCK;

		var dimensions = new Vector2(Convert.ToInt32(lines.First().Split('|')[2]), Convert.ToInt32(lines.First().Split('|')[3]));

		Tiles = new Tile[(int)dimensions.x, (int)dimensions.y];

		var minHeight = int.Parse(lines.Where(line => line.StartsWith("BLOCK")).Select(blockLine => blockLine.Split('|')[4]).Min());

		int x = 0, y = 0;
		foreach (var line in lines.Skip(2).Where(line => line.StartsWith("BLOCK")))
		{
			// Index of current tile.
			var currentTileLine = line;
			var lineData = currentTileLine.Split('|');
			var rockClass = GetClassFromImportReference(lineData[1]);
			var aug = GetTileAugmentation(lineData[1]);
			var height = Convert.ToInt32(lineData[4]);
			Tiles[((int)dimensions.x - 1) - x, y] = new Tile(biome, rockClass, aug, (height - minHeight) / 8f);

			x++;
			if (x == dimensions.x)
			{
				x = 0;
				y++;
			}
		}

		x = 0;
		y = 0;

		// Generate GameObjects in the tile list.
		for (y = 0; y < (int)Dimensions.y; y++)
		{
			for (x = 0; x < (int)Dimensions.x; x++)
			{
				var tile = Tiles[x, y];
				var tileGameObject = GenerateMapTile(tile, DefaultVerts, _indices);
				tile.TileGameObject = tileGameObject;
				tileGameObject.transform.localPosition = new Vector3(x, 0, y);
			}
		}
	}

	private static Tile.Augmentation GetTileAugmentation(string s)
	{
		return Tile.Augmentation.None;
	}

	public static String ConvertToString(Enum eff)
	{
		return Enum.GetName(eff.GetType(), eff);
	}

	private static Material GetMaterial(Tile tile)
	{
		if (tile == null) return null;
		//var biome = ConvertToString(tile.TileBiome).ToUpper();
		return Tile.GetWallMaterial(tile.TileClass, tile.TileBiome);
	}

	private static T[,] ResizeArray<T>(T[,] original, int rows, int cols)
	{
		var newArray = new T[rows, cols];
		var minRows = Math.Min(rows, original.GetLength(0));
		var minCols = Math.Min(cols, original.GetLength(1));
		for (var i = 0; i < minRows; i++)
			for (var j = 0; j < minCols; j++)
				newArray[i, j] = original[i, j];
		return newArray;
	}
	
	/// <summary>
	/// Get where a tile is in the map.
	/// </summary>
	/// <param name="tile"></param>
	/// <returns></returns>
	public static Vector2 GetTileCoords(Tile tile)
	{
		for (var i = 0; i < Dimensions.x; i++)
		{
			for (var j = 0; j < Dimensions.y; j++)
			{
				if(Tiles[i,j] == tile) return new Vector2(i, j);
			}
		}
		return new Vector2(-1, -1);
	}

	public static Tile GetNeighboringTile(Orientation direction, Vector2 location)
	{
		switch (direction)
		{
			case Orientation.North:
				return TileExists((int)location.x, (int)location.y-1) ? Tiles[(int)location.x, (int)location.y - 1] : null;
			case Orientation.East:
				return TileExists((int)location.x + 1, (int)location.y) ? Tiles[(int)location.x + 1, (int)location.y] : null;
			case Orientation.South:
				return TileExists((int)location.x, (int)location.y + 1) ? Tiles[(int)location.x, (int)location.y + 1] : null;
			case Orientation.West:
				return TileExists((int)location.x - 1, (int)location.y) ? Tiles[(int)location.x - 1, (int)location.y] : null;
			default:
				throw new ArgumentOutOfRangeException("direction");
		}
	}

	/// <summary>
	/// Get where a gameObject is in the map.
	/// </summary>
	/// <param name="gameObject"></param>
	/// <returns></returns>
	public static Vector2 GetGameObjectCoords(GameObject gameObject)
	{
		for (var i = 0; i < Dimensions.x; i++)
		{
			for (var j = 0; j < Dimensions.y; j++)
			{
				if (Tiles[i, j].TileGameObject == gameObject) return new Vector2(i, j);
			}
		}
		return new Vector2(-1, -1);
	}

	#region Building Placement Highlights
	public static void ShowBuildingHighlight(RaycastHit hitInfo, IBuildingDefinition buildingToBuild)
	{
		var targetTile = hitInfo.transform.gameObject;
		var location = GetGameObjectCoords(targetTile);
		var templateCopy = buildingToBuild.GetTemplate().Clone() as int[,];

		ClearBuildingHighlights();
		BuildingToBuild = buildingToBuild;

		#region Building Placement Template Rotation

		// Initial distance should be max, anything should be less.
		var distRight = float.MaxValue;
		var distLeft = float.MaxValue;
		var distTop = float.MaxValue;
		var distBottom = float.MaxValue;

		var mouseLoc = hitInfo.point;

		var tileRight = TileExists((int) location.x + 1, (int) location.y) ? Tiles[(int)location.x + 1, (int)location.y].TileGameObject : null;
		if (tileRight != null)
		{
			var locRight = tileRight.GetComponent<SkinnedMeshRenderer>().bounds.center;
			distRight = Vector3.Distance(locRight, mouseLoc);
		}
		var tileLeft = TileExists((int)location.x - 1, (int)location.y) ? Tiles[(int)location.x - 1, (int)location.y].TileGameObject : null;
		if (tileLeft != null)
		{
			var locLeft = tileLeft.GetComponent<SkinnedMeshRenderer>().bounds.center;
			distLeft = Vector3.Distance(locLeft, mouseLoc);
		}
		var tileTop = TileExists((int)location.x, (int)location.y + 1) ? Tiles[(int)location.x, (int)location.y + 1].TileGameObject : null;
		if (tileTop != null)
		{
			var locTop = tileTop.GetComponent<SkinnedMeshRenderer>().bounds.center;
			distTop = Vector3.Distance(locTop, mouseLoc);
		}
		var tileBottom = TileExists((int)location.x, (int)location.y - 1) ? Tiles[(int)location.x, (int)location.y - 1].TileGameObject : null;
		if (tileBottom != null)
		{
			var locBottom = tileBottom.GetComponent<SkinnedMeshRenderer>().bounds.center;
			distBottom = Vector3.Distance(locBottom, mouseLoc);
		}

		var minDist = new[] {distLeft, distRight, distTop, distBottom}.Min();

		if (distRight == minDist)
		{
			// Full retard.
			templateCopy = HelperMethods.RotateMatrix(HelperMethods.RotateMatrix(HelperMethods.RotateMatrix(templateCopy, 5), 5), 5);
			buildingToBuildOrientation = Orientation.East;
		}
		else if (distLeft == minDist)
		{
			templateCopy = HelperMethods.RotateMatrix(templateCopy, 5);
			buildingToBuildOrientation = Orientation.West;
		}
		else if (distTop == minDist)
		{
			//do nothing, it's perfect~!
			buildingToBuildOrientation = Orientation.North;
		}
		else if (distBottom == minDist)
		{
			templateCopy = HelperMethods.RotateMatrix(HelperMethods.RotateMatrix(templateCopy, 5), 5);
			buildingToBuildOrientation = Orientation.South;
		}

		#endregion

		for (var j = 0; j < 5; j++)
		{
			for (var i = 0; i < 5; i++)
			{
				var dx = j - 2;
				var dy = i - 2;

				var x = (int) location.x + dx;
				var y = (int) location.y + dy;

				// If there's building or foundation on this part of the template..
				if (templateCopy[i, j] > 0)
				{
					if (IsValidBuildLocation(x, y))
					{
						Highlights.Add(DrawHighlightOverTile(new Vector2(x, y), templateCopy[i, j]));
					}
					else // INVALID MAP POS (Tile taken by building, over undrilled rock, etc..)
					{
						ClearBuildingHighlights();
						return;
					}
				}
			}
		}

		for (var index = 0; index < Highlights.Count; index++)
		{
			var hl = Highlights[index];
			if (hl == null) break;
			var mat = hl.GetComponent<SkinnedMeshRenderer>().material;
			if (mat == MaterialManager.GetBuildingDeniedHoverTile())
			{
				foreach (var gameObject in Highlights)
				{
					gameObject.GetComponent<SkinnedMeshRenderer>().material = MaterialManager.GetBuildingDeniedHoverTile();
				}
				return;
			}
		}

		HighlightBuildingTemplate = templateCopy;
	}

	public static bool IsLowTileClass(Tile tile)
	{
		return tile.TileClass.Equals(Tile.RockClass.Soil) || tile.TileClass.Equals(Tile.RockClass.Water) ||
			   tile.TileClass.Equals(Tile.RockClass.Lava);
	}

	private static GameObject DrawHighlightOverTile(Vector2 location, int buildingSegmentClass)
	{
		bool isPlaceableOnSoil = false, isPlaceableOnLava = false, isPlaceableOnWater = false, isBuilding = false;
		var tile = Tiles[(int) location.x, (int) location.y];

		#region Building Segment Class

		switch ((BuildingTile) buildingSegmentClass)
		{
			case BuildingTile.Empty:
				break;
			case BuildingTile.SoilBuilding:
				isPlaceableOnSoil = true;
				isBuilding = true;
				break;
			case BuildingTile.SoilFoundation:
				isPlaceableOnSoil = true;
				break;
			case BuildingTile.WaterBuilding:
				isPlaceableOnWater = true;
				isBuilding = true;
				break;
			case BuildingTile.WaterFoundation:
				isPlaceableOnWater = true;
				break;
			case BuildingTile.LavaBuilding:
				isPlaceableOnLava = true;
				isBuilding = true;
				break;
			case BuildingTile.LavaFoundation:
				isPlaceableOnLava = true;
				break;
			case BuildingTile.SoilAndWaterBuilding:
				isPlaceableOnWater = true;
				isPlaceableOnSoil = true;
				isBuilding = true;
				break;
			case BuildingTile.SoilAndWaterFoundation:
				isPlaceableOnWater = true;
				isPlaceableOnSoil = true;
				break;
			case BuildingTile.SoilAndLavaBuilding:
				isPlaceableOnLava = true;
				isPlaceableOnSoil = true;
				isBuilding = true;
				break;
			case BuildingTile.SoilAndLavaFoundation:
				isPlaceableOnLava = true;
				isPlaceableOnSoil = true;
				break;
			case BuildingTile.SoilWaterLavaBuilding:
				isPlaceableOnWater = true;
				isPlaceableOnLava = true;
				isPlaceableOnSoil = true;
				isBuilding = true;
				break;
			case BuildingTile.SoilWaterLavaFoundation:
				isPlaceableOnWater = true;
				isPlaceableOnLava = true;
				isPlaceableOnSoil = true;
				break;
			default:
				throw new ArgumentOutOfRangeException("buildingSegmentClass");
		}

		#endregion

		#region Material

		var canPlace = (tile.IsSoil && isPlaceableOnSoil) || (tile.IsLava && isPlaceableOnLava) ||
					   (tile.IsWater && isPlaceableOnWater);
		var mat = canPlace ? isBuilding ? MaterialManager.GetBuildingHoverTile() : MaterialManager.GetBuildingFoundationHoverTile() : MaterialManager.GetBuildingDeniedHoverTile();

		#endregion

		var tileGameObject = Tiles[(int) location.x, (int) location.y].TileGameObject;
		var verts = tileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices;
			//.Select(vert => new Vector3(vert.x, vert.y, vert.z)).ToArray();
		var inds = tileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.triangles;
		var gameObject = GenerateMapTile(tile, verts, inds, mat);
		gameObject.transform.localPosition = new Vector3(location.x, .1f, location.y);
		return gameObject;
	}

	public static void ClearBuildingHighlights()
	{
		foreach (var gameObject in Highlights)
		{
			Object.Destroy(gameObject);
		}
		BuildingToBuild = null;
	}

	#endregion

	/// <summary>
	/// 
	/// </summary>
	/// <param name="location">The location in the tile grid (not the world space).</param>
	/// <returns></returns>
	public static Vector3 GetRandomizedOffsetLocationForOreOrCrystal(Vector2 location)
	{
		//var tile = Tiles[(int)location.x, (int)location.y];
		var rand = new Random();

		// Horizontal coords with a small offset to spread tile contents out a little.
		var x = Tilesize * location.x + (rand.Next(0, 1) / 2f);
		var z = Tilesize * location.y + (rand.Next(0, 1) / 2f);

		// We need to know where to drop the tile decoration, as tiles aren't perfectly.
		// Raycast straight down from the location to find where the item should sit.
		RaycastHit hitInfo;
		Physics.Raycast(new Ray(new Vector3(x, 1, z), new Vector3(x, 0, z)), out hitInfo);

		var y = hitInfo.point.y;

		return new Vector3(x, y, z);
	}
	public static int[] GetResourcesOnTile(Vector2 vector2)
	{
		foreach (var resource in GameResourceMap)
		{
		}
		return new int[1];
	}


	public static GameObject[,] GetSurroundingBuildingTiles(Vector2 location)
	{
		var surroundsList = new GameObject[3, 3];

		for (var j = 0; j < 3; j++)
		{
			for (var i = 0; i < 3; i++)
			{
				var dx = j - 1;
				var dy = i - 1;

				var x = (int)location.x + dx;
				var y = (int)location.y + dy;

				if (TileExists(x, y))
				{
					if (GameBuildingTileMap[x, y] != null)
					{
						surroundsList[i, j] = Tiles[x, y].TileGameObject;
					}
				}
				else surroundsList[i, j] = null;

				//if (TileExists(x, y)) surroundsList[i, j] = (GameBuildingTileMap[x, y] != null ?? GameMap[x,y] : null);
				//else surroundsList[i,j] = null;
			}
		}

		return surroundsList;
	}

	public static BuildingInstance CreateBuildingFoundation(Vector2 location, IBuildingDefinition buildingToBuild)
	{
		// TODO: Check if we actually are allowed to place a building here before this method.

		var targetTile = Tiles[(int)location.x, (int)location.y].TileGameObject;

		var building = new BuildingInstance(buildingToBuild, location, buildingToBuildOrientation);
		GameBuildings.Add(building);

		var neighboringBuildings = 0;

		var neighbors = GetSurroundingBuildingTiles(location);

		//return building;

		for (var j = 0; j < 5; j++)
		{
			for (var i = 0; i < 5; i++)
			{
				var dx = j - 2;
				var dy = i - 2;

				var x = (int)location.x + dx;
				var y = (int)location.y + dy;

				if (GameBuildingTileMap[x, y] != null) neighboringBuildings++;
			}
		}

		Debug.Log("neighbors " + neighboringBuildings);

		//if (neighboringBuildings == 0)
		//{
		//    // No neighboring buildings, so flatten away. 
		//    FlattenTerrainToTileAverage(location);
		//    return building;
		//}
		//else if (neighboringBuildings == 1)
		//{
		//    float height = 0f;
		//    // Match height.
		//    foreach (var neighbor in neighbors)
		//    {
		//        height = neighbor.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices.First().y;
		//        break;
		//    }
		//    SetTerrainHeight(location, height);
		//}
		//else
		//{
		//    // Our target height.
		//    var avg = (from GameObject neighbor in neighbors where neighbor != null select neighbor.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices.Select(vert => vert.y).Average()).Average();

		//    SetTerrainHeight(location, avg);

		//    for (var j = 0; j < 5; j++)
		//    {
		//        for (var i = 0; i < 5; i++)
		//        {
		//            var dx = j - 2;
		//            var dy = i - 2;

		//            var x = (int)location.x + dx;
		//            var y = (int)location.y + dy;

		//            if (IsBuildingOnTile(x, y) && GetTileHeight(x,y) != avg) RecursivelySetTileHeight(x,y, avg);
		//        }
		//    }
		//}

		FlattenTerrain(location, buildingToBuild);

		for (var j = 0; j < 5; j++)
		{
			for (var i = 0; i < 5; i++)
			{
				var dx = j - 2;
				var dy = i - 2;

				var x = (int)location.x + dx;
				var y = (int)location.y + dy;

				// If there's building or foundation on this part of the template..
				if (HighlightBuildingTemplate[i, j] != 0)//%2 != 0)     // if the tile class is odd, which corresponds to tiles with buildings on them, by chance.
				{
					if (IsValidBuildLocation(x, y))
					{
						// TODO: Add to temporary variables and add after loops are done.
						var foundation = AddFoundationToTile(x, y, building);

						// Add foundation to map.
						GameBuildingTileMap[x, y] = foundation;
						building.Tiles.Add(foundation);
					}
					else // INVALID MAP POS (Tile taken by building, over undrilled rock, etc..)
					{
						ClearBuildingHighlights();
						return null;
					}
				}
			}
		}

		return building;
	}

	private static void ReplantBuildings()
	{
		// TODO: Write this.
		throw new NotImplementedException();
	}

	#region Map Wrapper

	/// <summary>
	/// Gets cells around a provided tile.
	/// </summary>
	/// <param name="x">X location of central tile.</param>
	/// <param name="y">Y location of central tile.</param>
	/// <param name="includeTargetCell">Toggle whether to include the central cell in the returned array.</param>
	/// <param name="fillBlankCells">Toggle whether cells that don't exist should default to a preset type.</param>
	/// <returns>Returns a new 3x3 2D array of cells surrounding a specified cell.</returns>
	public static Tile[,] GetSurroundingCells(int x, int y, bool includeTargetCell = true, bool fillBlankCells = false)
	{
		var width = Tiles.GetLength(0);
		var height = Tiles.GetLength(1);
		var blankTileFiller = fillBlankCells ? new Tile(MaterialManager.Biome.ROCK, Tile.RockClass.Solid) : null;
		var surroundsList = new Tile[3, 3];
		if (y - 1 != -1)
		{
			if (x - 1 != -1 && TileExists(x - 1, y - 1))
				surroundsList[0, 0] = Tiles[x - 1, y - 1];
			else surroundsList[0, 0] = blankTileFiller;
			if (TileExists(x, y - 1)) surroundsList[1, 0] = Tiles[x, y - 1];
			else surroundsList[1, 0] = blankTileFiller;
			if (x + 1 < width && TileExists(x + 1, y - 1))
				surroundsList[2, 0] = Tiles[x + 1, y - 1];
			else surroundsList[2, 0] = blankTileFiller;
		}
		if (x - 1 != -1 && TileExists(x - 1, y))
			surroundsList[0, 1] = Tiles[x - 1, y];
		else surroundsList[0, 1] = blankTileFiller;
		surroundsList[1, 1] = includeTargetCell ? Tiles[x, y] : fillBlankCells ? blankTileFiller : null;
		if (x + 1 < width && TileExists(x + 1, y))
			surroundsList[2, 1] = Tiles[x + 1, y];
		else surroundsList[2, 1] = blankTileFiller;
		if (x - 1 != -1 && y + 1 < height && TileExists(x - 1, y + 1))
			surroundsList[0, 2] = Tiles[x - 1, y + 1];
		else surroundsList[0, 2] = blankTileFiller;
		if (y + 1 < height && TileExists(x, y + 1))
			surroundsList[1, 2] = Tiles[x, y + 1];
		else surroundsList[1, 2] = blankTileFiller;
		if (x + 1 < width && y + 1 < height && TileExists(x + 1, y + 1))
			surroundsList[2, 2] = Tiles[x + 1, y + 1];
		else surroundsList[2, 2] = blankTileFiller;
		return surroundsList;
	}

	/// <summary>
	/// Gets cells around a provided tile.
	/// </summary>
	/// <param name="location">Location of central tile.</param>
	/// <param name="includeTargetCell">Toggle whether to include the central cell in the returned array.</param>
	/// <param name="fillBlankCells">Toggle whether cells that don't exist should default to a preset type.</param>
	/// <returns>Returns a new 3x3 2D array of cells surrounding a specified cell.</returns>
	public static Tile[,] GetSurroundingCells(Vector2 location, bool includeTargetCell = true,
		bool fillBlankCells = false)
	{
		return GetSurroundingCells((int) location.x, (int) location.y, includeTargetCell, fillBlankCells);
	}

	/// <summary>
	/// Checks if a specified tile exists.
	/// Performs the boring basic checks to make sure cell coordinates aren't outside sane bounds.
	/// </summary>
	/// <param name="x">X location of tile to investigate.</param>
	/// <param name="y">Y location of tile to investigate.</param>
	/// <returns>Boolean to indicate whether tile exists.</returns>
	private static bool TileExists(int x, int y)
	{
		return (x > 0 && y > 0 && x < Tiles.GetLength(0) && y < Tiles.GetLength(1));
	}

	/// <summary>
	/// Sets the height of the a tile to prepare it for a building being put on it, and then does the same for any neighboring buildings.
	/// This is done in the following way:
	/// If there are no buildings nearby, we get the average height of all the verticies of the tile and flatten the tile to that height.
	/// If there is one building neighboring we get the height of that building's foundation and use it as a reference for our tile's desired height.
	/// If there are multiple buildings neighboring, we average their heights, set our tile's height to that height and then set their tile heights to match (if they don't already), 
	/// invoking this method on those neighbor tiles (but only if the height is different, to prevent infinite recursion).
	/// TODO: Determine whether this will be invoked forever if two neighbors have the same height? 
	/// </summary>
	/// <param name="x">X location of tile to set height.</param>
	/// <param name="y">Y location of tile to set height.</param>
	/// <param name="height"></param>
	private static void RecursivelySetTileHeight(int x, int y, float height)
	{
		for (var j = 0; j < 5; j++)
		{
			for (var i = 0; i < 5; i++)
			{
				var dx = j - 2;
				var dy = i - 2;

				var x1 = x + dx;
				var y1 = y + dy;

				if (IsBuildingOnTile(x1, y1) && GetTileHeight(x1, y1) != height)
				{
					SetTerrainHeight(new Vector2(x, y), height);
					SetTileContentsHeight(x, y, height);
					RecursivelySetTileHeight(x, y, height);
				}
			}
		}
	}

	/// <summary>
	/// Sets the height of a tile to a provided value.
	/// </summary>
	/// <param name="x">Tile loa</param>
	/// <param name="y"></param>
	/// <param name="height"></param>
	private static void SetTileContentsHeight(int x, int y, float height)
	{
		// TODO: Upgrade for Ore, Crystals.

		// Adjust buildings.
		var tile = GameBuildingTileMap[x, y];
		if (tile != null)
		{
			var pos = tile.BuildingModel.transform.position;
			GameBuildingTileMap[x, y].BuildingModel.transform.position = new Vector3(pos.x, height, pos.z);
		}
	}

	private static void SetTerrainHeight(Vector2 location, float height)
	{
		var x = (int) location.x;
		var y = (int) location.y;

		var targetTile = Tiles[x, y].TileGameObject;
		var targetsmr = targetTile.GetComponent<SkinnedMeshRenderer>();
		var verts = targetsmr.sharedMesh.vertices;
		targetTile.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices =
			verts.Select(vert => new Vector3(vert.x, height, vert.z)).ToArray();
		targetTile.GetComponent<MeshCollider>().sharedMesh = targetsmr.sharedMesh;

		//NW
		if (TileExists(x - 1, y - 1))
		{
			var tile = Tiles[x - 1, y - 1].TileGameObject;
			var smr = tile.GetComponent<SkinnedMeshRenderer>();
			var newVerts = smr.sharedMesh.vertices;
			newVerts[2].y = height; // This magic number represents the vert closest to the tile we set the height of.
			smr.sharedMesh.vertices = newVerts;
			tile.GetComponent<MeshCollider>().sharedMesh = smr.sharedMesh;
			smr.sharedMesh.RecalculateNormals();
			smr.sharedMesh.RecalculateBounds();
		}
		//N
		if (TileExists(x, y - 1))
		{
			var tile = Tiles[x, y - 1].TileGameObject;
			var smr = tile.GetComponent<SkinnedMeshRenderer>();
			var newVerts = smr.sharedMesh.vertices;
			newVerts[1].y = height;
			newVerts[2].y = height;
			smr.sharedMesh.vertices = newVerts;
			tile.GetComponent<MeshCollider>().sharedMesh = smr.sharedMesh;
			smr.sharedMesh.RecalculateNormals();
			smr.sharedMesh.RecalculateBounds();
		}
		//NE
		if (TileExists(x + 1, y - 1))
		{
			var tile = Tiles[x + 1, y - 1].TileGameObject;
			var smr = tile.GetComponent<SkinnedMeshRenderer>();
			var newVerts = smr.sharedMesh.vertices;
			newVerts[1].y = height; // This magic number represents the vert closest to the tile we set the height of.
			smr.sharedMesh.vertices = newVerts;
			tile.GetComponent<MeshCollider>().sharedMesh = smr.sharedMesh;
			smr.sharedMesh.RecalculateNormals();
			smr.sharedMesh.RecalculateBounds();
		}
		//E
		if (TileExists(x + 1, y))
		{
			var tile = Tiles[x + 1, y].TileGameObject;
			var smr = tile.GetComponent<SkinnedMeshRenderer>();
			var newVerts = smr.sharedMesh.vertices;
			newVerts[0].y = height;
			newVerts[1].y = height;
			smr.sharedMesh.vertices = newVerts;
			tile.GetComponent<MeshCollider>().sharedMesh = smr.sharedMesh;
			smr.sharedMesh.RecalculateNormals();
			smr.sharedMesh.RecalculateBounds();
		}
		//SW
		if (TileExists(x - 1, y + 1))
		{
			var tile = Tiles[x - 1, y + 1].TileGameObject;
			var smr = tile.GetComponent<SkinnedMeshRenderer>();
			var newVerts = smr.sharedMesh.vertices;
			newVerts[3].y = height; // This magic number represents the vert closest to the tile we set the height of.
			smr.sharedMesh.vertices = newVerts;
			tile.GetComponent<MeshCollider>().sharedMesh = smr.sharedMesh;
			smr.sharedMesh.RecalculateNormals();
			smr.sharedMesh.RecalculateBounds();
		}
		//W
		if (TileExists(x - 1, y))
		{
			var tile = Tiles[x - 1, y].TileGameObject;
			var smr = tile.GetComponent<SkinnedMeshRenderer>();
			var newVerts = smr.sharedMesh.vertices;
			newVerts[2].y = height;
			newVerts[3].y = height;
			smr.sharedMesh.vertices = newVerts;
			tile.GetComponent<MeshCollider>().sharedMesh = smr.sharedMesh;
			smr.sharedMesh.RecalculateNormals();
			smr.sharedMesh.RecalculateBounds();
		}
		//SE
		if (TileExists(x + 1, y + 1))
		{
			var tile = Tiles[x + 1, y + 1].TileGameObject;
			var smr = tile.GetComponent<SkinnedMeshRenderer>();
			var newVerts = smr.sharedMesh.vertices;
			newVerts[0].y = height; // This magic number represents the vert closest to the tile we set the height of.
			smr.sharedMesh.vertices = newVerts;
			tile.GetComponent<MeshCollider>().sharedMesh = smr.sharedMesh;
			smr.sharedMesh.RecalculateNormals();
			smr.sharedMesh.RecalculateBounds();
		}
		//S
		if (TileExists(x, y + 1))
		{
			var tile = Tiles[x, y + 1].TileGameObject;
			var smr = tile.GetComponent<SkinnedMeshRenderer>();
			var newVerts = smr.sharedMesh.vertices;
			newVerts[0].y = height;
			newVerts[3].y = height;
			smr.sharedMesh.vertices = newVerts;
			tile.GetComponent<MeshCollider>().sharedMesh = smr.sharedMesh;
			smr.sharedMesh.RecalculateNormals();
			smr.sharedMesh.RecalculateBounds();
		}

		SetTileContentsHeight(x, y, height);
	}

	/// <summary>
	/// Flattens the verticies of a tile to the average height of all of them.
	/// </summary>
	/// <param name="location"></param>
	private static void FlattenTerrainToTileAverage(Vector2 location)
	{
		var targetTile = Tiles[(int) location.x, (int) location.y].TileGameObject;
		var verts = targetTile.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices;
		var avg = verts.Select(vert => vert.y).Average();
		SetTerrainHeight(location, avg);
		//targetTile.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices =
		//    verts.Select(vert => new Vector3(vert.x, avg, vert.z)).ToArray();
	}

	private static void FlattenTerrain(Vector2 location, IBuildingDefinition buildingToBuild)
	{
		var targetTile = Tiles[(int) location.x, (int) location.y].TileGameObject;
		var buildingcount = 0;

		float? height = null;

		for (var j = 0; j < 3; j++)
		{
			for (var i = 0; i < 3; i++)
			{
				if (i == 1 && j == 1) break;
				var dx = j - 1;
				var dy = i - 1;

				var x = (int) location.x + dx;
				var y = (int) location.y + dy;

				// If there's a tile to look at, and there's a building already on that tile, get the height of that and use it to set
				if (TileExists(x, y) && IsBuildingOnTile(x, y))
				{
					var tileHeight = GetTileHeight(x, y);
					if (height == null) height = tileHeight;
					else height = (height + GetTileHeight(x, y))/2;
					buildingcount++;
				}
			}
		}

		if (buildingcount == 0)
		{
			// Average height of neighbor verts and call it a day.
			FlattenTerrainToTileAverage(location);
		}
		else if (buildingcount == 1)
		{
			// Set height to match only neighbor.
			SetTerrainHeight(location, height.Value);
		}
		else
		{
			// Worst case; now we reconcile multiple heights, oy vey!
			RecursivelySetTileHeight((int) location.x, (int) location.y, height.Value);
		}


		// Find building segments directly adjacent to this tile, check if they're the same height as this tile, if not, adjust accordingly.
	}

	private static void FlattenTileAtSetHeight(int x, int y, float height)
	{
		var targetTile = Tiles[x, y].TileGameObject;
		var smr = targetTile.GetComponent<SkinnedMeshRenderer>();
		var mesh = smr.GetComponent<Mesh>();
		mesh.vertices = mesh.vertices.Select(vert => new Vector3(vert.x, (float) height, vert.z)).ToArray();

		SetTerrainHeight(new Vector2(x, y), height);

		for (var j = 0; j < 3; j++)
		{
			for (var i = 0; i < 3; i++)
			{
				var dx = j - 1;
				var dy = i - 1;

				var x1 = x + dx;
				var y1 = y + dy;

				if (IsBuildingOnTile(x1, y1) && GetTileHeight(x1, y1) == height)
				{
					// do nothing
				}
			}
		}
	}

	// Returns the average height of a tile. 
	private static float GetTileHeight(int x, int y)
	{
		return
			Tiles[x, y].TileGameObject.GetComponent<SkinnedMeshRenderer>()
				.sharedMesh.vertices.Select(vert => vert.y)
				.Average();
	}

	private static bool IsValidBuildLocation(int x, int y)
	{
		if (TileExists(x, y) && IsLowTileClass(Tiles[x, y]) && GameBuildingTileMap[x, y] == null)
		{
			return true;
		}
		return false;
	}

	// Determines whether there is part of a building on a tile.
	// Note: this means an actual chunk of building (3D model), not foundation, etc. 
	private static bool IsBuildingOnTile(int x, int y)
	{
		if (GameBuildingTileMap[x, y] != null && GameBuildingTileMap[x, y].BuildingModel != null) return true;
		else return false;
		// I don't know matey.
		//throw new NotImplementedException();
	}
	
	#endregion

	private static BuildingTileInstance AddFoundationToTile(int x, int y, BuildingInstance building)
	{
		//var tile = Tiles[x, y].TileGameObject;
		//var smr = tile.GetComponent<SkinnedMeshRenderer>();
		Tiles[x, y].IsFoundation = true;
		//var mesh = smr.sharedMesh;
		//var foundation = GenerateMapTile(null, mesh.vertices.Select(vert => new Vector3(vert.x, vert.y += .001f, vert.z)).ToArray(), mesh.triangles, TileMaterialLookup[48]);
		//foundation.transform.localPosition = new Vector3(x, 0, y);
		return new BuildingTileInstance(x, y, building);
	}

	/// <summary>
	/// Create building.
	/// Also set model height.
	/// </summary>
	/// <param name="target"></param>
	/// <param name="building"></param>
	public static void CreateBuilding(Vector2 target, BuildingInstance building)
	{
		var targetPos = Tiles[(int)target.x, (int)target.y].TileGameObject.transform.position;
		var gameObject = new GameObject { name = "building" };
		foreach (var model in building.GetBuildingDefinition().GetModels())
		{
			LoadModelGroup(model, target, targetPos, building);
		}
	}

	public static void LoadModelGroup(List<Model> models, Vector2 target, Vector3 targetPos, BuildingInstance building)
	{
		var index = 0;
		var workList = new List<Action<Model>>();
		var flameList = new List<TeleportFireEffectScript>();

		Action<Model> LoadModel = (model) =>
		{
			var buildingModelScript = Map.LoadModel(target, targetPos, building, model);

			var flames = buildingModelScript.GetComponentsInChildren<TeleportFireEffectScript>();
			flameList.AddRange(flames);
			flames.ForEach(flame => flame.ShowFlames());

			Action teleportCompleteAction = () =>
			{
				index++;
				if (index == models.Count - 1) flameList.ForEach(flame => flame.HideFlames());
				if (index < models.Count) workList[index](models[index]);
			};

			buildingModelScript.TeleportInComplete += () =>
			{
				teleportCompleteAction();
			};
			
			if (model.Teleport) buildingModelScript.TeleportIn();
			//else teleportCompleteAction();
		};

		workList = Enumerable.Repeat(LoadModel, models.Count).ToList();
		workList.First()(models.First());
	}

	private static BuildingModelScript LoadModel(Vector2 target, Vector3 targetPos, BuildingInstance building, Model model)
	{
		var angle = GetAngleFromOrientation(building.TemplateOrientation);
		var tform = targetPos + model.OffsetTransform;
		tform.y = GetTileHeight((int) target.x, (int) target.y);

		var buildingInstance = Object.Instantiate(model.Asset, tform, new Quaternion()) as GameObject;
		var buildingModelScript = buildingInstance.AddComponent<BuildingModelScript>();

		buildingInstance.transform.RotateAround(targetPos + new Vector3(Tilesize/2f, 0, Tilesize/2f), Vector3.up, angle);
		building.Models.Add(buildingInstance);
		return buildingModelScript;
	}

	private static int GetAngleFromOrientation(Orientation orientation)
	{
		switch (orientation)
		{
			case Orientation.North:
				return 180;
				break;
			case Orientation.East:
				return 270;
				break;
			case Orientation.West:
				return 90;
				break;
		}
		return 0;   // S
	}

	public static void CreatePowerPath(GameObject targetTile)
	{
		var location = GetGameObjectCoords(targetTile);
		var tile = Tiles[(int) location.x, (int) location.y];
		if (tile != null) tile.IsPowerPath = true;
	}
}

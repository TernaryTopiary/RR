using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class TileScript : MonoBehaviour
{
    public MapScript MapReference;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter()
	{
	}
	void OnMouseExit()
	{
	}

	void OnMouseUp()
	{
		if (gameObject.name.Contains("selectableMapTile"))
		{
            TileClick();
		}
	}

    public float v0
    {
        get { return GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices[0].y; }
        set
        {
            var verts = GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices;
            verts[0].y = value;
        }
    }

    public float v1
    {
        get { return GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices[1].y; }
        set
        {
            var verts = GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices;
            verts[1].y = value;
        }
    }

    public float v2
    {
        get { return GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices[2].y; }
        set
        {
            var verts = GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices;
            verts[2].y = value;
        }
    }

    public float v3
    {
        get { return GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices[3].y; }
        set
        {
            var verts = GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices;
            verts[3].y = value;
        }
    }

    private void TileClick()
    {
        if (MapReference != null) MapReference.TileClickEvent(gameObject);
    }

    //private void Unselect()
    //{
    //    var mr = gameObject.GetComponent<SkinnedMeshRenderer>();
    //    var mats = mr.materials.ToList();
    //    mr.materials = mats.Except(mats.Where(mat => mat.name.Contains(MapConstants.TintSelected.name))).ToArray();
    //}

}

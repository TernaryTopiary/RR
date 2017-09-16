using System;
using UnityEngine;

public interface IDecoration
{
    Vector2 GetLocationInTileMap();
}

public class TileAugmentation : IDecoration
{
    public Vector2 GetLocationInTileMap()
    {
        throw new System.NotImplementedException();
    }
}

public class Reinforcement : IDecoration
{
    public Vector2 GetLocationInTileMap()
    {
        throw new System.NotImplementedException();
    }
}

public interface ICarryable : IDecoration
{
    bool IsBeingCarried { get; }
}

public class Dynamite : ICarryable
{
    public Vector2 GetLocationInTileMap()
    {
        throw new System.NotImplementedException();
    }

    public bool IsBeingCarried
    {
        get { throw new System.NotImplementedException(); }
    }
}

public interface IResource : ICarryable
{
    Vector3 GetLocationInWorldSpace();
}

public class Ore : IResource
{
    public GameObject GameObject;

    public Vector2 GetLocationInTileMap()
    {
        throw new NotImplementedException();
    }

    public Vector3 GetLocationInWorldSpace()
    {
        return GameObject.transform.position;
    }

    public bool IsBeingCarried
    {
        get { throw new System.NotImplementedException(); }
    }
}

public class Crystal : IResource
{
    public GameObject GameObject;

    public Vector2 GetLocationInTileMap()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetLocationInWorldSpace()
    {
        return GameObject.transform.position;
    }

    public bool IsBeingCarried
    {
        get { throw new System.NotImplementedException(); }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using AIexLibrary;

public class EntityBehaviour : MonoBehaviour {
    public Action interactMethod;
    public List<Action> rightClickMethods=new List<Action>();
    /// <summary>
    /// Deprecated. Use rightClickMethods instead.
    /// </summary>
    public Action rightClickMethod;
    public Action leftClickMethod;
    /// <summary>
    /// Called in tile.InitializeGameObject().
    /// </summary>
    public Action startMethod;
    public bool canBuildInTop;
    /// <summary>
    /// Deprecated. Use canBuildInTop instead.
    /// </summary>
    public bool canNail;
    public MapUtils.Tile linkedTile;

    private void Awake()
    {
        if (canBuildInTop)
        {
            rightClickMethods.Add(BuildOnTop);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            foreach(Action method in rightClickMethods)
            {
                method();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            leftClickMethod();
        }
    }

    public void BuildOnTop()
    {
        if (linkedTile.parentRegion.GetTile(linkedTile.tileX, linkedTile.tileY, 1) == null)
        {
            //Later to be replaced with Client.Build(x,y,tileId); This one will check in the user's inv.
            linkedTile.parentRegion.SetTile(linkedTile.tileX, linkedTile.tileY, 1, new MapUtils.Tile(linkedTile.parentRegion, linkedTile.tileX, linkedTile.tileY, 1, TileUtils.FindTileData("0001Stone")));
            GameEngine.instance.LoadRegion(linkedTile.parentRegion.regionX, linkedTile.parentRegion.regionY,linkedTile.parentRegion.parentMap);
            linkedTile.parentRegion.GetTile(linkedTile.tileX, linkedTile.tileY, 1).tileZPos = 1;
            linkedTile.parentRegion.linkedObject.GetComponent<RegionRenderer>().Reload();
        }
        else
            Debug.LogWarning("Can't build in top: Space 1 already used!");
    }
}

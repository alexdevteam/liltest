using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIexLibrary;

public class RegionRenderer : MonoBehaviour {
    public MapUtils.Region reg;
    public GameObject tilePrefab;

    public void Start()
    {
        Reload();
    }
    /// <summary>
    /// Reload the entire region.
    /// </summary>
    public void Reload () {
        foreach (MapUtils.Tile tile in reg.tiles)
        {
            //Do all the stuff here!
            //First, if the tile hasn't got an object AND it is not null...
            if (tile == null) { }

            else if (tile.linkedObject == null)
            {
                //Create a gameobject for it!
                GameObject x = Instantiate(tilePrefab, transform, true);
                x.transform.position= new Vector3(tile.tileX + tile.parentRegion.regionX * 10 - 10, 0f, tile.tileY + tile.parentRegion.regionY * 10 - 10);

                //Set the Y position of the object.
                if (tile.tileZPos == 0)
                {
                    x.transform.Translate(Vector3.up * tile.tileZ);
                }
                else
                {
                    x.transform.Translate(Vector3.up * tile.tileZPos);
                }

                //Link the tile to the object created.
                tile.linkedObject = x;
                tile.InitializeGameObject();
            }

            //Or if the tile has got an object BUT it is not updated...
            //else if()
        }
	}

    public void DestroyTile(MapUtils.Tile tile)
    {
        Destroy(tile.linkedObject);
        tile.parentRegion.SetTile(tile.tileX, tile.tileY, tile.tileZ,null);
    } 
}

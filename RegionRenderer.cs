using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIexLibrary;

public class RegionRenderer : MonoBehaviour {
    public MapUtils.Region reg;
    public GameObject tilePrefab;
    public GameObject lightObstaclePrefab;

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

            //If the tile hasn't got an object BUT it isn't null...
            else if (tile.linkedObject == null)
            {
                //Create a gameobject for it!
                GameObject x = Instantiate(tilePrefab, transform, true);
                x.transform.position = new Vector3(tile.tileX + tile.parentRegion.regionX * 10 - 10, tile.tileY + tile.parentRegion.regionY * 10 - 10, 0f);
                GameObject y = Instantiate(lightObstaclePrefab, x.transform, true);
                y.transform.position = new Vector3(tile.tileX + tile.parentRegion.regionX * 10 - 10, tile.tileY + tile.parentRegion.regionY * 10 - 10, 0f);

                //Set the Y position of the object.

                //Link the tile to the object created.
                tile.linkedObject = x;
                tile.InitializeGameObject();
            }

            

            if (tile == null) { }

            //Or if the tile has got an object, just set their joining properties.
            else if(tile.tileData.usingTileset)
            {
                if (reg.GetTile(tile.tileX + 1, tile.tileY, tile.tileZ) == null)
                {
                    // ???
                    // ?#.
                    // ???
                    if (reg.GetTile(tile.tileX - 1, tile.tileY, tile.tileZ) == null)
                    {
                        // ???
                        // .#.
                        // ???

                        if (reg.GetTile(tile.tileX, tile.tileY + 1, tile.tileZ) == null)
                        {
                            // ?.?
                            // .#.
                            // ???

                            if (reg.GetTile(tile.tileX, tile.tileY - 1, tile.tileZ) == null)
                            {
                                // ?.?
                                // .#.
                                // ?.?
                                // NEED A TILE FOR THIS
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[4];

                            }
                            else
                            {
                                // ?.?
                                // .#.
                                // .#.
                                // NEED A TILE FOR THIS
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[4];
                            }
                        }
                        else
                        {
                            // ?#?
                            // .#.
                            // ???

                            if (reg.GetTile(tile.tileX, tile.tileY - 1, tile.tileZ) == null)
                            {
                                // ?#?
                                // .#.
                                // ?.?
                                // NEED A TILE FOR THIS
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[4];
                            }
                            else
                            {
                                // ?#?
                                // .#.
                                // ?#?
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[4];
                            }
                        }
                    }
                    else
                    {
                        // ???
                        // ##.
                        // ???

                        if (reg.GetTile(tile.tileX, tile.tileY + 1, tile.tileZ) == null)
                        {
                            // ?.?
                            // ##.
                            // ???

                            if (reg.GetTile(tile.tileX, tile.tileY - 1, tile.tileZ) == null)
                            {
                                // ?.?
                                // ##.
                                // ?.?
                                // NEED A TILE FOR THIS
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[4];

                            }
                            else
                            {
                                // ?.?
                                // ##.
                                // ?#?
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[2];
                                //tile.linkedObject.transform.GetChild(0).localPosition = new Vector2(-0.005f, -0.02f);
                            }
                        }
                        else
                        {
                            if (reg.GetTile(tile.tileX, tile.tileY - 1, tile.tileZ) == null)
                            {
                                // ?#?
                                // ##.
                                // ?.?
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[8];
                                //tile.linkedObject.transform.GetChild(0).localPosition = new Vector2(-0.01f, 0.01f);


                            }
                            else
                            {
                                // ?#?
                                // ##.
                                // ?#?
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[5];
                            }
                        }
                    }
                }
                else
                {
                    // ???
                    // ?##
                    // ???
                    
                    if (reg.GetTile(tile.tileX - 1, tile.tileY, tile.tileZ) == null)
                    {
                        // ???
                        // .##
                        // ???

                        if (reg.GetTile(tile.tileX, tile.tileY + 1, tile.tileZ) == null)
                        {
                            // ?.?
                            // .##
                            // ???

                            if (reg.GetTile(tile.tileX, tile.tileY - 1, tile.tileZ) == null)
                            {
                                // ?.?
                                // .##
                                // ?.?
                                // NEED A TILE FOR THIS
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[4];

                            }
                            else
                            {
                                // ?.?
                                // .##
                                // ?#?
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[0];
                                //tile.linkedObject.transform.GetChild(0).localPosition = new Vector2(0f, -0.02f);
                            }
                        }
                        else
                        {
                            // ?#?
                            // .##
                            // ???

                            if (reg.GetTile(tile.tileX, tile.tileY - 1, tile.tileZ) == null)
                            {
                                // ?#?
                                // .##
                                // ?.?
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[6];
                                //tile.linkedObject.transform.GetChild(0).localPosition = new Vector2(0.01f, 0.01f);
                            }
                            else
                            {
                                // ?#?
                                // .##
                                // ?#?
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[3];
                            }
                        }
                    }
                    else
                    {
                        // ???
                        // ###
                        // ???
                        
                        if(reg.GetTile(tile.tileX,tile.tileY+1, tile.tileZ) == null)
                        {
                            // ?.?
                            // ###
                            // ???

                            if (reg.GetTile(tile.tileX, tile.tileY - 1, tile.tileZ) == null)
                            {
                                // ?.?
                                // ###
                                // ?.?
                                // NEED A TILE FOR THIS
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[4];
                            }
                            else
                            {
                                // ?.?
                                // ###
                                // ?#?
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[1];
                            }
                        }
                        else
                        {
                            // ?#?
                            // ###
                            // ???

                            if (reg.GetTile(tile.tileX, tile.tileY - 1, tile.tileZ) == null)
                            {
                                // ?#?
                                // ###
                                // ?.?
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[7];
                                //tile.linkedObject.transform.GetChild(0).localPosition = new Vector2(0, 0.01f);
                            }
                            else
                            {
                                // ?#?
                                // ###
                                // ?#?
                                tile.linkedObject.GetComponent<SpriteRenderer>().sprite = tile.tileData.tileset[4];
                            }
                        }
                    }
                }
            }

            if (tile == null) { }

            
            else
            {
                tile.FinishLight();

            }

            

        }
	}

    public void DestroyTile(MapUtils.Tile tile)
    {
        Destroy(tile.linkedObject);
        tile.parentRegion.SetTile(tile.tileX, tile.tileY, tile.tileZ,null);
    }

    void SetLighting(MapUtils.Tile tile, int part)
    {
        tile.linkedObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = TileDatabase.instance.FindEntity("Light").tileset[part];
    }
}

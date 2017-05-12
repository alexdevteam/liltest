using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIexLibrary;
using UnityEngine.AI;

public class GameEngine : MonoBehaviour {
    [SerializeField] static GameEngine instance;
    [SerializeField] public GameObject player;
    [SerializeField] public Vector2 playerRegPos;
    [SerializeField] MapUtils.Map level;
    [SerializeField] int mapLength;
    [SerializeField] int mapWidth;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject activeRegionPrefab;
    bool shouldBeInActive = true;
    MapUtils.Region[,] lastRegions=new MapUtils.Region[1,1];
    int lateDelay = 0;
    void Awake()
    {
        lastRegions = new MapUtils.Region[mapLength, mapWidth];
        instance = this;
        TileUtils.GetTileData();
         level = MapUtils.GenerateMap(mapLength, mapWidth,"tfddsf",50,2);
    }
    private void Update()
    {
        //Maximum number of regions in which the player can be active: 20.
        MapUtils.Region[] activeRegions=new MapUtils.Region[20];
        int activeRegionCount = 0;
        //Check for regions in which the player is active.
        foreach (MapUtils.Region reg in level.regions)
        {
            if (((reg.regionX * 10+10) > player.transform.position.x && (reg.regionX * 10 -20) < player.transform.position.x)
                && ((reg.regionY*10+5) > player.transform.position.z && (reg.regionY * 10 -20) < player.transform.position.z))
            {
                activeRegions[activeRegionCount] = reg;
                activeRegionCount++;
            }
            else if ((lastRegions[reg.regionX, reg.regionY] == reg)&&(reg.linkedObject!=null))
            {
                foreach (Transform child in reg.linkedObject.transform)
                {
                    Destroy(child.gameObject);
                }
                Destroy(reg.linkedObject);
            }
            if (reg.tiles[0, 0, 0].linkedObject != null)
            {
                if (((reg.regionX * 10) > player.transform.position.x && (reg.regionX * 10 - 10) < player.transform.position.x)
                && ((reg.regionY * 10) > player.transform.position.z && (reg.regionY * 10 - 10) < player.transform.position.z))
                {
                    reg.tiles[0, 0, 0].linkedObject.transform.parent.GetChild(0).GetComponent<LocalNavMeshBuilder>().enabled = true;
                    playerRegPos = new Vector2(reg.regionX, reg.regionY);
                }
                else
                    reg.tiles[0, 0, 0].linkedObject.transform.parent.GetChild(0).GetComponent<LocalNavMeshBuilder>().enabled = false;
            }
        }
        foreach(MapUtils.Region reg in activeRegions)
        {
            if(reg!=null)
                LoadRegion(reg.regionX, reg.regionY,level);
        }
        
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), new GUIContent(playerRegPos.ToString()));
    }

    public void LoadRegion(int x, int y, MapUtils.Map map)
    {
        var reg = map.GetRegion(x, y);
        
        //If the region holder is null, create one!
        if (reg.linkedObject == null) {
            GameObject regHolder = Instantiate(activeRegionPrefab, new Vector3(reg.regionX * 10 - 10, 0f, reg.regionY * 10 - 10), Quaternion.Euler(Vector3.zero)) as GameObject;
            reg.linkedObject = regHolder;
            regHolder.GetComponent<RegionRenderer>().reg = reg;
            lastRegions[reg.regionX, reg.regionY] = reg;
        }
    }
}

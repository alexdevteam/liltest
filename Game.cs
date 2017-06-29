using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Made by AlexINF 2017
namespace AIexLibrary
{
    public class MapUtils
    {
        public class Tile
        {
            public GameObject linkedObject;
            public TileDatabase.Entity tileData;
            public Region parentRegion;
            public int tileX;
            public int tileY;
            public int tileZ;
            public int tileZPos=0;
            public Tile(Region parent, int x, int y, int z, TileDatabase.Entity data)
            {
                tileX = x;
                tileY = y;
                tileZ = z;
                parentRegion = parent;
                tileData = data;
            }
            public void InitializeGameObject()
            {
                linkedObject.GetComponent<SpriteRenderer>().sprite = tileData.sprite;
                tileData.linkedTile = this;
                tileData.Init();
            }
            public void FinishLight()
            {
                if (!tileData.collidable)
                {
                    linkedObject.GetComponent<SpriteRenderer>().sortingOrder = -5;
                    linkedObject.transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    linkedObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    linkedObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = TileDatabase.instance.FindEntity("Light").tileset[(tileData.tileset as List<Sprite>).IndexOf(linkedObject.GetComponent<SpriteRenderer>().sprite)];
                    linkedObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    linkedObject.transform.GetChild(0).gameObject.GetComponent<Light2D.LightObstacleGenerator>().enabled = true;
                }
            }
        }

        /// <summary>
        /// The map region class. Each map divides into a list of regions.
        /// </summary>
        public class Region
        {
            public Map parentMap;
            public int regionX;
            public int regionY;
            public GameObject linkedObject;
            /// <summary>
            /// The tiles of the region. Each region is composed by a list of 10x10x5 tiles.
            /// The Z axis, 5, is used for having more than one tile in each position.
            /// The 0 Z axis will ALWAYS exist, unless there is no floor (Which is mostly due
            /// to a connection error.)
            /// </summary>
            public Tile[,,] tiles;
            const int length = 10;
            const int width = 10;
            /// <summary>
            /// The size of the region. [Not counting Z values]
            /// </summary>
            const int size = 100;
            ///<summary>
            ///Create blank region. (10x10 tiles)
            ///</summary>
            public Region(Map parent, int x, int y)
            {
                tiles = new Tile[length, width,5];
                parentMap = parent;
                regionX = x;
                regionY = y;
                for (int i = 0; i < size; i++)
                    tiles[i % length, Mathf.FloorToInt(i / length),0] = new Tile(this, i % length, Mathf.FloorToInt(i*1f / length*1f),0, TileDatabase.instance.FindEntity("Background Stone"));
            }
            public Region(Map parent, int x, int y, Tile[,,] data)
            {
                if (size != data.Length)
                {
                    Debug.LogError("Size not matching data given! [Engine.Region.Region(Tile[,,] data)]");
                    return;
                }
                parentMap = parent;
                tiles = data;
                regionX = x;
                regionY = y;
            }
            public void SetTile(int x, int y, int z, Tile tile)
            {
                tiles[x, y, z] = tile;
            }
            public Tile GetTile(int x, int y, int z)
            {
                if (x > 9)
                {
                    if (y > 9)
                    {
                        return parentMap.GetRegion(regionX + 1, regionY + 1).GetTile(x-10,y-10, z);
                    }
                    else if (0 > y)
                    {
                        return parentMap.GetRegion(regionX + 1, regionY - 1).GetTile(x-10, y+10, z);
                    }
                    else
                    {
                        return parentMap.GetRegion(regionX + 1, regionY).GetTile(x - 10, y, z);
                    }
                }
                else if (0 > x)
                {
                    if (y > 9)
                    {
                        return parentMap.GetRegion(regionX - 1, regionY + 1).GetTile(x + 10, y - 10, z);
                    }
                    else if (0 > y)
                    {
                        return parentMap.GetRegion(regionX - 1, regionY - 1).GetTile(x + 10, y + 10, z);
                    }
                    else
                    {
                        return parentMap.GetRegion(regionX - 1, regionY).GetTile(x + 10, y, z);
                    }
                }
                if (y > 9)
                {
                    return parentMap.GetRegion(regionX, regionY + 1).GetTile(x, y - 10, z);
                }
                else if (0 > y)
                {
                    return parentMap.GetRegion(regionX, regionY - 1).GetTile(x, y + 10, z);
                }
                try
                {
                    return tiles[x, y, z];
                }
                catch
                {
                    return null;
                }
                
            }
            public Tile[] GetTiles(int x, int y)
            {
                Tile[] list = new Tile[5];
                for (int i = 0; i < 5; i++)
                {
                    list[i] = tiles[x, y, i];
                }
                return list;
            }
        }

        public class Map
        {
            public Region[,] regions = new Region[1, 1];
            public int length = 0;
            public int width = 0;
            /// <summary>
            /// The size of the map.
            /// </summary>
            public int size()
            {
                return length * width;
            }
            ///<summary>
            ///Create blank map with x regions long and y regions wide.
            ///</summary>
            public Map(int length, int width)
            {
                this.length = length;
                this.width = width;
                regions = new Region[length, width];
                for (int i = 0; i < size(); i++)
                {
                    regions[i % length, Mathf.FloorToInt(i / length)] = new Region(this, i % length, Mathf.FloorToInt(i*1f / length*1f));
                }
            }
            public Map(int length, int width, Region[,] data)
            {
                if (size() != data.Length)
                {
                    Debug.LogError("Size not matching data given! [Engine.Map.Map(int length, int width, Region[] data)]");
                    return;
                }
                this.length = length;
                this.width = width;
                regions = data;
            }
            public Region GetRegion(int x, int y)
            {
                return regions[x, y];
            }
        }
        ///<summary>
        ///Create a map (With x regions long and y regions wide)
        /// </summary>
        public static Map GenerateMap(int length, int width, string seed, int randomFillPercent,int smoothLoops)
        {
            int[,] map = new int[length*10, width*10];
            //Fill the map randomly
            if (seed == "")
            {
                seed = Time.time.ToString();
            }

            System.Random pseudoRandom = new System.Random(seed.GetHashCode());

            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    if (x == 0 || x == length - 1 || y == 0 || y == width - 1)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                    }
                }
            }

            //Map filled randomly. Now to smooth it.

            for (int i = 0; i < smoothLoops; i++)
            {
                for (int y = 0; y < width; y++)
                {
                    for (int x = 0; x < length; x++)
                    {
                        //Check neighbours of tile
                        int wallCount = 0;
                        for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
                        {
                            for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
                            {
                                if (neighbourX >= 0 && neighbourX < length && neighbourY >= 0 && neighbourY < width)
                                {
                                    if (neighbourX != x || neighbourY != y)
                                        wallCount += map[neighbourX, neighbourY];
                                }
                                else
                                {
                                    wallCount++;
                                }
                            }
                        }
                        //Smooth.
                        if (wallCount > 4)
                            map[x, y] = 1;
                        else if (wallCount < 4)
                            map[x, y] = 0;
                    }
                }
            }
            //Done! Now to pass the data to the tiles.
            Map tileMap = new Map(length, width);
            TileDatabase.Entity stoneData = TileDatabase.instance.FindEntity("Stone");
            TileDatabase.Entity coalData = TileDatabase.instance.FindEntity("Coal");
            TileDatabase.Entity backgroundStoneData = TileDatabase.instance.FindEntity("Stone Background");
            foreach (Region reg in tileMap.regions)
            {
                for (int tileY = 0; tileY < 10; tileY++)
                {
                    for (int tileX = 0; tileX < 10; tileX++)
                    {
                        if (map[tileX+reg.regionX*10, tileY+reg.regionY*10] == 1)
                        {
                            reg.SetTile(tileX, tileY, 1, new Tile(reg, tileX, tileY, 1, stoneData));
                        }
                    }
                }
            }
            return tileMap;
        }
    }

    public class Console : MonoBehaviour
    {
        public Font chatFont;
        public GameObject player;
        public string chatLog;
        public List<Command> commandList = new List<Command>();
        public class Command
        {
            public string commandName;
            public string description;
            public string command;
            public Command(string name, string description, string command)
            {
                commandName = name;
                this.description = description;
                this.command = command;
            }
        }
        // Use this for initialization
        void Awake()
        {
            AddCommand("test", "Test Command.", ";");
        }
        // Update is called once per frame
        private void OnGUI()
        {
            GUI.Box(new Rect(0, 0, 550, 575), new GUIContent("chat/command"));
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.font = chatFont;
            labelStyle.fontSize = 10;
            labelStyle.alignment = TextAnchor.UpperLeft;
            GUI.Label(new Rect(0, 0, 550, 575), chatLog, labelStyle);
            string enterText = GUI.TextField(new Rect(0, 550, 540, 15), ">", 90, labelStyle);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                chatLog = chatLog + enterText;
            }
        }
        public void AddCommand(string name, string description, string command)
        {
            commandList.Add(new Command(name, description, command));
        }
    }
}
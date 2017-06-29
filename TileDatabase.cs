using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class TileDatabase : MonoBehaviour
{
    public static TileDatabase instance = null;
    private void Awake()
    {
        instance = this;
    }
    [Serializable]
    public class Entity
    {
        public AIexLibrary.MapUtils.Tile linkedTile;
        public bool collidable;
        public string name;
        public uint id;
        public Sprite sprite;
        public bool usingTileset;
        public List<Sprite> tileset;
        public Action startMethod;
        public Action rightClickMethod;
        public Action leftClickMethod;
        public Entity(string name, uint id, bool collidable, Sprite sprite, Action startMethod, Action rightClickMethod, Action leftClickMethod)
        {
            this.collidable = collidable;
            this.name = name;
            this.id = id;
            this.sprite = sprite;
            this.startMethod = startMethod;
            this.rightClickMethod = rightClickMethod;
            this.leftClickMethod = leftClickMethod;
        }
        public void Init()
        {

        }
    }
    public Entity[] entities;
    public Entity FindEntity(string name)
    {
        foreach(Entity ent in entities)
        {
            if (ent.name == name)
            {
                return ent;
            }
        }
        return null;
    }
}

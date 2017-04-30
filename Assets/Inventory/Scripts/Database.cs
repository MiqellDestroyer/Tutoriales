﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Database")]
public class Database : ScriptableObject {
    public List<Item> items = new List<Item>();

    public Item FindItemInDatabase(int id)
    {
        foreach (Item item in items)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}


[System.Serializable]
public class Item
{
    public int id;
    public string name;
    [TextArea(5,5)]
    public string description;
    public bool isStackable;
    public ItemType itemType;
    public Stats stats;
    public Vector2 scrollPos;
    [System.Serializable]
    public struct Stats
    {
        public int cost;
        public int sellCost;       
        public int damage;
        public int defense;
        public int health;
        public int mana;
    }
    public enum ItemType {CONSUMABLE, WEAPON, CLOTH, QUEST, MISC}

}

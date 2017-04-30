using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemWindow : EditorWindow {
    private static Database database;
    private static EditorWindow window;

    private static Item newItem;
    private GUILayoutOption[] options = { GUILayout.MaxWidth(150.0f), GUILayout.MinWidth(20.0f) };
    public static void ShowEmptyWindow(Database db)
    {
        database = db;
        window = GetWindow<ItemWindow>();
        window.maxSize = new Vector2(300, 360);
        window.minSize = new Vector2(300, 360);
        newItem = new Item();
    }

    public void OnGUI()
    {
        DisplayItem(newItem);
        if (GUILayout.Button("Confirm"))
        {
            AddItem();
        }
        EditorGUI.EndDisabledGroup();
    }
    private bool shouldDisable;
    private void DisplayItem(Item item)
    {
        GUIStyle textAreaStyle = new GUIStyle(GUI.skin.textArea);
        textAreaStyle.wordWrap = true;
        GUIStyle valueStyle = new GUIStyle(GUI.skin.label);
        valueStyle.wordWrap = true;
        valueStyle.alignment = TextAnchor.MiddleLeft;
        valueStyle.fixedWidth = 50;
        valueStyle.margin = new RectOffset(0, 50, 0, 0);

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ID: ");
        item.id = EditorGUILayout.IntField(item.id, options);
        EditorGUILayout.EndHorizontal();

        if (database.FindItemInDatabase(item.id) == null)
            shouldDisable = false;
        else
            shouldDisable = true;

        EditorGUI.BeginDisabledGroup(shouldDisable);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name: ");
        item.name = EditorGUILayout.TextField(item.name, options);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Type: ");
        item.itemType = (Item.ItemType)EditorGUILayout.EnumPopup(item.itemType, options);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Stackable: ");
        item.isStackable = EditorGUILayout.Toggle(item.isStackable, options);
        EditorGUILayout.EndHorizontal();
        //EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Description: ");       
        item.description = EditorGUILayout.TextArea(item.description, textAreaStyle, GUILayout.MinHeight(100));
        GUILayout.Label("Statistics: ");
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Cost: ");
        item.stats.cost = EditorGUILayout.IntField(item.stats.cost, options);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Selling Cost: ");
        item.stats.sellCost = EditorGUILayout.IntField(item.stats.sellCost, options);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage: ");
        item.stats.damage = EditorGUILayout.IntField(item.stats.damage, options);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Defense: ");
        item.stats.defense = EditorGUILayout.IntField(item.stats.defense, options);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Health: ");
        item.stats.health = EditorGUILayout.IntField(item.stats.health, options);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Mana: ");
        item.stats.mana = EditorGUILayout.IntField(item.stats.mana, options);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

    }

    private void AddItem()
    {
        Undo.RecordObject(database, "Item Added");        
        database.items.Add(newItem);
        EditorUtility.SetDirty(database);
        window.Close();
    }


}

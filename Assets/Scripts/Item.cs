using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public int id = 0;
    public string itemName;
    public bool stackable { get; set; }
    public int maxStackSize;
    public Sprite sprite { get; set; }

    // public Item() {
    //     id = 0;
    //     itemName = null;
    //     stackable = false;
    //     maxStackSize = 0;
    //     sprite = null;
    // }

    // public Item(Item newItem) {
    //     id = newItem.GetID();
    //     itemName = newItem.GetName();
    //     stackable = newItem.GetIsStackable();
    //     maxStackSize = newItem.GetMaxStackSize();
    //     sprite = newItem.GetSprite();
    // }

    protected virtual void Awake() {
       // gameObject.tag = "Item";
        sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
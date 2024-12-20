using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int itemId;
    public string itemName;
    public float itemWeight;
    public Sprite itemIcon;
    public ItemType itemType;
    public GameObject prefab;
    public enum ItemType
    {
        Sphere,
        Cylinder,
        Capsule
    }
}

using UnityEngine;


public enum SlotTag { None, Holding, Equipment}

[CreateAssetMenu(menuName = "RPG 2D/Item")]
public class Item1 : ScriptableObject
{
    public Sprite sprite;
    public SlotTag itemTag;
    public GameObject prefab;
    public string itemName;

}

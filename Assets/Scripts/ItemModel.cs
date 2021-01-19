using UnityEngine;


public abstract class ItemModel : ScriptableObject
{
    [SerializeField] private Sprite image = default;
    [SerializeField] private string itemName = default;
    [SerializeField] private float weight = default;
    [SerializeField] private int id = default;

    public Sprite Image => image;
    public string ItemName => itemName;
    public float Weight => weight;
    public int ID => id;

}
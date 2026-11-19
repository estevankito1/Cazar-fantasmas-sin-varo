using UnityEngine;

public enum ItemEffectType // Define the types of item effects
{
    InstantDamage,
    DamageOverTime
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Combat/Item")] // Create a new item data asset in the Unity editor
public class ItemData : ScriptableObject // Define the data for an item
{
    [Header("Informacion del objeto")] // Header for item information in the Unity editor
    public string itemName;
    public string description;

    [Header("Configuracion del efecto")] // Header for effect configuration in the Unity editor
    public ItemEffectType effectType;
    public int damage;
    public int duration;
}

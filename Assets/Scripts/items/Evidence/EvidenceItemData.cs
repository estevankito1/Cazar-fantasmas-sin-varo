
using UnityEngine; // Import the UnityEngine namespace for Unity-specific classes and functions

[CreateAssetMenu(fileName = "NewEvidence", menuName = "Investigation/Evidence")] // Create a new evidence item data asset in the Unity editor
public class EvidenceItemData : ScriptableObject // Define the data for an evidence item
{
    [Header("Informacion del objeto")] // Header for item information in the Unity editor
    public string itemName;
    [TextArea(3, 5)]
    public string description;

    [Header("Presentacion")] // Header for presentation in the Unity editor
    public Sprite evidenceImage;

    [Header("Sistema de investigacion")] // Header for investigation system in the Unity editor
    public bool isKeyObject;
}

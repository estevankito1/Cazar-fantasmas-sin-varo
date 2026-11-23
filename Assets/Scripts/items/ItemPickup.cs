
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Item Information")]
    public EvidenceItemData itemData;

    [Header("Combat")]
    public NewCombatManager combatManager;
    public bool startsCombat;

    public void Interact()
    {
        if (itemData == null)
        {
            Debug.LogWarning("No hay evidencia asignada.");
            return;
        }

        Debug.Log("Evidencia encontrada: " + itemData.name);

        if (startsCombat)
        {
            if (combatManager == null)
            {
                Debug.LogError("No hay NewCombatManager asignado.");
                return;
            }

            combatManager.StartCombat();
        }

        Destroy(gameObject);
    }
}

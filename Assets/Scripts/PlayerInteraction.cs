
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private ItemPickup nearbyItem;

    private void Update()
    {
        if (nearbyItem == null)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            nearbyItem.Interact();
            nearbyItem = null;

            Debug.Log("Interacción completada.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemPickup item = other.GetComponent<ItemPickup>();

        if (item != null)
        {
            nearbyItem = item;
            Debug.Log("Presiona E para recoger: " + item.itemData.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ItemPickup item = other.GetComponent<ItemPickup>();

        if (item != null && nearbyItem == item)
        {
            nearbyItem = null;
            Debug.Log("Te alejaste del objeto.");
        }
    }
}

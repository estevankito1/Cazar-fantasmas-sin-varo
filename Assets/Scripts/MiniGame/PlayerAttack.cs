
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public GameObject attackHitbox;
    public float attackDuration = 0.2f;
    public float attackCooldown = 0.4f;

    private bool canAttack = true;

    void Start()
    {
        attackHitbox.SetActive(false);
    }

    void Update()
    {
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame &&
            canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;

        attackHitbox.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        attackHitbox.SetActive(false);

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}

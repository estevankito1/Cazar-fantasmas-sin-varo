
using UnityEngine;
using UnityEngine.UI;

public class VisibilityController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Camera playerCamera;
    public Image maskImage;

    [Header("Cordura")]
    [Range(0, 10)]
    public int cordura = 10;

    public static int corduraPersistente = 10;

    [Header("Visibility Settings")]
    public float maxRadius = 0.35f;
    public float edgeSoftness = 0.03f;

    private Material runtimeMaterial;

    void Start()
    {
        if (maskImage == null || maskImage.material == null)
        {
            Debug.LogError("Falta asignar Mask Image o su material.");
            return;
        }

        // Recuperar la cordura guardada
        cordura = corduraPersistente;

        runtimeMaterial = new Material(maskImage.material);
        maskImage.material = runtimeMaterial;

        UpdateVisibility();
    }

    void LateUpdate()
    {
        if (player == null || playerCamera == null ||
            runtimeMaterial == null)
            return;

        Vector3 screenPosition =
            playerCamera.WorldToViewportPoint(player.position);

        runtimeMaterial.SetVector(
            "_PlayerScreenPos",
            new Vector4(
                screenPosition.x,
                screenPosition.y,
                0,
                0
            )
        );
    }

    public void ReduceCordura()
    {
        SetCordura(cordura - 2);
    }

    public void SetCordura(int value)
    {
        cordura = Mathf.Clamp(value, 0, 10);
        corduraPersistente = cordura;

        UpdateVisibility();

        Debug.Log("Cordura actual: " + cordura);
    }

    public static void ReducirCorduraGlobal(int cantidad)
    {
        corduraPersistente = Mathf.Clamp(
            corduraPersistente - cantidad,
            0,
            10
        );

        Debug.Log(
            "Cordura después del minijuego: " +
            corduraPersistente
        );
    }

    void UpdateVisibility()
    {
        if (runtimeMaterial == null)
            return;

        float radius = maxRadius * (cordura / 10f);

        runtimeMaterial.SetFloat("_Radius", radius);
        runtimeMaterial.SetFloat("_Softness", edgeSoftness);
    }

    [ContextMenu("Reducir cordura (-2)")]
    void TestCordura()
    {
        ReduceCordura();
    }

    void OnDestroy()
    {
        if (runtimeMaterial != null)
            Destroy(runtimeMaterial);
    }
}

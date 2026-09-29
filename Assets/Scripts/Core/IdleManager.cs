using UnityEngine;

public class IdleManager : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float tiempoInactividad = 30f;

    private GameManager gameManager;

    private float tiempoSinInteraccion;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        if (gameManager == null)
            return;

        if (gameManager.CurrentState != GameManager.GameState.Playing)
        {
            tiempoSinInteraccion = 0f;
            return;
        }

        tiempoSinInteraccion += Time.deltaTime;

        if (tiempoSinInteraccion >= tiempoInactividad)
        {
            ReiniciarPorInactividad();
        }
    }

    public void RegistrarInteraccion()
    {
        tiempoSinInteraccion = 0f;
    }

    private void ReiniciarPorInactividad()
    {
        tiempoSinInteraccion = 0f;

        Debug.Log(
            "30 segundos sin interacción → reiniciando al tutorial."
        );

        gameManager.ReiniciarPorInactividad();
    }
}
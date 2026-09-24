using UnityEngine;

public class FeedManager : MonoBehaviour
{
    private GameManager gameManager;
    private FeedSelector feedSelector;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        feedSelector = FindFirstObjectByType<FeedSelector>();
    }

    private void Start()
    {
        CargarSiguienteShort();
    }

    public void Next()
    {
        CargarSiguienteShort();
    }

    private void CargarSiguienteShort()
    {
        if (feedSelector == null)
        {
            Debug.LogError("No se encontró FeedSelector.");
            return;
        }

        ShortData siguienteShort = feedSelector.ObtenerSiguiente();

        if (siguienteShort == null)
        {
            Debug.Log("No hay más Shorts disponibles.");
            return;
        }

        gameManager.SetCurrentShort(siguienteShort);
    }
}
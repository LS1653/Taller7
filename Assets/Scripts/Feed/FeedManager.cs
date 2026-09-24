using UnityEngine;

public class FeedManager : MonoBehaviour
{
    [SerializeField] private ShortData[] shorts;

    private GameManager gameManager;

    private int currentIndex = 0;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Start()
    {
        if (shorts != null && shorts.Length > 0)
        {
            gameManager.SetCurrentShort(shorts[0]);
        }
    }

    public ShortData GetFirstShort()
    {
        if (shorts != null && shorts.Length > 0)
            return shorts[0];
    
        return null;
    }

    public void Next()
    {
        if (shorts == null || shorts.Length == 0)
            return;
    
        if (currentIndex >= shorts.Length - 1)
        {
            Debug.Log("No hay más Shorts disponibles.");
            return;
        }
    
        currentIndex++;
    
        ShortData nextShort = shorts[currentIndex];
    
        gameManager.SetCurrentShort(nextShort);
    }
}
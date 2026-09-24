using UnityEngine;

public class FeedManager : MonoBehaviour
{
    [SerializeField] private ShortData firstShort;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Start()
    {
        if (firstShort != null)
        {
            gameManager.SetCurrentShort(firstShort);
        }
    }
}
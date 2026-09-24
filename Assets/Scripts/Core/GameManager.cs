using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Tutorial,
        Playing,
        Doom,
        Result
    }

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        SetState(GameState.Tutorial);
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;

        Debug.Log("Estado del juego: " + CurrentState);
    }
}
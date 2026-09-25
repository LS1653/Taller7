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

    public enum ResultType
    {
        Ninguno,
        FeedPerfecto,
        Neutral,
        Critico
    }

    public GameState CurrentState { get; private set; }

    public ResultType CurrentResult { get; private set; }

    public ShortData CurrentShort { get; private set; }

    [Header("Duración de la partida")]
    [SerializeField] private float duracionPartida = 180f;
    
    private float tiempoTranscurrido;

    private void Awake()
    {
        tiempoTranscurrido = 0f;
        CurrentResult = ResultType.Ninguno;
    
        SetState(GameState.Tutorial);
    }

    private void Update()
    {
        if (CurrentState != GameState.Playing)
            return;
    
        tiempoTranscurrido += Time.deltaTime;
    
        if (tiempoTranscurrido >= duracionPartida)
        {
            FinalizarPorTiempo();
        }
    }

    public void SetCurrentShort(ShortData shortData)
    {
        CurrentShort = shortData;

        if (CurrentShort != null)
        {
            Debug.Log("Short actual: " + CurrentShort.name);
        }
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;

        Debug.Log("Estado del juego: " + CurrentState);
    }

    public void IniciarJuego()
    {
        SetState(GameState.Playing);
    }

    private void FinalizarPorTiempo()
    {
        DoomingManager doomingManager =
            FindFirstObjectByType<DoomingManager>();
    
        if (doomingManager == null)
        {
            Debug.LogError("No se encontró DoomingManager.");
            return;
        }
    
        if (doomingManager.ObtenerNivel(doomingManager.Sensacionalismo) == 1 &&
            doomingManager.ObtenerNivel(doomingManager.Falsedad) == 1)
        {
            CurrentResult = ResultType.FeedPerfecto;
        }
        else
        {
            CurrentResult = ResultType.Neutral;
        }
    
        SetState(GameState.Result);
    
        Debug.Log(
            $"Partida terminada por tiempo → Resultado: {CurrentResult}"
        );
    }

    public void FinalizarPorCritico()
    {
        CurrentResult = ResultType.Critico;
    
        SetState(GameState.Doom);
    
        Debug.Log(
            $"Partida terminada por estado crítico → Resultado: {CurrentResult}"
        );
    }
}
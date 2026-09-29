using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject doomPanel;

    [Header("Resultado")]
    [SerializeField] private TMP_Text resultTitle;
    [SerializeField] private TMP_Text resultDescription;

    private void Start()
    {
        OcultarTodo();
    }

    public void MostrarResultado(GameManager.ResultType resultado)
    {
        OcultarTodo();

        if (resultPanel == null)
            return;

        resultPanel.SetActive(true);

        switch (resultado)
        {
            case GameManager.ResultType.FeedPerfecto:

                resultTitle.text = "FEED PERFECTO";
                resultDescription.text =
                    "Mantuviste el feed en un estado equilibrado.";

                break;

            case GameManager.ResultType.Neutral:

                resultTitle.text = "ESTADO NEUTRAL";
                resultDescription.text =
                    "La partida terminó sin alcanzar un estado crítico.";

                break;

            default:

                resultTitle.text = "RESULTADO";
                resultDescription.text =
                    "La partida ha terminado.";

                break;
        }
    }

    public void MostrarDoom()
    {
        OcultarTodo();

        if (doomPanel != null)
        {
            doomPanel.SetActive(true);
        }
    }

    private void OcultarTodo()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);

        if (doomPanel != null)
            doomPanel.SetActive(false);
    }

    public void OcultarResultados()
    {
        OcultarTodo();
    }
}
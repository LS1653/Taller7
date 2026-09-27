using UnityEngine;

public class ShortInteraction : MonoBehaviour
{
    private bool liked;
    private bool reported;

    // Cada tipo de búsqueda se puede aplicar solo una vez
    // por Short.
    private bool busquedaNegativaUsada;
    private bool busquedaNeutralUsada;
    private bool busquedaPositivaUsada;

    private float tiempoInicioShort;

    private const float TIEMPO_SCROLL_IMPULSIVO = 1f;

    private bool isTutorial;

    private ShortData shortData;
    private DoomingManager doomingManager;

    [SerializeField] private GameObject reportConfirmation;

    // Guardamos cuánto efecto produjo realmente el Like.
    // Esto permite revertirlo correctamente incluso
    // si el valor estaba cerca del mínimo.
    private float likeCambioSAplicado;
    private float likeCambioFAplicado;

    public bool Liked => liked;
    public bool Reported => reported;

    public void Initialize(
        ShortData data,
        bool tutorial,
        DoomingManager manager)
    {
        tiempoInicioShort = Time.time;
        shortData = data;
        isTutorial = tutorial;
        doomingManager = manager;

        liked = false;
        reported = false;

        busquedaNegativaUsada = false;
        busquedaNeutralUsada = false;
        busquedaPositivaUsada = false;

        likeCambioSAplicado = 0f;
        likeCambioFAplicado = 0f;

        if (reportConfirmation != null)
        {
            reportConfirmation.SetActive(false);
        }

        Debug.Log(
            $"ShortInteraction inicializado → {shortData.name}, " +
            $"Tutorial: {isTutorial}"
        );
    }

    // =========================================================
    // LIKE
    // =========================================================

    public void Like()
    {
        if (shortData == null || doomingManager == null)
            return;

        // Quitar Like
        if (liked)
        {
            liked = false;

            if (isTutorial)
            {
                Debug.Log("Like quitado en tutorial. No modifica Dooming.");
                return;
            }

            // Revertimos exactamente el efecto que produjo
            // el Like anterior.
            doomingManager.ModificarDooming(
                -likeCambioSAplicado,
                -likeCambioFAplicado
            );

            Debug.Log(
                $"Like quitado → S: {-likeCambioSAplicado}, " +
                $"F: {-likeCambioFAplicado}"
            );

            likeCambioSAplicado = 0f;
            likeCambioFAplicado = 0f;

            return;
        }

        // Dar Like
        liked = true;

        if (isTutorial)
        {
            Debug.Log("Like registrado en tutorial. No modifica Dooming.");
            return;
        }

        int cambioS = ObtenerCambioLikeSensacionalismo();
        int cambioF = ObtenerCambioLikeFalsedad();

        Vector2 cambioReal = doomingManager.ModificarDooming(
            cambioS,
            cambioF
        );

        likeCambioSAplicado = cambioReal.x;
        likeCambioFAplicado = cambioReal.y;

        Debug.Log(
            $"Like aplicado → S: {cambioReal.x}, F: {cambioReal.y}"
        );
    }

    // =========================================================
    // REPORT
    // =========================================================

    public void Report()
    {
        if (reported)
            return;

        Debug.Log("Report solicitado. Esperando confirmación.");

        if (reportConfirmation != null)
        {
            reportConfirmation.SetActive(true);
        }
    }

    public void CancelarReport()
    {
        if (reportConfirmation != null)
        {
            reportConfirmation.SetActive(false);
        }

        Debug.Log("Reporte cancelado.");
    }

    public void ConfirmarReport(bool reporteCorrecto)
    {
        if (reported)
            return;

        reported = true;

        if (reportConfirmation != null)
        {
            reportConfirmation.SetActive(false);
        }

        if (isTutorial)
        {
            Debug.Log(
                "Report confirmado en tutorial. No modifica Dooming."
            );
            return;
        }

        int cambioS = ObtenerCambioReport(reporteCorrecto);

        doomingManager.ModificarSensacionalismo(cambioS);

        Debug.Log(
            $"Report confirmado → S: {cambioS}"
        );
    }

    public void ConfirmarReportCorrecto()
    {
        ConfirmarReport(true);
    }

    // =========================================================
    // SEARCH
    // =========================================================

    public void Search(int indiceOpcion)
    {
        if (shortData == null || doomingManager == null)
            return;

        if (shortData.searchOptions == null)
            return;

        if (indiceOpcion < 0 ||
            indiceOpcion >= shortData.searchOptions.Length)
            return;

        SearchOption opcion = shortData.searchOptions[indiceOpcion];

        // Comprobamos si ESTE TIPO ya fue utilizado.
        if (BusquedaYaUtilizada(opcion.tipo))
        {
            Debug.Log(
                $"Búsqueda {opcion.tipo} ya utilizada en este Short. " +
                "No modifica Dooming."
            );

            return;
        }

        // La marcamos como utilizada antes de aplicar el efecto.
        MarcarBusquedaComoUtilizada(opcion.tipo);

        if (isTutorial)
        {
            Debug.Log(
                $"Búsqueda registrada en tutorial → {opcion.tipo}. " +
                "No modifica Dooming."
            );

            return;
        }

        float cambioF = opcion.ObtenerEfectoFalsedad();

        doomingManager.ModificarFalsedad(cambioF);

        Debug.Log(
            $"Búsqueda → {opcion.tipo}, F: {cambioF}"
        );
    }

    private bool BusquedaYaUtilizada(SearchOption.SearchType tipo)
    {
        switch (tipo)
        {
            case SearchOption.SearchType.Negativa:
                return busquedaNegativaUsada;

            case SearchOption.SearchType.Neutral:
                return busquedaNeutralUsada;

            case SearchOption.SearchType.Positiva:
                return busquedaPositivaUsada;

            default:
                return false;
        }
    }

    private void MarcarBusquedaComoUtilizada(
        SearchOption.SearchType tipo)
    {
        switch (tipo)
        {
            case SearchOption.SearchType.Negativa:
                busquedaNegativaUsada = true;
                break;

            case SearchOption.SearchType.Neutral:
                busquedaNeutralUsada = true;
                break;

            case SearchOption.SearchType.Positiva:
                busquedaPositivaUsada = true;
                break;
        }
    }

    // =========================================================
    // EFECTOS
    // =========================================================

    private int ObtenerCambioLikeSensacionalismo()
    {
        switch (shortData.sensacionalismo)
        {
            case 1: return -2;
            case 2: return 2;
            case 3: return 5;
            default: return 0;
        }
    }

    private int ObtenerCambioLikeFalsedad()
    {
        switch (shortData.falsedad)
        {
            case 1: return -2;
            case 2: return 2;
            case 3: return 3;
            default: return 0;
        }
    }

    private int ObtenerCambioReport(bool reporteCorrecto)
    {
        if (!reporteCorrecto)
            return 3;

        switch (shortData.sensacionalismo)
        {
            case 1: return 0;
            case 2: return -8;
            case 3: return -12;
            default: return 0;
        }
    }

    public void ProcesarScroll()
    {
        if (isTutorial)
        {
            Debug.Log("Scroll del tutorial. No modifica Dooming.");
            return;
        }
    
        float tiempoTranscurrido =
            Time.time - tiempoInicioShort;
    
        if (tiempoTranscurrido < TIEMPO_SCROLL_IMPULSIVO)
        {
            doomingManager.ModificarDooming(4f, 4f);
    
            Debug.Log(
                $"Scroll impulsivo → +4 S, +4 F " +
                $"(tiempo: {tiempoTranscurrido:F2}s)"
            );
        }
        else
        {
            Debug.Log(
                $"Scroll normal → sin penalización " +
                $"(tiempo: {tiempoTranscurrido:F2}s)"
            );
        }
    }
}
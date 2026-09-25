using UnityEngine;

public class ShortInteraction : MonoBehaviour
{
    private bool liked;
    private bool reported;
    private bool searched;

    private bool isTutorial;

    private ShortData shortData;
    private DoomingManager doomingManager;

    public bool Liked => liked;
    public bool Reported => reported;
    public bool Searched => searched;

    public void Initialize(
        ShortData data,
        bool tutorial,
        DoomingManager manager)
    {
        shortData = data;
        isTutorial = tutorial;
        doomingManager = manager;

        liked = false;
        reported = false;
        searched = false;

        Debug.Log(
        $"ShortInteraction inicializado → {shortData.name}, " +
        $"Tutorial: {isTutorial}"
        );
    }

    public void Like()
    {
        if (liked)
            return;

        liked = true;

        if (isTutorial)
        {
            Debug.Log("Like registrado en tutorial. No modifica Dooming.");
            return;
        }

        int cambioS = ObtenerCambioLikeSensacionalismo();
        int cambioF = ObtenerCambioLikeFalsedad();

        doomingManager.ModificarDooming(
            cambioS,
            cambioF
        );

        Debug.Log(
            $"Like aplicado → S: {cambioS}, F: {cambioF}"
        );
    }

    public void Report()
    {
        if (reported)
            return;

        Debug.Log("Report solicitado. Esperando confirmación.");
    }

    public void ConfirmarReport(bool reporteCorrecto)
    {
        if (reported)
            return;

        reported = true;

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

    public void Search(int indiceOpcion)
    {
        if (shortData.searchOptions == null)
            return;
    
        if (indiceOpcion < 0 || indiceOpcion >= shortData.searchOptions.Length)
            return;
    
        SearchOption opcion = shortData.searchOptions[indiceOpcion];
    
        searched = true;
    
        if (isTutorial)
        {
            Debug.Log(
                $"Búsqueda registrada en tutorial → {opcion.tipo}. No modifica Dooming."
            );
            return;
        }
    
        float cambioF = opcion.ObtenerEfectoFalsedad();
    
        doomingManager.ModificarFalsedad(cambioF);
    
        Debug.Log(
            $"Búsqueda → {opcion.tipo}, F: {cambioF}"
        );
    }

    private int ObtenerCambioLikeSensacionalismo()
    {
        switch (shortData.sensacionalismo)
        {
            case 1:
                return -2;

            case 2:
                return 2;

            case 3:
                return 5;

            default:
                return 0;
        }
    }

    private int ObtenerCambioLikeFalsedad()
    {
        switch (shortData.falsedad)
        {
            case 1:
                return -2;

            case 2:
                return 2;

            case 3:
                return 3;

            default:
                return 0;
        }
    }

    private int ObtenerCambioReport(bool reporteCorrecto)
    {
        if (!reporteCorrecto)
            return 3;

        switch (shortData.sensacionalismo)
        {
            case 1:
                return 0;

            case 2:
                return -8;

            case 3:
                return -12;

            default:
                return 0;
        }
    }
}
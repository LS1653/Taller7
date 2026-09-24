using System.Collections.Generic;
using UnityEngine;

public class FeedSelector : MonoBehaviour
{
    [SerializeField] private ShortData[] shorts;

    private List<ShortData> shortsDisponibles = new List<ShortData>();
    private List<ShortData> shortsUtilizados = new List<ShortData>();

    private void Awake()
    {
        ReiniciarBiblioteca();
    }

    private void ReiniciarBiblioteca()
    {
        shortsDisponibles.Clear();
        shortsUtilizados.Clear();

        if (shorts == null)
            return;

        foreach (ShortData shortData in shorts)
        {
            if (shortData != null)
            {
                shortsDisponibles.Add(shortData);
            }
        }
    }

    public ShortData ObtenerSiguiente()
    {
        if (shortsDisponibles.Count == 0)
        {
            Debug.Log("Todos los Shorts fueron utilizados.");
            return null;
        }

        int indice = Random.Range(0, shortsDisponibles.Count);

        ShortData seleccionado = shortsDisponibles[indice];

        shortsDisponibles.RemoveAt(indice);
        shortsUtilizados.Add(seleccionado);

        return seleccionado;
    }

    public bool YaFueUtilizado(ShortData shortData)
    {
        return shortsUtilizados.Contains(shortData);
    }

    public int ObtenerCantidadDisponibles()
    {
        return shortsDisponibles.Count;
    }

    public int ObtenerCantidadUtilizados()
    {
        return shortsUtilizados.Count;
    }
}
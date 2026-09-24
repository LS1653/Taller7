using UnityEngine;

public class DoomingManager : MonoBehaviour
{
    [Header("Valores actuales")]
    [SerializeField] private float sensacionalismo = 1f;
    [SerializeField] private float falsedad = 1f;

    [Header("Progresión automática")]
    [SerializeField] private float aumentoPorSegundo = 0.30f;

    public float Sensacionalismo => sensacionalismo;
    public float Falsedad => falsedad;   

    private void Update()
    {
        sensacionalismo += aumentoPorSegundo * Time.deltaTime;
        falsedad += aumentoPorSegundo * Time.deltaTime;
    }

    public void ModificarSensacionalismo(float cantidad)
    {
        sensacionalismo = Mathf.Max(1f, sensacionalismo + cantidad);
    }
    
    public void ModificarFalsedad(float cantidad)
    {
        falsedad = Mathf.Max(1f, falsedad + cantidad);
    }
    
    public void ModificarDooming(float cambioSensacionalismo, float cambioFalsedad)
    {
        sensacionalismo = Mathf.Max(1f, sensacionalismo + cambioSensacionalismo);
        falsedad = Mathf.Max(1f, falsedad + cambioFalsedad);
    }

    public int ObtenerNivel(float valor)
    {
        if (valor <= 30f)
            return 1;
    
        if (valor <= 60f)
            return 2;
    
        if (valor <= 90f)
            return 3;
    
        return 4;
    }
}
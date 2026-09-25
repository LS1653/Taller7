using UnityEngine;
using UnityEngine.Video;

public class ShortView : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("ShortView necesita un VideoPlayer.");
        }
    }

    public void MostrarShort(ShortData shortData)
    {
        if (shortData == null)
        {
            Debug.LogWarning("ShortView recibió un ShortData vacío.");
            return;
        }

        if (videoPlayer == null)
            return;

        if (shortData.video == null)
        {
            Debug.LogWarning(
                $"El Short {shortData.name} no tiene VideoClip asignado."
            );
            return;
        }

        videoPlayer.Stop();

        videoPlayer.clip = shortData.video;
        videoPlayer.Play();

        Debug.Log(
            $"Reproduciendo video de: {shortData.name}"
        );
    }
}
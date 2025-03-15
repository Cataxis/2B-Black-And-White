using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoteDeAudio
{
    public AudioClip[] audios; // Arreglo de audios para cada lote
}

public class ChangeVoiceGlitch : MonoBehaviour
{
    [Header("Configuración de Audio")]
    public AudioSource audioSource;
    public int audiosPorLote = 2; // Cantidad de audios por lote
    public int cantidadLotes = 2; // Número de lotes

    [Header("Configuración de Reproducción")]
    public float minTimeBetweenSounds = 1f; // Tiempo mínimo entre sonidos
    public float maxTimeBetweenSounds = 5f; // Tiempo máximo entre sonidos

    [Header("Lotes de Audios")]
    public List<LoteDeAudio> lotes = new List<LoteDeAudio>(); // Lista de lotes con audios

    private int loteSeleccionado;

    void OnValidate()
    {
        AjustarLotes();
    }

    void Start()
    {
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        loteSeleccionado = Random.Range(0, cantidadLotes); // Selecciona un lote al inicio
        StartCoroutine(ReproducirSonidosAleatorios());
    }

    void AjustarLotes()
    {
        while (lotes.Count < cantidadLotes)
        {
            lotes.Add(new LoteDeAudio { audios = new AudioClip[audiosPorLote] });
        }

        while (lotes.Count > cantidadLotes)
        {
            lotes.RemoveAt(lotes.Count - 1);
        }

        foreach (var lote in lotes)
        {
            if (lote.audios.Length != audiosPorLote)
            {
                lote.audios = new AudioClip[audiosPorLote];
            }
        }
    }

    IEnumerator ReproducirSonidosAleatorios()
    {
        while (true)
        {
            if (lotes[loteSeleccionado].audios.Length > 0)
            {
                AudioClip clip = lotes[loteSeleccionado].audios[Random.Range(0, lotes[loteSeleccionado].audios.Length)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            float waitTime = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
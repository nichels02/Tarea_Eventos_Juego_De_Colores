using UnityEngine;
using FMODUnity;  // Necesario para acceder a FMOD desde Unity

public class FMODSoundManager : MonoBehaviour
{
    public string eventPath = "event:/Musica";  // Cambia esto por el path de tu evento
    private static FMOD.Studio.EventInstance musicInstance;  // Instancia del evento de música

    private void Awake()
    {
        // Verifica si ya hay una instancia activa
        if (musicInstance.isValid())
        {
            Debug.Log("La música ya se está reproduciendo.");
            Destroy(gameObject);  // No duplicar el manager
            return;
        }

        // Marca este objeto para que persista entre escenas
        DontDestroyOnLoad(gameObject);

        // Crea la instancia del evento y empieza a reproducirla
        musicInstance = RuntimeManager.CreateInstance(eventPath);
        musicInstance.start();
    }

    //private void OnDestroy()
    //{
    //    // Detiene la música cuando el objeto sea destruido
    //    if (musicInstance.isValid())
    //    {
    //        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //        musicInstance.release();
    //    }
    //}
}

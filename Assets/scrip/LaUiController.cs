using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaUiController : MonoBehaviour
{
    [SerializeField] TMP_Text ElTextoTiempo;
    [SerializeField] TMP_Text ElTextoPuntaje;
    [SerializeField] GameObject ElTextoPanelPausa;
    [SerializeField] GameObject ElTextoPanelPerder;
    [SerializeField] GameObject ElTextoPanelGanar;
    [SerializeField] TMP_Text ElTextoPanelPuntaje;
    [SerializeField] Image LaVida;
    [SerializeField] GameObject ElPanel;



    [SerializeField] Handler LaInteraccion;


    [SerializeField] int EltiempoMax = 300;
    int Eltiempo = 300;
    float cronometro;

    private void Awake()
    {
        LaInteraccion.LosEventos.DesactivarEventosUI();
        LaInteraccion.LosEventos.AparecerPanelGanar += AparecerPanelGanar;
        LaInteraccion.LosEventos.AparecerPanelPerder += AparecerPanelPerder;
        LaInteraccion.LosEventos.ActualizarVida += ActualizarVida;
        LaInteraccion.LosEventos.ActualizarPuntaje += ActualizarPuntaje;
    }


    // Start is called before the first frame update
    void Start()
    {
        Eltiempo = EltiempoMax;
        ElTextoTiempo.text = "TIME: " + Eltiempo;
        
    }

    private void Update()
    {
        cronometro = cronometro < 1 ? cronometro + Time.deltaTime : ActualizarTiempo();
    }

    #region Botones

    public void VolverAlMenu()
    {
        Time.timeScale = 1;
        //fijarme numero de escena
        SceneManager.LoadScene(1);
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1;
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        //Gamemanager._instancia.EstaPausado = false;
        SceneManager.LoadScene(escenaActual);
    }

    #endregion


    #region EventosBiblioteca
    public void AparecerPanelPausa()
    {
        ElTextoPanelPausa.SetActive(!ElPanel.activeSelf);
        ElPanel.SetActive(!ElPanel.activeSelf);
        ElTextoPanelPuntaje.text = "Puntaje: " + LaInteraccion.LosEventos.ActivarPuntaje();
    }

    public void AparecerPanelPerder()
    {
        ElTextoPanelPuntaje.text = "Puntaje: " + LaInteraccion.LosEventos.ActivarPuntaje();
        ElTextoPanelPerder.SetActive(true);
        ElPanel.SetActive(true);
    }

    public void AparecerPanelGanar()
    {
        ElTextoPanelPuntaje.text = "Puntaje: " + LaInteraccion.LosEventos.ActivarPuntaje();
        ElTextoPanelGanar.SetActive(true);
        ElPanel.SetActive(true);
    }

    public void ActualizarVida()
    {
        LaVida.fillAmount = LaInteraccion.LosEventos.ActivarVidaJugador() / LaInteraccion.LosEventos.ActivarVidaMaxJugador();
    }

    public void ActualizarPuntaje()
    {
        print("Puntaje: " + LaInteraccion.LosEventos.ActivarPuntaje());
        ElTextoPuntaje.text = "Puntaje: " + LaInteraccion.LosEventos.ActivarPuntaje();
    }

    public float ActualizarTiempo()
    {
        Eltiempo--;
        ElTextoTiempo.text = "TIME: " + Eltiempo;
        return 0;
    }
    #endregion
}


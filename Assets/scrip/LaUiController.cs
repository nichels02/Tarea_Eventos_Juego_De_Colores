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


    [SerializeField] int EltiempoMax = 300;
    int Eltiempo = 300;

    private void Awake()
    {
        Gamemanager._instancia.AgregarFuncion(ActualizarTiempo, 0);
        Gamemanager._instancia.AgregarFuncion(AparecerPanelPausa, 1);
        Gamemanager._instancia.AgregarFuncion(AparecerPanelPerder, 2);
        Gamemanager._instancia.AgregarFuncion(AparecerPanelGanar, 3);
        Gamemanager._instancia.AgregarFuncion(ActualizarVida, 4);
        Gamemanager._instancia.AgregarFuncion(ActualizarPuntaje, 5);
    }


    // Start is called before the first frame update
    void Start()
    {
        Eltiempo = EltiempoMax;
        ElTextoTiempo.text = "TIME: " + Eltiempo;
        
    }

    #region Botones
    
    public void VolverAlMenu()
    {
        //fijarme numero de escena
        SceneManager.LoadScene(1);
    }

    public void ReiniciarNivel()
    {
        //Gamemanager._instancia.EstaPausado = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion


    #region EventosBiblioteca
    public float AparecerPanelPausa()
    {
        ElTextoPanelPausa.SetActive(!ElPanel.activeSelf);
        ElPanel.SetActive(!ElPanel.activeSelf);
        ElTextoPanelPuntaje.text = "Puntaje: " + Gamemanager._instancia.Biblioteca[8].Invoke();
        return 0;
    }

    public float AparecerPanelPerder()
    {
        ElTextoPanelPuntaje.text = "Puntaje: " + Gamemanager._instancia.Biblioteca[8].Invoke();
        ElTextoPanelPerder.SetActive(true);
        ElPanel.SetActive(true);
        return 0;
    }

    public float AparecerPanelGanar()
    {
        Gamemanager._instancia.EstaPausado = true;
        ElTextoPanelPuntaje.text = "Puntaje: " + Gamemanager._instancia.Biblioteca[8].Invoke();
        ElTextoPanelGanar.SetActive(true);
        ElPanel.SetActive(true);
        return 0;
    }

    public float ActualizarVida()
    {
        LaVida.fillAmount = Gamemanager._instancia.Biblioteca[6].Invoke() / Gamemanager._instancia.Biblioteca[7].Invoke();
        return 0;
    }

    public float ActualizarPuntaje()
    {
        print("Puntaje: " + Gamemanager._instancia.Biblioteca[8].Invoke());
        ElTextoPuntaje.text = "Puntaje: " + Gamemanager._instancia.Biblioteca[8].Invoke();
        return 0f;
    }

    public float ActualizarTiempo()
    {
        Eltiempo--;
        ElTextoTiempo.text = "TIME: " + Eltiempo;
        return 0;
    }
    #endregion
}


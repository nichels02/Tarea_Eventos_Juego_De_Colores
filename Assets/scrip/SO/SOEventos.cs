using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Define la clase del ScriptableObject
[CreateAssetMenu(fileName = "NuevoData", menuName = "SO/SOEventos", order = 1)]
public class SOEventos : ScriptableObject
{
    #region EventosDeJugador
    public event Func<float> VidaJugador; 
    public event Func<float> VidaMaxJugador; 
    public event Func<float> Puntaje;
    #endregion

    #region EventosUI
    public event Action AparecerPanelPerder;
    public event Action AparecerPanelGanar;
    public event Action ActualizarVida;
    public event Action ActualizarPuntaje;
    #endregion

    #region ActivarEventosDeJugador
    public float ActivarVidaJugador()
    {
        float x = VidaJugador.Invoke();
        return x;
    }

    public float ActivarVidaMaxJugador()
    {
        float x = VidaMaxJugador.Invoke();
        return x;
    }
    public float ActivarPuntaje()
    {
        float x = Puntaje.Invoke();
        return x;
    }
    #endregion

    #region ActivarEventosUI
    public void ActivarAparecerPanelPerder()
    {
        AparecerPanelPerder.Invoke();
    }
    public void ActivarAparecerPanelGanar()
    {
        AparecerPanelGanar.Invoke();
    }
    public void ActivarActualizarVida()
    {
        ActualizarVida.Invoke();
    }
    public void ActivarActualizarPuntaje()
    {
        ActualizarPuntaje.Invoke();
    }
    #endregion


    #region desactivarEventos
    public void DesactivarEventosJugador()
    {
        VidaJugador = null;
        VidaMaxJugador = null;
        Puntaje = null;
    }

    public void DesactivarEventosUI()
    {
        AparecerPanelGanar = null;
        AparecerPanelPerder = null;
        ActualizarPuntaje = null;
        ActualizarVida = null;
    }
    #endregion

}
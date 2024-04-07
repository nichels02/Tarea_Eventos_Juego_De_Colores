using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void EntrarAlJuego()
    {
        SceneManager.LoadScene(0);
    }
    public void salir()
    {
        Application.Quit();
    }
}

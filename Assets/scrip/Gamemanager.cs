using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gamemanager : MonoBehaviour
{
    //public static Gamemanager _instancia { get; private set; }

    public float eltiempo;
    //public bool EstaPausado;
    public bool Final;

    public List<Func<float>> Biblioteca { get; private set; } = new List<Func<float>>();

    /*
    private void Awake()
    {
        // Si ya existe una instancia diferente de esta clase, destruimos este objeto
        if (_instancia != null && _instancia != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Si no hay una instancia existente, establecemos esta instancia como la única
            _instancia = this;
        }
    }
    */



    // Update is called once per frame
    void Update()
    {
        //eltiempo = eltiempo < 1 ? eltiempo + Time.deltaTime : Biblioteca[0].Invoke();
    }

    public void PausarElJuego(InputAction.CallbackContext Value)
    {
        if (Value.started && Final == false)
        {
            //EstaPausado = EstaPausado ? false : true;
            Biblioteca[1].Invoke();
        }
    }

    public void AgregarFuncion(Func<float> laFuncion, int posicionPuesta)
    {
        while (Biblioteca.Count-1 <= posicionPuesta)
        {
            Biblioteca.Add(null); // Añade elementos nulos para llenar la lista
        }

        // Inserta la función en la posición especificada
        if (Biblioteca[posicionPuesta]==null)
        {
            print(posicionPuesta);
            Biblioteca.Insert(posicionPuesta, laFuncion);
            //print("la posicion " + posicionPuesta + "fue ocupada por "+laFuncion.Method);
        }
        else
        {
            print("la posicion " + posicionPuesta + " esta ocupada");
        }
        
    }


    public void nombrarFuncionesDeLaBiblioteca()
    {
        for(int i = 0; i < Biblioteca.Count; i++)
        {
            if (Biblioteca[i] != null)
            {
                print("la posicion " + i + " fue ocupada por " + Biblioteca[i].Method);
            }
            else
            {
                print("la posicion " + i + " fue ocupada por una funcion Null");
            }
        }
    }




}


// lista de funciones de la biblioteca:
// 
// funcion 0: actualizar el tiempo de la ui
// funcion 1: aparecer el panel de pausa
// funcion 2: aparecer el panel de Perder
// funcion 3: aparecer el panel de Ganar
// funcion 4: Actualizar la vida de la ui
// funcion 5: Funcion para tener la vida publica para todos
// funcion 6: Funcion para tener la vida maxima publica para todos
// funcion 7: 
// funcion 8: 



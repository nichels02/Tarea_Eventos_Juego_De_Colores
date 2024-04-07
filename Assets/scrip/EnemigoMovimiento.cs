using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMovimiento : Enemigo
{
    [SerializeField] Vector2[] listaDePosiciones = new Vector2[2];
    int index = 0;
    [SerializeField] float distancia;
    [SerializeField] float velocity;
    // Start is called before the first frame update
    void Start()
    {
        cambiarColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Gamemanager._instancia.EstaPausado)
        {
            if (Vector2.Distance(transform.position, listaDePosiciones[index]) > distancia)
            {
                Vector2 direccion = listaDePosiciones[index] - new Vector2(transform.position.x, transform.position.y);
                transform.position = new Vector2(transform.position.x, transform.position.y) + direccion.normalized * velocity * Time.deltaTime;
            }
            else
            {
                index = index < listaDePosiciones.Length - 1 ? index + 1 : 0;
            }
        }
    }
}

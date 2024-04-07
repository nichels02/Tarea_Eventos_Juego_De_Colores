using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pisos : MonoBehaviour
{
    [SerializeField] Transform ElJugador;
    [SerializeField] float distancia;
    Collider2D LaColicion;

    private void Start()
    {
        LaColicion = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (ElJugador.position.y > transform.position.y + distancia)
        {
            LaColicion.enabled = true;
        }
        else
        {
            LaColicion.enabled = false;
        }
    }
}

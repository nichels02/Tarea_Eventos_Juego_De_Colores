using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public ElColorRVA ElColor;
    SpriteRenderer MySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        cambiarColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void cambiarColor()
    {
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        MySpriteRenderer.color = ElColor == ElColorRVA.Rojo ? Color.red : ElColor == ElColorRVA.Azul ? Color.blue : Color.green;
    }
}

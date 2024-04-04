using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ElColorRVA
{
    Rojo,
    Verde,
    Azul
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] ElColorRVA ElColor;
    Rigidbody2D MyRigidbody2D;
    SpriteRenderer MySpriteRenderer;
    float direccion;
    [SerializeField] Vector2 tamaño;
    [SerializeField] Vector2 PosicionBox;
    [SerializeField] bool TocaAlgo;
    [SerializeField] float FuerzaDeSalto;
    [SerializeField] float velocidad;
    [SerializeField] float DistanceRaycast;
    [SerializeField] int numeroDeSaltos;
    [SerializeField] LayerMask Layers;
    [SerializeField] LayerMask LayersEnemigos;

    Collider2D Colicion;
    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        MySpriteRenderer.color = ElColor == ElColorRVA.Rojo ? Color.red : ElColor == ElColorRVA.Azul ? Color.blue : Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(direccion * velocidad * Time.deltaTime, 0, 0);
        Debug.DrawRay(transform.position, Vector2.down * DistanceRaycast,Color.red);
        if (Physics2D.Raycast(transform.position, Vector2.down, DistanceRaycast, Layers))
        {
            numeroDeSaltos = 2;
        }
        if (Physics2D.BoxCast(new Vector2(transform.position.x + PosicionBox.x, transform.position.y + PosicionBox.y), tamaño, LayersEnemigos)) ;
        
    }


    public void Movimiento(InputAction.CallbackContext Value)
    {
        direccion = Value.ReadValue<float>();
    }

    public void salto(InputAction.CallbackContext Value)
    {
        if (Value.started)
        {
            if (numeroDeSaltos > 0)
            {
                print("salto");
                MyRigidbody2D.AddForce(Vector2.up * FuerzaDeSalto, ForceMode2D.Impulse);
                numeroDeSaltos--;
            }
        }
        
    }
    void cambiarDeColor()
    {

    }

    public void cambiarDeColorRojo(InputAction.CallbackContext Value)
    {
        ElColor = ElColorRVA.Rojo;
        MySpriteRenderer.color = Color.red;
        cambiarDeColor();
    }
    public void cambiarDeColorVerde(InputAction.CallbackContext Value)
    {
        ElColor = ElColorRVA.Verde;
        MySpriteRenderer.color = Color.green;
        cambiarDeColor();
    }
    public void cambiarDeColorAzul(InputAction.CallbackContext Value)
    {
        ElColor = ElColorRVA.Azul;
        MySpriteRenderer.color = Color.blue;
        cambiarDeColor();
    }
}

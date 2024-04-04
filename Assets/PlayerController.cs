using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D MyRigidbody2D;
    float direccion;
    [SerializeField] float ElColor;
    [SerializeField] float FuerzaDeSalto;
    [SerializeField] float velocidad;
    [SerializeField] float DistanceRaycast;
    [SerializeField] int numeroDeSaltos;
    [SerializeField] LayerMask Layers;

    Collider2D Colicion;
    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
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
        print(ElColor);
        /*
        if (ElColor != 0)
        {
            print("cambio");
        }
        else
        {
            print("sigue");
        }
        */
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
    public void cambiarDeColor(InputAction.CallbackContext Value)
    {
        if (Value.started)
            ElColor = Value.ReadValue<float>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}

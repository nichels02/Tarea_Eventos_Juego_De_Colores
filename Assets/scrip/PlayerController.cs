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
    [SerializeField] float FuerzaDeSalto;
    [SerializeField] float velocidad;


    [SerializeField] float DistanceRaycast;
    [SerializeField] int numeroDeSaltos;
    [SerializeField] int numeroDeSaltosMax;
    [SerializeField] LayerMask Layers;


    [SerializeField] float time=100;
    [SerializeField] float timeMax;
    [SerializeField] bool TocaAlgo;
    [SerializeField] LayerMask LayersEnemigos;
    [SerializeField] Vector2 size = new Vector2(1, 1);
    [SerializeField] float distance = 5f;
    [SerializeField] Color color;
    bool IdaSubida;



    [SerializeField] int Vida = 10;
    [SerializeField] int VidaMax = 10;
    [SerializeField] int puntaje = 0;


    public float PublicVida()
    {
        return (float)Vida;
    }
    public float PublicVidaMax()
    {
        return (float)VidaMax;
    }
    public float PublicPuntaje()
    {
        return (float)puntaje;
    }

    private void Awake()
    {
        Gamemanager._instancia.AgregarFuncion(PublicVida, 6);
        Gamemanager._instancia.AgregarFuncion(PublicVidaMax, 7);
        Gamemanager._instancia.AgregarFuncion(PublicPuntaje, 8);
    }

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
        time += Time.deltaTime;
        #region movimiento
        if (Gamemanager._instancia.EstaPausado == false)
        {
            transform.position += new Vector3(direccion * velocidad * Time.deltaTime, 0, 0);
        }
        
        #endregion

        #region raycastSalto
        Debug.DrawRay(transform.position, Vector2.down * DistanceRaycast,Color.red);
        if (Physics2D.Raycast(transform.position, Vector2.down, DistanceRaycast, Layers) && MyRigidbody2D.velocity.y<0)
        {
            numeroDeSaltos = numeroDeSaltosMax;
        }
        #endregion

        #region boxCast 
        //Physics2D.BoxCast(el centro del boxscat,   tamaño del boxcast,    rotacion, direccion, mover el punto donde se creo, layer );
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, size, 0f, Vector2.right, distance, LayersEnemigos);

        // Dibujar la BoxCast
        DebugDrawBoxCast(transform.position, size, 0f, Vector2.right, distance, color);

        if(hit.collider != null)
        {
            if (hit.collider.GetComponent<Enemigo>().ElColor != ElColor && time > timeMax)
            {
                Vida--;
                time = 0;
                Gamemanager._instancia.Biblioteca[4].Invoke();
                if (Vida == 0)
                {
                    MyRigidbody2D.gravityScale = 0;
                    Collider2D x = GetComponent<Collider2D>();
                    x.enabled = false;
                    Gamemanager._instancia.EstaPausado = true;
                    Gamemanager._instancia.Final = true;
                    Gamemanager._instancia.Biblioteca[2].Invoke();
                }
            }
            TocaAlgo = true;
        }

        if (time < timeMax)
        {

            // Calcula el valor oscilante usando una función seno
            float alpha = Mathf.Sin(Time.time * 10) * 1 + 0.5f; // Ajusta el offset para que esté entre 0 y 1

            // Aplica el valor al componente de color alpha del material del SpriteRenderer
            Color color = MySpriteRenderer.color;
            color.a = alpha;
            MySpriteRenderer.color = color;
        }
        else
        {
            Color color = MySpriteRenderer.color;
            color.a = 1;
            MySpriteRenderer.color = color;
            TocaAlgo = false;
        }
        TocaAlgo = hit.collider != null ? true : false;
        #endregion
    }


    public void Movimiento(InputAction.CallbackContext Value)
    {
        if (!Gamemanager._instancia.EstaPausado)
        {
            direccion = Value.ReadValue<float>();
        }
    }

    public void salto(InputAction.CallbackContext Value)
    {
        if (Value.started && !Gamemanager._instancia.EstaPausado)
        {
            if (numeroDeSaltos > 0)
            {
                print("salto");
                MyRigidbody2D.AddForce(Vector2.up * FuerzaDeSalto, ForceMode2D.Impulse);
                numeroDeSaltos--;
            }
        }
        
    }

    public void cambiarDeColorRojo(InputAction.CallbackContext Value)
    {
        if (!TocaAlgo && !Gamemanager._instancia.EstaPausado)
        {
            ElColor = ElColorRVA.Rojo;
            MySpriteRenderer.color = Color.red;
        }
    }
    public void cambiarDeColorVerde(InputAction.CallbackContext Value)
    {
        if (!TocaAlgo && !Gamemanager._instancia.EstaPausado)
        {
            ElColor = ElColorRVA.Verde;
            MySpriteRenderer.color = Color.green;
        }
    }
    public void cambiarDeColorAzul(InputAction.CallbackContext Value)
    {
        if (!TocaAlgo && !Gamemanager._instancia.EstaPausado)
        {
            ElColor = ElColorRVA.Azul;
            MySpriteRenderer.color = Color.blue;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Moneda":
                {
                    puntaje += 10;
                    
                    Gamemanager._instancia.Biblioteca[5].Invoke();
                    Destroy(collision.gameObject);
                }
                break;
            case "Corazon":
                {
                    Vida = Vida < VidaMax ? Vida + 1 : Vida;
                    Gamemanager._instancia.Biblioteca[4].Invoke();
                    Destroy(collision.gameObject);
                }
                break;
            case "Final":
                {
                    direccion = 0;
                    Gamemanager._instancia.Final = true;
                    Gamemanager._instancia.Biblioteca[3].Invoke();
                    Destroy(collision.gameObject);
                }
                break;
        }
    }




    void DebugDrawBoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, Color color)
    {
        Vector2 center = origin + (direction.normalized * distance * 0.5f); // Calcular el centro del BoxCast
        float halfDistance = distance * 0.5f; // Calcular la mitad de la distancia

        // Calcular las esquinas del rectángulo del BoxCast
        Vector2 topLeft = center + (Vector2)(Quaternion.Euler(0, 0, angle) * new Vector2(-size.x * 0.5f, size.y * 0.5f));
        Vector2 topRight = center + (Vector2)(Quaternion.Euler(0, 0, angle) * new Vector2(size.x * 0.5f, size.y * 0.5f));
        Vector2 bottomLeft = center + (Vector2)(Quaternion.Euler(0, 0, angle) * new Vector2(-size.x * 0.5f, -size.y * 0.5f));
        Vector2 bottomRight = center + (Vector2)(Quaternion.Euler(0, 0, angle) * new Vector2(size.x * 0.5f, -size.y * 0.5f));

        // Dibujar las líneas del rectángulo
        Debug.DrawLine(topLeft, topRight, color);
        Debug.DrawLine(topRight, bottomRight, color);
        Debug.DrawLine(bottomRight, bottomLeft, color);
        Debug.DrawLine(bottomLeft, topLeft, color);

        // Dibujar las diagonales del rectángulo
        Debug.DrawLine(topLeft, bottomRight, color);
        Debug.DrawLine(topRight, bottomLeft, color);
    }




}

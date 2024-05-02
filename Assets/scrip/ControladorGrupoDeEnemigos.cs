using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[System.Serializable]
public class ListEnemigos
{
    public Enemigo[] ElGrupo;
    public ListEnemigos()
    {

    }
}

public class ControladorGrupoDeEnemigos : MonoBehaviour
{
    [SerializeField] float tiempo;
    [SerializeField] float tiempoMax;
    [SerializeField] List<ListEnemigos> ListaDeEnemigos = new List<ListEnemigos>();
    ElColorRVA elColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempo > tiempoMax)
        {
            tiempo = 0;
            for(int i = 0; i < ListaDeEnemigos.Count; i++)
            {
                elColor = cambiarDeColor(ListaDeEnemigos[i].ElGrupo[0].ElColor);
                for(int j = 0; j < ListaDeEnemigos[i].ElGrupo.Length; j++)
                {
                    ListaDeEnemigos[i].ElGrupo[j].ElColor = elColor;
                    ListaDeEnemigos[i].ElGrupo[j].cambiarColor();
                }
            }
        }
        else
        {
            tiempo += Time.deltaTime;
        }
    }




    ElColorRVA cambiarDeColor(ElColorRVA color)
    {
        switch (color)
        {
            case ElColorRVA.Azul:
                {
                    color = ElColorRVA.Verde;
                }
                break;
            case ElColorRVA.Verde:
                {
                    color = ElColorRVA.Rojo;
                }
                break;
            case ElColorRVA.Rojo:
                {
                    color = ElColorRVA.Azul;
                }
                break;
        }
        return color;
    }
}

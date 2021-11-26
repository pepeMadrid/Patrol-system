using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverPorPuntos : MonoBehaviour {
    private Vector3[] posiciones;
    private int cuboMover;

    public bool loopMode; //usado para que el movimiento sea un ciclo, NPCs, enemigos simples. sino es significa que se dirige al objetivo a marcar en otros scripts.
    // Use this for initialization
    void Start()
    {
        posiciones = new Vector3[transform.Find("PuntosMover").childCount];
        cuboMover = 0;
        for (int x = 0; x < posiciones.Length; x++)
        {
            posiciones[x] = transform.Find("PuntosMover").GetChild(x).position;
            print("numero" + x);
        }

        Destroy(transform.Find("PuntosMover").gameObject);//ya no necesitamos los cubos como referencia
        
        if(loopMode)
            StartCoroutine(mover());
    }

    public Vector3 getPosition(int posicion)
    {

        return posiciones[posicion];
    }

    public IEnumerator mover()
    {

        yield return new WaitForSeconds(2.0f);
        GetComponent<NavMeshAgent>().SetDestination(posiciones[cuboMover]);
        //transform.LookAt(posiciones[cuboMover]);
            
        comprobarDistancia();
        
        StartCoroutine(mover());

    }
    private void comprobarDistancia()
    {
        float distance = Vector3.Distance(transform.position, posiciones[cuboMover]);
        if (distance < 15.0f)
        {
            cuboMover++;
            if (cuboMover > posiciones.Length - 1)
            {
                    cuboMover = 0;
            }
        }

    }

}

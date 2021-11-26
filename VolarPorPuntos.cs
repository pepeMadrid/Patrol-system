using System.Collections;
using UnityEngine;


public class VolarPorPuntos : MonoBehaviour {
    private Vector3[] posiciones;
    public int objetivo;

    public float velocidad;
    //public bool lookAtObjetivo;
    public GameObject observar;
    public bool loopMode; //usado para que el movimiento sea un ciclo, NPCs, enemigos simples. sino es significa que se dirige al objetivo a marcar en otros scripts.
    // Use this for initialization
    // Use this for initialization
    void Awake()
    {
        posiciones = new Vector3[transform.Find("PuntosMover").childCount];
        objetivo = 0;
        for (int x = 0; x < posiciones.Length; x++)
        {
            posiciones[x] = transform.Find("PuntosMover").GetChild(x).position;
        }

        Destroy(transform.Find("PuntosMover").gameObject);
        if(loopMode)
            StartCoroutine(comprobarDistancia()); 
    }

    private Vector3 dir;
    private Quaternion rot;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, posiciones[objetivo], velocidad * Time.deltaTime);
        //if (lookAtObjetivo)
        {
            dir = posiciones[objetivo] - transform.position;
           //dir.y = 0; // keep the direction strictly horizontal
            rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 0.1f * Time.deltaTime);

        }
        if(observar)
            transform.LookAt(observar.transform);
    }


    private IEnumerator comprobarDistancia()
    {
        float distance = Vector3.Distance(transform.position, posiciones[objetivo]);
        if (distance < 2.0f)
        {
            objetivo++;
            if (objetivo > posiciones.Length - 1)
            {
                 objetivo = 0;
            }
        }
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(comprobarDistancia());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    public float speed;
    private Vector3 posicion;
    private Cronometro crono;
    private Vector3 posicionInicial;


    void Start()
    {
        rb = GetComponent<Rigidbody> ();//Solicita a Unity que devuelva el componente especifico de un objeto, en este caso el componenete Rigidbody
        crono = FindObjectOfType<Cronometro>();//Obtener el componente Cronometro de manera automatica, FindObjectOfType busca en toda la escena coincidencia con <>
        posicionInicial = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 7)
        {
            reiniciarJugador();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movimiento = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movimiento * speed);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meta"))
        {
            crono.detenerTiempo();
        }else if (other.CompareTag("Reinicio"))
        {
            crono.reiniciarCrono();
        }
    }

    void reiniciarJugador()
    {
        rb.velocity = Vector3.zero; //rb.velocity modifica unicamente la velocidad, Vector3.zero vector con componentes(0.0.0)
        transform.position = posicionInicial;
        crono.reiniciarCrono();
    }//Fin REINICIAR_JUGADOR
    
}

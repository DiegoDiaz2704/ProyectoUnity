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
    public Transform particulasBuenas;
    public Transform particulasMalas;
    private ParticleSystem sistemaParticulasBuenas;
    private ParticleSystem sistemaParticulasMalas;
    private AudioSource audioRecollection;
    private List<GameObject> objetosRecolectados = new List<GameObject>();//Lista para guardar los recolectables cuando se recogen
    private int puntaje = 0;//Puntos del jugador


    void Start()
    {
        rb = GetComponent<Rigidbody> ();//Solicita a Unity que devuelva el componente especifico de un objeto, en este caso el componenete Rigidbody
        crono = FindObjectOfType<Cronometro>();//Obtener el componente Cronometro de manera automatica, FindObjectOfType busca en toda la escena coincidencia con <>
        posicionInicial = transform.position;
        sistemaParticulasBuenas = particulasBuenas.GetComponent<ParticleSystem>();
        sistemaParticulasMalas = particulasMalas.GetComponent<ParticleSystem>();
        sistemaParticulasBuenas.Stop();
        sistemaParticulasMalas.Stop();
        audioRecollection = GetComponent<AudioSource>();

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
            Debug.Log("NIVEL COMPLETADO!. Puntaje Final: " + puntaje);
        }else if (other.CompareTag("Reinicio"))
        {
            crono.reiniciarCrono();
        }
        
        // Objetos recolectables (buenos y malos)
        if (other.CompareTag("RecolectableBueno") || other.CompareTag("RecolectableMalo"))
        {
            posicion = other.transform.position;
            bool bueno = other.CompareTag("RecolectableBueno");
            
            // Elegir partículas
            if(bueno) {
                puntaje += 100; // Suma 100 puntos al jugador
                Debug.Log("¡Bien! 100 puntos más. Total: " + puntaje);
                crono.ActualizarPuntos(puntaje);
                particulasBuenas.position = posicion;
                sistemaParticulasBuenas.Play();
            } else {
                puntaje -= 200; // Resta 200 puntos al jugador
                Debug.LogWarning("¡Cuidado! 200 puntos menos. Total: " + puntaje);
                crono.ActualizarPuntos(puntaje);
                particulasMalas.position = posicion;
                sistemaParticulasMalas.Play();
            }

            audioRecollection.Play();//reproduce el sonido al recoger el recolectable
            other.gameObject.SetActive(false);//Este método oculta el objeto pero sigue presente en la memoria
            objetosRecolectados.Add(other.gameObject);//añade los recolectables a la Lista para guardarlos y volverlos a generar si se reinicia el jugador
        }
        
        else{

            //el objeto no es recolectable
        }
    }

    void reiniciarJugador()
    {
        rb.velocity = Vector3.zero; //rb.velocity modifica unicamente la velocidad, Vector3.zero vector con componentes(0.0.0)
        transform.position = posicionInicial;
        crono.reiniciarCrono();
        puntaje = 0; // Se resetean los puntos del jugador
        Debug.Log("Puntaje reseteado a 0.");
        foreach (GameObject objeto in objetosRecolectados) //reestablecer los recolectables al reiniciar al jugador
        {
            if (objeto != null) 
            {
                objeto.SetActive(true);
            }
        }
        objetosRecolectados.Clear();
    }//Fin REINICIAR_JUGADOR
    
}

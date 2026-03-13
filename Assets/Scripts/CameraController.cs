using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;  //transform.position obtiene la posición del objeto
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate (){
        transform.position = player.transform.position + offset; //Se ejecuta por cada frame del juego, este método sirve para hacer que la cámara siga al jugador y siempre mantenga la misma distancia con respecto al jugador 
    }
}

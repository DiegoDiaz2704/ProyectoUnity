using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textoCrono; //Identificar texto dentro del componente
    [SerializeField] private float tiempo;//Tiempo en segundos
    [SerializeField] TextMeshProUGUI felicitacion;
    private int minutos, segundos, decimasSegundo;
    private bool circuitoFinalizado = false;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!circuitoFinalizado)
        {
            cronometro();
        }
    }

    void cronometro()
    {
        tiempo += Time.deltaTime;

        minutos = Mathf.FloorToInt(tiempo / 60);
        segundos = Mathf.FloorToInt(tiempo % 60);
        decimasSegundo = Mathf.FloorToInt((tiempo % 1) * 100);

        //Mathf.FloorToInt se usa para mostrar el entero

        textoCrono.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, decimasSegundo);
    }//Fin CRONOMETRO

    public void detenerTiempo()
    {

        circuitoFinalizado = true;
        felicitacion.text = "¡Felicidades!\nTu tiempo fue: " + textoCrono.text;
        felicitacion.gameObject.SetActive(true);

    } //Fin DETENER_TIEMPO

    public void reiniciarCrono()
    {
        textoCrono.text = string.Format("{0:00}:{1:00}:{2:00}", 0, 0, 0);
        tiempo = 0;
        circuitoFinalizado = false;
        felicitacion.gameObject.SetActive(false);

    }//Fin REINICIAR_CRONO

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private bool stateMute;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Music"))
        {
            stateMute = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicController>().stateMute;
        }
        
        if (GameObject.FindGameObjectWithTag("BotonVolumen"))
        {
            GameObject.FindGameObjectWithTag("BotonVolumen").GetComponent<Animator>().SetBool("mute", stateMute);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Jugar()
    {
        SceneManager.LoadScene("RecognitionScene");
    }
    public void Coleccion()
    {
        SceneManager.LoadScene("ColeccionScene");
    }
    public void Instrucciones()
    {
        SceneManager.LoadScene("InstruccionesScene");
    }
    public void Soluciones()
    {
        SceneManager.LoadScene("SolucionesScene");
    }
    public void Salir()
    {
          #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
          #else
            SalirDeLaAplicacion();
          #endif
    }
    public void VolverMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    private void SalirDeLaAplicacion()
    {
        // En una compilación, usar Application.Quit() para cerrar la aplicación

        Application.Quit();
    }
    public void BotonVolumen()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicController>().MuteVolumen();
       
    }
}

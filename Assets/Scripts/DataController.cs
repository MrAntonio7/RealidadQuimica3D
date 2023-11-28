using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataController : MonoBehaviour
{

    private static DataController instance;
    public List<string> arrayEncontrados;

    // Start is called before the first frame update

    private void Awake()
    {
        // Verifica si ya existe una instancia
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            arrayEncontrados = new List<string>();

            string datosLeidos = PlayerPrefs.GetString("miListaKey", "");
            if (!string.IsNullOrEmpty(datosLeidos))
            {
                // Deserializar los datos y cargar la lista
                arrayEncontrados = new List<string>(datosLeidos.Split(','));
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

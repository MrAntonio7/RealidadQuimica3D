using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;

public class CollectionController : MonoBehaviour
{
    //public GameObject prefabLabel;
    public Transform contenedor;
    private List<string> objetosColeccionados;
    private List<GameObject> interrogaciones;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        interrogaciones = new List<GameObject>();
        for (int i = 0; i < contenedor.transform.childCount; i++)
        {
            Transform hijoTransform = contenedor.transform.GetChild(i);
            GameObject hijo = hijoTransform.gameObject;

            if (hijo.CompareTag("interrogacion"))
            {
                interrogaciones.Add(hijo);
            }
        }

        foreach (Transform todosObjetos in contenedor.transform)
        {
            todosObjetos.gameObject.SetActive(false);
        }

        foreach (GameObject interrogacion in interrogaciones)
        {
            interrogacion.gameObject.SetActive(true);
        }

        objetosColeccionados = GameObject.FindGameObjectWithTag("Data").GetComponent<DataController>().arrayEncontrados;
        objetosColeccionados = objetosColeccionados.Distinct().ToList();

        foreach (string obj in objetosColeccionados)
        {

            Debug.Log("Debugeando " + obj.ToLower());
            contenedor.Find(obj.ToLower()).gameObject.SetActive(true);

        }

        for (int i = 0; i < objetosColeccionados.Count; i++)
        {
            if (i < interrogaciones.Count)
            {
                interrogaciones[i].SetActive(false);
            }
        }
    }
}
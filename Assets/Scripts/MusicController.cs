using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private AudioSource musicSource;
    private static MusicController instance;
    public bool stateMute = false;
    public static GameObject botonVolumen;

    // Start is called before the first frame update

    private void Awake()
    {
        botonVolumen = GameObject.FindGameObjectWithTag("BotonVolumen");
        musicSource = GetComponent<AudioSource>();
        // Verifica si ya existe una instancia
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

    public void MuteVolumen()
    {
        if (!stateMute)
        {
            stateMute = true;
        }
        else if (stateMute)
        {
            stateMute = false;
        }
        musicSource.mute = stateMute;
        botonVolumen.GetComponent<Animator>().SetBool("mute", stateMute);
    }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.name == "MenuScene")
        {
            botonVolumen = GameObject.FindGameObjectWithTag("BotonVolumen");
        }
    }
}

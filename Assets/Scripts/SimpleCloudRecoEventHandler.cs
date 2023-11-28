using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;
using static UnityEngine.CullingGroup;
using static Vuforia.CloudRecoBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

[System.Serializable]
public class metadatos
{
    public string formula;
    public string nombre;
    public string modelo3D;
    public string imagenModelo3D;
    public string texto;


    public static metadatos CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<metadatos>(json);
    }

}

//[System.Diagnostics.DebuggerDisplay("{" + nameof(DebuggerDisplay) + "(),nq}")]

public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    //public GameObject asset3D;
    CloudRecoBehaviour mCloudRecoBehaviour;
    bool mIsScanning = false;
    metadatos mTargetMetadata;
    private GameObject objetoNuevo;

    public ImageTargetBehaviour ImageTargetTemplate;

    public GameObject textoFormula;
    public GameObject textoNombre;
    public GameObject textoDescripcion;
    public GameObject textoRecarga;
    public GameObject botonRecarga;




    // Register cloud reco callbacks
    void Awake()
    {
        textoDescripcion.transform.parent.gameObject.SetActive(false);
        textoFormula.transform.parent.gameObject.SetActive(false);
        //botonRecarga.interactable = false;
        botonRecarga.transform.localScale = Vector3.zero;
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco initialized");
    }

    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());

    }
    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;

        if (scanning)
        {
            // Clear all known targets
        }
    }
    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        // Store the target metadata
        mTargetMetadata = metadatos.CreateFromJSON(cloudRecoSearchResult.MetaData);

        // Stop the scanning by disabling the behaviour
        mCloudRecoBehaviour.enabled = false;

        // Build augmentation based on target 
        /*
        if (ImageTargetTemplate)
        
        {
            
            string prefabBundleUrl = "https://drive.google.com/uc?export=download&id=1ooFZFFXvXmMYtJBEMXPQiV7w5z5GEs5m";

            StartCoroutine(FetchGameObjectFromServer(cloudRecoSearchResult, prefabBundleUrl, "prueba", 1419886407, new Hash128()));
        }
            
        */
        string prefabBundleUrl = mTargetMetadata.modelo3D;
        StartCoroutine(FetchGameObjectFromServer(cloudRecoSearchResult, prefabBundleUrl, "prueba", 1419886407, new Hash128()));
        
    }
    //void OnGUI()
    //{

    //    GUIStyle myBoxStyle = new GUIStyle();
    //    Font myFont = (Font)Resources.Load("Fonts / comic", typeof(Font));

    //    myBoxStyle.fontSize = 42;
    //    myBoxStyle.normal.textColor = Color.white;
    //    myBoxStyle.font = myFont;

    //    // Display current 'scanning' status
    //    GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Scanning" : "Not scanning", myBoxStyle);
    //    // Display metadata of latest detected cloud-target
    //    GUI.Box(new Rect(100, 300, 200, 50), "Formula: " + mTargetMetadata.formula, myBoxStyle);
    //    GUI.Box(new Rect(100, 400, 200, 50), "Nombre: " + mTargetMetadata.nombre, myBoxStyle);
    //    GUI.Box(new Rect(100, 500, 200, 50), "Descripcion: " + mTargetMetadata.texto, myBoxStyle);

        
    //    // If not scanning, show button
    //    // so that user can restart cloud scanning
    //    if (!mIsScanning)
    //    {
    //        if (GUI.Button(new Rect(100, 600, 200, 50), "Restart Scanning", myBoxStyle))
    //        {
    //            // Reset Behaviour
    //            mCloudRecoBehaviour.enabled = true;
    //            mTargetMetadata = null;
    //            BorraObjecto();
    //        }
    //    }
    //}

   void OnGUI()
    {
        if (mIsScanning)
        {
            textoRecarga.GetComponent<TextMeshProUGUI>().text = "Escaneando...";
            //botonRecarga.interactable = false;
            botonRecarga.transform.localScale = Vector3.zero;
            textoFormula.transform.parent.gameObject.SetActive(false);
            textoNombre.SetActive(false);
            textoFormula.SetActive(false);
            textoDescripcion.transform.parent.gameObject.SetActive(false);
            textoDescripcion.SetActive(false);

        }
        if(!mIsScanning)
        {
            Debug.Log("Debugeando " + mTargetMetadata.formula + " " + mTargetMetadata.nombre);
            textoRecarga.GetComponent<TextMeshProUGUI>().text = "";
            //botonRecarga.interactable = true;
            botonRecarga.transform.localScale = Vector3.one*2;
            textoFormula.transform.parent.gameObject.SetActive(true);
            textoNombre.SetActive(true);
            textoFormula.SetActive(true);
            textoDescripcion.transform.parent.gameObject.SetActive(true);
            textoDescripcion.SetActive(true);
            textoFormula.GetComponent<TextMeshProUGUI>().text = mTargetMetadata.formula;
            textoNombre.GetComponent<TextMeshProUGUI>().text = mTargetMetadata.nombre;
            textoDescripcion.GetComponent<TextMeshProUGUI>().text = mTargetMetadata.texto;

        }
    }
    public void BotonRecarga()
    {
        // Reset Behaviour
        
        mCloudRecoBehaviour.enabled = true;
        mTargetMetadata = null;
        BorraObjecto();
    }

    IEnumerator FetchGameObjectFromServer(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult, string url, string manifestFileName, uint crcR, Hash128 hashR)
    {

        //Get from generated manifest file of assetbundle.
        uint crcNumber = crcR;
        //Get from generated manifest file of assetbundle.
        Hash128 hashCode = hashR;
        UnityWebRequest webrequest =
           UnityWebRequestAssetBundle.GetAssetBundle(url); //, new CachedAssetBundle(manifestFileName, hashCode), crcNumber);


        webrequest.SendWebRequest();

        while (!webrequest.isDone)
        {
            Debug.Log(webrequest.downloadProgress);

        }

        AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(webrequest);
        yield return assetBundle;
        if (assetBundle == null)
            yield break;


        //Gets name of all the assets in that assetBundle.
        string[] allAssetNames = assetBundle.GetAllAssetNames();
        Debug.Log(allAssetNames.Length + "objects inside prefab bundle");
        foreach (string gameObjectsName in allAssetNames)
        {
            string gameObjectName = Path.GetFileNameWithoutExtension(gameObjectsName).ToString();
            GameObject objectFound = assetBundle.LoadAsset(gameObjectName) as GameObject;
            //GameObject objectNuevo = Instantiate(objectFound);
            objetoNuevo = Instantiate(objectFound, ImageTargetTemplate.gameObject.transform);
            objetoNuevo.transform.parent = ImageTargetTemplate.gameObject.transform;
            objetoNuevo.AddComponent<Rotation>();
            GuardaDatosObjecto();
            //objectNuevo.transform.localScale = new Vector3(0.05f,0.05f,0.05f);
            //objectNuevo.transform.parent = ImageTargetTemplate.gameObject.transform;
            mCloudRecoBehaviour.EnableObservers(cloudRecoSearchResult, ImageTargetTemplate.gameObject);

        }
        assetBundle.Unload(false);
        yield return null;
    }

    public void BorraObjecto()
    {
        Destroy(objetoNuevo);
        mCloudRecoBehaviour.ClearObservers(false);
    }
    public void GuardaDatosObjecto()
    {
        GameObject.FindGameObjectWithTag("Data").GetComponent<DataController>().arrayEncontrados.Add(mTargetMetadata.formula);
        Debug.Log("Debugeando " + mTargetMetadata.nombre + " guardado");

        string datosSerializados = string.Join(",", GameObject.FindGameObjectWithTag("Data").GetComponent<DataController>().arrayEncontrados.ToArray());
        PlayerPrefs.SetString("miListaKey", datosSerializados);
        PlayerPrefs.Save();
    }
    private void Start()
    {

    }
}
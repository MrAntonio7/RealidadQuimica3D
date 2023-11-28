using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetBundle : MonoBehaviour
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectoryPath=Application.dataPath+"/AssetsBundles";
        Debug.Log("Building bundled asset");
        try
        {
            //string assetBundleDirectory = "Assets/StreamingAssets";

            /*
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            */

            BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None,
              EditorUserBuildSettings.activeBuildTarget);
            AssetDatabase.Refresh();

        }
        catch (Exception e)
        {
            Debug.Log(" failed to create bundled assets" + e);
            throw;
        }

    }
}

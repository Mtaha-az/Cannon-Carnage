using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WallsManager : MonoBehaviour
{
    public GameObject MobileWalls; // Assign your on-screen buttons in the Unity Inspector
    public GameObject webWells; // Assign your on-screen buttons in the Unity Inspector

    void Start()
    {


        if (isMobile())
        {
            MobileWalls.SetActive(true);
            webWells.SetActive(false);

        }
        else
        {
            MobileWalls.SetActive(false);
            webWells.SetActive(true);
        }


    }
    #region WebGL is on mobile check
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        return IsMobile();
#endif
        // Return false for non-WebGL platforms (including the Unity Editor)
        return false;
    }
    #endregion
}
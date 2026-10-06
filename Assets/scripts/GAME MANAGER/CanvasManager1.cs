using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CanvasManager1 : MonoBehaviour
{

    public GameObject MobileCanvas; // Assign your on-screen buttons in the Unity Inspector
    public GameObject webCanvas; // Assign your on-screen buttons in the Unity Inspector

    void Start()
    {


        if (isMobile())
        {
            MobileCanvas.SetActive(true);
            webCanvas.SetActive(false);

        }
        else
        {
            MobileCanvas.SetActive(false);
            webCanvas.SetActive(true);
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

        return false;
    }

    #endregion
}

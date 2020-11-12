using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class TestDataPath : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Application.dataPath);
        
        GameAnalytics.Initialize();
    }
}

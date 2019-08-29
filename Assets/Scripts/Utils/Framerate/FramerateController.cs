using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateController : MonoBehaviour
{
	void Start () 
	{
		Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
	}

    void Update()
    {
		Application.targetFrameRate = 120;
    }
}
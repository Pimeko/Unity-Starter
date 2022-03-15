using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateController : MonoBehaviour
{
	[SerializeField]
	int targetFrameRate = 120;
	
	void Start () 
	{
		Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = 0;
	}

    void Update()
    {
		Application.targetFrameRate = targetFrameRate;
    }
}
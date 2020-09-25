using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SerializedVirtualCamerasKey : SerializedDictionaryValue<CameraTypeVariable>
{
    public CinemachineVirtualCamera vCam;
}

public class SerializedVirtualCameras : SerializedDictionary<CameraTypeVariable, SerializedVirtualCamerasKey> {}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public void OnEvent(int x)
    {
        print("event " + x);
    }
}
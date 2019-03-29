using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public void OnBasicEvent()
    {
        print("basic event");
    }
    
    public void OnEvent(int x)
    {
        print("TEST : " + x);
    }
}
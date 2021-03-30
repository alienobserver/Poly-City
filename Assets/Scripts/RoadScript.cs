using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    public bool destroy = false;
    void Start()
    {
        
    }

    void SelfDestroy()
    {
        if (destroy)
        {
            Destroy(this);
        }
    }

    void Update()
    {
        SelfDestroy();
    }
}

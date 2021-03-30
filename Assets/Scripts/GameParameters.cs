using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public GameObject canvas;
    public Vector3 nextLevelPos;
    public string nextLevelName;
    public string lastLevel;
    public string thisLevel;
    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("canvas");
        Debug.Log(canvas.transform.GetChild(0).childCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

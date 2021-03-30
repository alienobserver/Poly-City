using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinScript : MonoBehaviour
{
    public GameObject gameController;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController");
            text.text = "Collected diamonds : " + gameController.GetComponent<Scores>().diamonds.ToString();
        }
    }
}

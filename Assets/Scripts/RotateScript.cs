using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float rotateSpeed;
    public GameObject player;
    public GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameController = GameObject.FindWithTag("GameController");
    }

    void checkPos()
    {
        if(gameObject.transform.position.z < gameController.GetComponent<RoadController>().minX)
        {
            Destroy(this.gameObject);
            gameController.GetComponent<RoadController>().realCount--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed));
        checkPos();
    }
}

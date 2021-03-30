using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Material material;
    public Light light;
    public string name;
    public float rotateSpeed;

    void Start()
    {
        this.gameObject.AddComponent<MeshCollider>();
        this.gameObject.GetComponent<MeshCollider>().convex = true;
        this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
        this.gameObject.GetComponent<MeshRenderer>().material = material;

        Light spotLight = Instantiate(light);
        spotLight.transform.position = this.gameObject.transform.position;
        spotLight.transform.SetParent(this.gameObject.transform);
        spotLight.type = LightType.Spot;
        spotLight.color = this.gameObject.GetComponent<MeshRenderer>().material.color;
        spotLight.range = 30;
        spotLight.spotAngle = 120;
        spotLight.intensity = 2;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed));
    }
}

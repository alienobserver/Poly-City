using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public float roadLen = 20;
    public float distance = 5;
    public float roadWidth = 2;
    public float minX;
    public float maxX;
    public GameObject player;
    public GameObject road;
    public List<GameObject> roads;

    public float Y, maxZ;
    public int count;
    public float size;
    public float rotateSpeed;
    public GameObject[] samples;
    public Material[] materials;
    public string[] names;
    public int realCount = 0;
    public Light light;
    void Start()
    {
        List<GameObject> roads = addRoads();
        player = GameObject.FindWithTag("Player");
    }

    List<GameObject> addRoads()
    {
        if(roads.Count == 0)
        {
            roads.Add(Instantiate(road));
            roads[0].transform.position = new Vector3(0, 0 , -10 * roadLen);
            minX = roads[0].transform.position.z;
            roads.Add(Instantiate(road));
            roads[1].transform.position = new Vector3(0, 0, 0);
            roads.Add(Instantiate(road));
            roads[2].transform.position = new Vector3(0, 0, 10 * roadLen);
            maxX = roads[2].transform.position.z;
        }

        else if(player.transform.position.z > roads[0].transform.position.z + roadLen * 10 + distance * 10)
        {
            Destroy(roads[0]);
            roads.Remove(roads[0]);
            roads.Add(Instantiate(road));
            roads[2].transform.position = new Vector3(0, 0, roads[1].transform.position.z + 10 * roadLen);
            minX = roads[0].transform.position.z;
            maxX = roads[2].transform.position.z;
        }
        return roads;
    }

    void itemController()
    {
        if (realCount < count)
        {
            int index = Random.Range(0, samples.Length);
            GameObject sample = samples[index];
            Material material = materials[index];

            GameObject obj = Instantiate(sample);
            obj.transform.position = new Vector3(Random.Range(0, roadWidth*4), Y, Random.Range(player.transform.position.z+ 10*distance, maxX));
            obj.transform.localScale = new Vector3(size, size, size);
            obj.AddComponent<MeshCollider>();
            obj.GetComponent<MeshCollider>().convex = true;
            obj.GetComponent<MeshCollider>().isTrigger = true;
            obj.GetComponent<MeshRenderer>().material = material;
            obj.name = names[index];

            Light spotLight = Instantiate(light);
            spotLight.transform.position = obj.transform.position;
            spotLight.transform.SetParent(obj.transform);
            spotLight.type = LightType.Spot;
            spotLight.color = obj.GetComponent<MeshRenderer>().material.color;
            spotLight.range = 30;
            spotLight.spotAngle = 120;
            spotLight.intensity = 2;

            obj.AddComponent<RotateScript>();
            obj.GetComponent<RotateScript>().rotateSpeed = rotateSpeed;

            realCount++;
        }
    }

    void Update()
    {
        roads = addRoads();
        itemController();
    }
}

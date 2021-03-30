using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    public GameObject stairs;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        createArea(40, 2, 40);
        Debug.Log(gameObject.transform.position);
    }

    GameObject createCube(float x, float y, float z, float size1, float size2, float size3, Material material)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.SetParent(gameObject.transform);
        obj.AddComponent<MeshRenderer>();
        obj.AddComponent<MeshCollider>();
        obj.transform.position = gameObject.transform.position + new Vector3(x, y, z);
        obj.transform.localScale = new Vector3(size1, size2, size3);
        obj.GetComponent<MeshRenderer>().material = material;

        return obj;
    }

    GameObject createObj(GameObject sample, float x, float y, float z, float size1, float size2, float size3, float rotation, Material material)
    {
        GameObject obj = Instantiate(sample);
        obj.transform.SetParent(gameObject.transform);
        obj.AddComponent<MeshRenderer>();
        obj.AddComponent<MeshCollider>();
        obj.transform.position = gameObject.transform.position + new Vector3(x, y, z);
        obj.transform.localScale = new Vector3(size1, size2, size3);
        obj.transform.Rotate(0, rotation, 0);
        obj.GetComponent<MeshRenderer>().material = material;

        return obj;
    }

    void createArea(float x,float y, float z)
    {
        GameObject floor = createCube(0, 0, 0, x + 1, 1, z + 1, material);
        GameObject wall1 = createCube(0, y / 2, z / 2, x + 1, y, 1, material);
        GameObject wall2 = createCube(0, y / 2, -1 * z / 2, x + 1, y, 1, material);
        GameObject wall3 = createCube(x / 2, y / 2, 0, 1, y, z + 1, material);
        GameObject wall4 = createCube(-1 * x / 2, y / 2, 0, 1, y, z + 1, material);

        GameObject stairs1 = createObj(stairs, 0, 0.5f, z / 2 - 2, x / 2, y / 8 * 3, y / 8 * 3, 0, material);
        GameObject stairs2 = createObj(stairs, 0, 0.5f, -1*z / 2 + 2, x / 2, y / 8 * 3, y / 8 * 3, 180, material);
        GameObject stairs3 = createObj(stairs, x / 2 - 2, 0.5f, 0, x / 2, y / 8 * 3, y / 8 * 3, 90, material);
        GameObject stairs4 = createObj(stairs, -1 * x / 2 + 2, 0.5f, 0, x / 2, y / 8 * 3, y / 8 * 3, -90, material);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

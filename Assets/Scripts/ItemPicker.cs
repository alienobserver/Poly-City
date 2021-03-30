using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ItemPicker : MonoBehaviour
{
    public int healthStartVal = 100;
    public int health = 100;
    public int healthTaker = 1;
    public int diamonds = 0;
    public new AudioSource audio;
    public AudioClip sound;

    public string name;

    public int point;

    public int cost;

    public Text coinText;
    public GameObject example;
    public GameObject keyObject;
    public string nextLevelName;
    public string lastLevel;
    public string thisLevel;
    public GameObject canvas;
    public Vector3 nextLevelPos;
    public GameObject gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        canvas = gameController.GetComponent<GameParameters>().canvas;
        nextLevelPos = gameController.GetComponent<GameParameters>().nextLevelPos;
        nextLevelName = gameController.GetComponent<GameParameters>().nextLevelName;
        lastLevel = gameController.GetComponent<GameParameters>().lastLevel;
        thisLevel = gameController.GetComponent<GameParameters>().thisLevel;
        name = example.GetComponent<CoinScript>().name;
        cost = 0;
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = sound;
        audio.volume = 4;
        audio.pitch = 1;
    }

    public GameObject OnSceneChange(GameObject m_MyGameObject)
    {
        if(GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            gameController = GameObject.FindGameObjectsWithTag("GameController")[1];
        }
        else
        {
            gameController = GameObject.FindGameObjectWithTag("GameController");
        }
        m_MyGameObject.GetComponent<ItemPicker>().canvas = gameController.GetComponent<GameParameters>().canvas;
        m_MyGameObject.GetComponent<ItemPicker>().nextLevelPos = gameController.GetComponent<GameParameters>().nextLevelPos;
        m_MyGameObject.GetComponent<ItemPicker>().nextLevelName = gameController.GetComponent<GameParameters>().nextLevelName;
        m_MyGameObject.GetComponent<ItemPicker>().lastLevel = gameController.GetComponent<GameParameters>().lastLevel;
        m_MyGameObject.GetComponent<ItemPicker>().thisLevel = gameController.GetComponent<GameParameters>().thisLevel;

        return m_MyGameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "road")
        {
            health -= healthTaker;
        }
        if(health <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void OnGUI()
    {
        coinText.text = $"{name.ToUpper()} : {cost}";
    }

    public IEnumerator LoadYourAsyncScene(string m_Scene, GameObject m_MyGameObject, GameObject canvas)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        m_MyGameObject.transform.position = nextLevelPos;
        m_MyGameObject.transform.eulerAngles = new Vector3(0,0,0);
        m_MyGameObject.GetComponent<ItemPicker>().thisLevel = thisLevel;
        m_MyGameObject.GetComponent<ItemPicker>().lastLevel = lastLevel;
        m_MyGameObject.GetComponent<ItemPicker>().nextLevelName = nextLevelName;
        m_MyGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_Scene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        m_MyGameObject = OnSceneChange(m_MyGameObject);

        SceneManager.MoveGameObjectToScene(m_MyGameObject, SceneManager.GetSceneByName(m_Scene));
        SceneManager.MoveGameObjectToScene(canvas, SceneManager.GetSceneByName(m_Scene));

        SceneManager.UnloadSceneAsync(currentScene);
        m_MyGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    public IEnumerator LoadYourAsyncSceneForWin(string m_Scene)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_Scene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        GameObject m_MyGameObject = new GameObject();
        m_MyGameObject.tag = "GameController";
        m_MyGameObject.AddComponent<Scores>();
        m_MyGameObject.GetComponent<Scores>().diamonds = cost;

        SceneManager.MoveGameObjectToScene(m_MyGameObject, SceneManager.GetSceneByName(m_Scene));

        SceneManager.UnloadSceneAsync(currentScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"+{point} {name}");
        cost += point;
        if (other.GetComponent<CoinScript>().name != "teleport")
        {
            Destroy(other.gameObject);
        }
        audio.Play();
        if(other.GetComponent<CoinScript>().name == "key")
        {
            if(thisLevel == lastLevel)
            {
                StartCoroutine(LoadYourAsyncSceneForWin("WinScene"));
            }
            if(thisLevel != lastLevel)
            {
                StartCoroutine(LoadYourAsyncScene(nextLevelName, this.gameObject, canvas));
                Debug.Log("DONE");
            }
        }
        else if(other.GetComponent<CoinScript>().name == "teleport")
        {
            this.gameObject.transform.position = other.GetComponent<TeleportScript>().EndPoint;
        }
        else if (other.GetComponent<CoinScript>().name == "health" && health + healthTaker <= healthStartVal)
        {
            health += healthTaker;
        }
    }

    void Update()
    {

    }
}

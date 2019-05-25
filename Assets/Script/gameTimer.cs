using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameTimer : MonoBehaviour {

    public float levelSeconds; 
    private float secondsLeft; // TODO make private later

    private Slider slider;
    private AudioSource audioSource;
    private bool isEndOfLevel = false;
    private LevelManager LevelManager;
    private GameObject winLabel;

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
        audioSource = GetComponent<AudioSource>();
        LevelManager = GameObject.FindObjectOfType<LevelManager>();
        FindYouWin();
        winLabel.SetActive(false);
    }

    void FindYouWin() {
        winLabel = GameObject.Find("You Win");
        if (!winLabel) {
            Debug.LogWarning("Please check win text");
        }
    }

    void Update () {
        slider.value = Time.timeSinceLevelLoad / levelSeconds;

        if (Time.timeSinceLevelLoad >= levelSeconds && !isEndOfLevel) {
            print("level over");
            HandleWinCondition();
        }
    }

    private void HandleWinCondition() {
        //DestroyAllTaggedObjects();
        DestroyAllSpawners();
        audioSource.Play();
        winLabel.SetActive(true);
        Invoke("LoadNextLevel", audioSource.clip.length);
        LoadNextLevel();
        isEndOfLevel = true;
    }

    public GameObject[] spawners;
    void DestroyAllSpawners() {
       // var spawners = GameObject.FindObjectOfType<Spawner>();
        foreach (GameObject spawner in spawners) {
            Destroy(spawner);
        }
    }

    /*void DestroyAllTaggedObjects() {
        GameObject[] taggedObjectArray = GameObject.FindGameObjectsWithTag("destroyOnWin");
        foreach (GameObject taggedObject in taggedObjectArray) {
            Destroy(taggedObject);
        }
    } */

    void LoadNextLevel() {
        LevelManager.LoadNextLevel();
    }
}

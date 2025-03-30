using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.Collections.Generic;


public class CloseDoorScript : MonoBehaviour
{

    private string[] levels = { "Level1", "Level2", "LavaLevel", "Laser Room", "Outside"};
    //private string[] levels;

    //void Awake()
    //{
    //    levels = GetLevelsInBuild();
    //}
    
    //private string[] GetLevelsInBuild()
    //{
    //    int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
    //    string[] scenes = new string[sceneCount];

    //    Debug.Log("Scene count in build settings: " + sceneCount);
    //    for (int i = 0; i < sceneCount; i++)
    //    {
    //        scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
    //    }
    //    return scenes;
    //}

    [SerializeField] private float sceneLoadDelay = 1.0f;
    GameObject skillsCanvas;

    
    private bool levelEnded = false;

    private void Start()
    {
        skillsCanvas = GameObject.Find("SkillsCanvas");
        skillsCanvas.GetComponent<Canvas>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !levelEnded)
        {
            Debug.Log("Player has entered the finish room");
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Finish");
            StartCoroutine(CloseDoorsAndLoadScene(doors));
        }
    }

    private IEnumerator CloseDoorsAndLoadScene(GameObject[] doors)
    {
        levelEnded = true;

        // Close all doors
        foreach (GameObject door in doors)
        {
            StartCoroutine(MoveDoor(door.transform, door.transform.position - new Vector3(0, 3, 0), 2.0f));
        }

        // Wait for doors to finish closing
        yield return new WaitForSeconds(2.0f);

        // Load scene after delay
        yield return new WaitForSeconds(sceneLoadDelay);
        GameObject.Find("GuiCanvas").SetActive(false);



        skillsCanvas.GetComponent<Canvas>().enabled = true;


        Cursor.lockState = CursorLockMode.None;


        FindAnyObjectByType<CameraController>().mouseSensitivity = 0f;
        FindAnyObjectByType<PlayerController>().enabled = false;
        SkillCanvasPopulator populator = skillsCanvas.GetComponent<SkillCanvasPopulator>();
        List<Skill> skillsList = GameManager.instance.skills;

        int NoOfSkills = skillsList.Count;
        Debug.Log($"You have {NoOfSkills} skills in the pool.");
        int skillIndex1 = UnityEngine.Random.Range(0, NoOfSkills);
        Skill skill1 = skillsList[skillIndex1];
        skillsList.RemoveAt(skillIndex1);

        NoOfSkills -= 1;
        int skillIndex2 = UnityEngine.Random.Range(0, NoOfSkills);
        Skill skill2 = skillsList[skillIndex2];
        skillsList.RemoveAt(skillIndex2);

        NoOfSkills -= 1;
        int skillIndex3 = UnityEngine.Random.Range(0, NoOfSkills);
        Skill skill3 = skillsList[skillIndex3];
        skillsList.RemoveAt(skillIndex3);



        populator.skill1 = skill1;
        populator.skill2 = skill2;
        populator.skill3 = skill3;
        populator.UpdateCanvas();
        //LoadNextLevel();

    }

    private IEnumerator MoveDoor(Transform doorTransform, Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = doorTransform.position;

        while (time < duration)
        {
            doorTransform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        doorTransform.position = targetPosition;
    }

    public void LoadNextLevel()
    {
        // deenable currennt scene  folder 
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Current scene: " + currentSceneName);



        // Load a random scene from the levels array
        int nextSceneIndex = UnityEngine.Random.Range(0, levels.Length);
        Debug.Log("Loading next scene: " + levels[nextSceneIndex]);
        SceneManager.LoadScene(levels[nextSceneIndex]);


    }
}
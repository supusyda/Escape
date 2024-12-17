using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string GAME_SCENE = "Game";
    public string LEVEL_SCENE = "LevelSelection";
    // public string MAIN_MENU_SCENE = "Menu";




    public static LevelManager Instance { get; private set; }
    [SerializeField] List<Transform> transitions;
    private int currentLevel = 1;

    private List<string> levelFiles;
    public static UnityEvent<int> OnSelectLevel = new();

    private void Awake()
    {
        // Ensure only one instance of LevelManager exists
        if (Instance == null)
        {
            Instance = this; Debug.Log("DONT DESTROY");
            DontDestroyOnLoad(transform.gameObject);
            // Keep across scene loads
        }
        else
        {
            Destroy(gameObject);
        }

        // Load all level files at the start
        // transitions

        LoadTransition();
        // LoadAllLevelFiles();

    }

    void OnEnable()
    {
        LoadEvent();
    }
    void OnDisable()
    {
        RemoveEvent();
    }


    void LoadTransition()
    {
        Transform allTransition = transform.Find("Transition");
        foreach (Transform transition in allTransition)
        {
            transitions.Add(transition);
        }
    }
    void LoadEvent()
    {

    }



    void RemoveEvent()
    {

    }
    ITransition GetTransitionByName(string name)
    {

        return transitions.First(t => t.name == name).GetComponent<ITransition>();
    }
    // Load all level files ending with level.json


    // Get all available levels
    public List<string> GetAvailableLevels()
    {
        return new List<string>(levelFiles);
    }


    public void TransitionToScene(string sceneName, string transitionName, Action callback = null)
    {
        StartCoroutine(LoadSceneAsync(sceneName, transitionName, callback));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string transitionName, Action callback
     = null)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        ITransition transition = GetTransitionByName(transitionName);

        // Optionally, show a loading screen or progress here
        yield return transition.TransitionIn();
        while (asyncLoad.progress < 0.9f)
        {

            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        // yield return new WaitUntil(() => asyncLoad.isDone);
        yield return null;

        callback?.Invoke();
        yield return transition.TransitionOut();

    }
    IEnumerator NextLevelAnim(string transitionName)
    {
        ITransition transition = GetTransitionByName(transitionName);
        Debug.Log(GetTransitionByName(transitionName));
        yield return transition.TransitionIn();

        yield return transition.TransitionOut();


    }
    // Save level progress (customize as needed)
    public void SetCurrentLevel(int level)
    {
        currentLevel = level;

    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    void OnReset()
    {
        TransitionToScene("Game", "Circle", (() =>
        {
            // GameManager.Instance.ResetLevel();
        }));

    }

    private void OnLevelSelect(int levelID)
    {
        TransitionToScene(GAME_SCENE, "Circle", (() =>
       {
           LevelManager.OnSelectLevel.Invoke(levelID);
       }));
    }
}

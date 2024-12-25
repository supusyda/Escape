using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string GAME_SCENE = "Game";
    public string LEVEL_SCENE = "LevelSelection";
    public string CURRENT_LEVEL = "CurrentLevel";
    public static readonly int MAX_LEVEL = 5;
    // public string MAIN_MENU_SCENE = "Menu";




    public static LevelManager Instance { get; private set; }
    [SerializeField] List<Transform> transitions;
    private int currentLevel = 1;

    private List<string> levelFiles;
    public static UnityEvent<int> OnSelectLevel = new();
    public static UnityEvent<string, string> OnUIBtnTrans = new();


    private void Awake()
    {
        // Ensure only one instance of LevelManager exists
        if (Instance == null)
        {
            Instance = this;
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
        OnUIBtnTrans.AddListener(BtnUITrans);
        // GameManager.OnWinStage.AddListener(OnWinStage);
    }

    public void GoToNextLevel()
    {
        currentLevel++;
        Debug.Log("Current Level: " + currentLevel);
        if (currentLevel > MAX_LEVEL)
        {
            Debug.Log("REACH MAX LEVEL");
            TransistionToLevelSelection();
            currentLevel = MAX_LEVEL;
            return;
        }
        else
        {
            SaveCurrentLevelToPlayerPref();
            LoadCurrentLevelTransistion();
        }
    }
    public void TransistionToLevelSelection()
    {
        Debug.Log("GO TO LEVEL SELECTION");
        TransitionToScene(LEVEL_SCENE, "Circle");
    }
    private void BtnUITrans(string sceneName, string transitionName)
    {
        TransitionToScene(sceneName, transitionName);
    }

    void RemoveEvent()
    {
        OnUIBtnTrans.RemoveListener(BtnUITrans);
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
    public void LoadCurrentLevelTransistion()
    {
        TransitionToScene(GAME_SCENE + currentLevel, "Circle");
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
        TransitionToScene(GAME_SCENE + currentLevel, "Circle");

    }

    public void OnLevelSelect(int levelID)
    {
        currentLevel = levelID;
        TransitionToScene(GAME_SCENE + levelID, "Circle");
    }
    void SaveCurrentLevelToPlayerPref()
    {
        if (LoadCurrentLevelFormPlayerPref() >= currentLevel) return;
        PlayerPrefs.SetInt(CURRENT_LEVEL, currentLevel);
        PlayerPrefs.Save(); // Ensure the changes are written to storage.
        Debug.Log("Level saved: " + currentLevel);
    }
    public int LoadCurrentLevelFormPlayerPref()
    {
        int level = PlayerPrefs.GetInt(CURRENT_LEVEL, 1); // Default to level 1 if not set.

        return level;
    }
    void ResetCurrentLevel()
    {
        TransitionToScene(GAME_SCENE + currentLevel, "Circle");

    }
    void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.R))
        {
            GoToNextLevel();
        }
    }

}

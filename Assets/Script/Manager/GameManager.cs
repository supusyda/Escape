using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public enum GameState
{
    WaitForInput, Play
}
public enum TurnState
{
    Angel, Demon
}
public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static public GameManager instance;
    public TurnState currentTurn = TurnState.Angel;
    public GameState currentGameState { get; private set; }

    [SerializeField] private int currentDemonActiveRePlayIndex = -1; // all demon have index < this varible can be replay
    public List<PlayerBase> angel = new List<PlayerBase>();
    public List<PlayerBase> demons = new List<PlayerBase>();
    [SerializeField] DoorSO doorSO;

    static public UnityEvent OnResetScene = new();// invoke  after some time when the round end
    static public UnityEvent OnEndRound = new();// invoke imitditly after the round end
    static public UnityEvent OnFailRound = new();
    static public UnityEvent OnHasInputActive = new();
    static public UnityEvent OnOutOfTime = new();
    static public UnityEvent OnWinStage = new();


    void OnEnable()
    {
        PlayerBase.LoadPlayer.AddListener(OnPlayerLoad);
        DoorTriggerInGame.OnTouchDoor.AddListener(OnTouchDoor);
        CharacterHealth.OnCharacterHealthDied.AddListener(OnCharDieLogic);
        OnOutOfTime.AddListener(ResetThisRound);

    }
    void OnDisable()
    {
        PlayerBase.LoadPlayer.RemoveListener(OnPlayerLoad);
        DoorTriggerInGame.OnTouchDoor.RemoveListener(OnTouchDoor);
        CharacterHealth.OnCharacterHealthDied.RemoveListener(OnCharDieLogic);
        OnOutOfTime.RemoveListener(ResetThisRound);

    }
    private void OnPlayerLoad(PlayerBase player, bool isDemon)
    {
        if (isDemon)
        {
            demons.Add(player);
        }
        else
        {
            angel.Add(player);
        }
    }
    void Awake()
    {

        doorSO.init();
    }
    void Start()
    {
        instance = this;
        ChangeGameState(GameState.WaitForInput);


        // ChangeTurn(currentTurn);// init the first logic


    }
    void Update()
    {
        if (currentGameState == GameState.WaitForInput)
        {
            WaitForInput();
        }
    }
    void WaitForInput()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            ChangeGameState(GameState.Play);
        }
    }

    private void HandleAllyTurn()
    {
        // Placeholder: Replace this with your actual ally turn logica
        angel[0].playerRecord.ChangeState(RecordState.Record);
        angel[0].transform.gameObject.SetActive(true);

        // currentTurn = TurnState.Angel;
        if (currentDemonActiveRePlayIndex < 0) return; // haven't touch door a single time yet

        DemonAction();

    }

    private void HandleEnemyTurn()
    {
        currentTurn = TurnState.Demon;

        angel[0].playerRecord.ChangeState(RecordState.Replay);//angle

        DemonAction();

    }
    void DemonAction()
    {
        ActiveReplayForDemon();// replay all other demon except for current demon
        if (currentTurn == TurnState.Demon)
        {
            GetDemonByID(currentDemonActiveRePlayIndex).playerRecord.ChangeState(RecordState.Record);//make the current demon reccord
        }
        else
        {
            GetDemonByID(currentDemonActiveRePlayIndex).playerRecord.ChangeState(RecordState.Replay);//make the current demon replay
        }

    }
    private void OnTouchDoor()
    {
        Debug.Log("OnTouchDoor");
        this.Invoke(() =>
        {
            HandleLogicWhenTouchDoor();
        }
        , .5f);


    }
    private void HandleLogicWhenTouchDoor()
    {
        // OnEndRound.Invoke();

        if (currentTurn == TurnState.Angel)// angle win round
        {
            currentDemonActiveRePlayIndex++;

            if (IsNoMoreEnemy())
            {
                OnGameWin();
                return;
            }
            else
            {
                ChangeTurn(TurnState.Demon); // 
            }
        }
        else
        {
            ResetThisRound();
        }
    }
    bool IsNoMoreEnemy()
    {
        return currentDemonActiveRePlayIndex >= demons.Count;
    }
    public void ChangeTurn(TurnState turn)
    {
        OnEndRound.Invoke();

        this.Invoke(() => ChangeTurnLogic(turn), 1f);//wait for a sec before reseting the scene

    }
    private void ChangeTurnLogic(TurnState turn)
    {
        OnResetScene.Invoke();
        ChangeGameState(GameState.WaitForInput);//make the user wait for input
        switch (turn)
        {
            case TurnState.Angel:
                // HandleAllyTurn();
                currentTurn = TurnState.Angel;

                break;
            case TurnState.Demon:
                currentTurn = TurnState.Demon;

                // HandleEnemyTurn();
                break;

            default: break;
        }
    }
    private void HandleCurrentTurn()
    {
        Debug.Log("HandleCurrentTurn");
        switch (currentTurn)
        {
            case TurnState.Angel:
                HandleAllyTurn();
                break;
            case TurnState.Demon:
                HandleEnemyTurn();
                break;
            default:
                break;
        }
    }
    public void ChangeGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WaitForInput:
                currentGameState = GameState.WaitForInput;
                break;
            case GameState.Play:
                OnHasInputActive?.Invoke(); // active input and time for game

                HandleCurrentTurn();// 
                currentGameState = GameState.Play;

                break;

            default: break;
        }
    }
    void OnCharDieLogic(PlayerBase playerThatDied)
    {
        // OnEndStage.Invoke();
        HandleCharDieLogic(playerThatDied);

    }
    private void HandleCharDieLogic(PlayerBase playerThatDied)
    {
        switch (currentTurn)
        {

            case TurnState.Angel:
                if (IsDemon(playerThatDied))//is a demon
                {
                    return;
                }
                else
                {

                    ResetThisRound();
                    //Reset current iteration
                }
                break;
            case TurnState.Demon:
                if (IsDemon(playerThatDied))//is a demon
                {
                    //not do any thing
                    Demon demon = playerThatDied as Demon;
                    if (demon.GetID() != currentDemonActiveRePlayIndex) return;
                    // if the demon that died is the current demon then restart the round
                    ResetThisRound();
                    //Reset current iteration   

                }
                else // angle died (WIN ROUND)
                {
                    doorSO.NextDoorOrder();
                    ChangeTurn(TurnState.Angel);
                }
                break;
        }
    }
    void ActiveReplayForDemon()
    {
        foreach (PlayerBase item in demons)
        {
            Demon demon = item as Demon;
            if (demon.GetID() < currentDemonActiveRePlayIndex)
            {
                demon.playerRecord.ChangeState(RecordState.Replay);
            }
        }
    }

    void ResetThisRound()
    {
        OnFailRound.Invoke();

        if (currentTurn == TurnState.Angel) angel[0].playerRecord.ClearRecord();//clear the record

        else GetDemonByID(currentDemonActiveRePlayIndex).playerRecord.ClearRecord(); // clear the record

        ChangeTurn(currentTurn);// just to restart the turn logic
                                // ChangeGameState(GameState.WaitForInput);//make the user wait for input

        Debug.Log("RESET THIS ROUND");
    }

    Demon GetDemonByID(int ID)
    {
        foreach (PlayerBase item in demons)
        {
            Demon demon = item as Demon;
            if (demon.GetID() == ID)
            {
                return demon;
            }
        }
        return null;
    }
    void OnGameWin()
    {

        OnWinStage.Invoke();
    }
    private bool IsDemon(PlayerBase player) // just for now. can be upgrade
    {
        return player != angel[0];
    }
}
public static class Utility
{
    public static void Invoke(this MonoBehaviour mb, Action f, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }

    private static IEnumerator InvokeRoutine(Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }
}
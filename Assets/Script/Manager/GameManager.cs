using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public enum GameState
{
    WaitForInput, Win, Lose, SwitchController
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
    [SerializeField] private int currentDemonActiveRePlayIndex = -1;
    public List<PlayerBase> angel = new List<PlayerBase>();
    public List<PlayerBase> demons = new List<PlayerBase>();

    static public UnityEvent OnResetScene = new();
    static public UnityEvent OnEndTurn = new();




    void OnEnable()
    {
        PlayerBase.LoadPlayer.AddListener(OnPlayerLoad);
        DoorTriggerInGame.OnTouchDoor.AddListener(OnTouchDoor);
        CharacterHealth.OnCharacterHealthDied.AddListener(OnCharDieLogic);

    }
    void OnDisable()
    {
        PlayerBase.LoadPlayer.RemoveListener(OnPlayerLoad);
        DoorTriggerInGame.OnTouchDoor.RemoveListener(OnTouchDoor);
        CharacterHealth.OnCharacterHealthDied.RemoveListener(OnCharDieLogic);

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
    void Start()
    {
        instance = this;
        ChangeTurn(currentTurn);
    }


    private void HandleAllyTurn()
    {
        // Placeholder: Replace this with your actual ally turn logica
        angel[0].playerRecord.ChangeState(RecordState.Record);
        angel[0].transform.gameObject.SetActive(true);

        currentTurn = TurnState.Angel;
        if (currentDemonActiveRePlayIndex < 0) return; // haven't touch door a single time yet

        DemonAction();

    }

    private void HandleEnemyTurn()
    {
        currentTurn = TurnState.Demon;

        angel[0].playerRecord.ChangeState(RecordState.Replay);

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


        if (currentTurn == TurnState.Angel)
        {
            currentDemonActiveRePlayIndex++;

            if (IsNoMoreEnemy()) { Debug.Log("NO MORE ENEMY"); return; }

            ChangeTurn(TurnState.Demon);
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
        OnResetScene.Invoke();

        switch (turn)
        {
            case TurnState.Angel:
                HandleAllyTurn();
                break;
            case TurnState.Demon:
                HandleEnemyTurn();
                break;

            default: break;
        }
    }
    void OnCharDieLogic(PlayerBase player)
    {
        switch (currentTurn)
        {

            case TurnState.Angel:
                if (player != angel[0])//is a demon
                {
                    return;
                }
                else
                {
                    ResetThisRound();
                    //TODO:Reset current iteration
                }
                break;
            case TurnState.Demon:
                if (player != angel[0])//is a demon
                {
                    //TODO:Reset current iteration

                }
                else // angle died (WIN ROUND)

                {

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

        if (currentTurn == TurnState.Angel) angel[0].playerRecord.ClearRecord();

        else demons[currentDemonActiveRePlayIndex].playerRecord.ClearRecord();

        ChangeTurn(currentTurn);// just to restart the turn logic
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

}

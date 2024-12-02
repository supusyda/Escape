using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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


    void OnEnable()
    {
        PlayerBase.LoadPlayer.AddListener(OnPlayerLoad);
        DoorTriggerInGame.OnTouchDoor.AddListener(OnTouchDoor);
    }
    void OnDisable()
    {
        PlayerBase.LoadPlayer.RemoveListener(OnPlayerLoad);
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

    void Update()
    {
        // Handle turn-specific logic
        switch (currentTurn)
        {
            case TurnState.Angel:
                // HandleAllyTurn();
                break;
            case TurnState.Demon:
                // HandleEnemyTurn();
                break;
        }
    }

    private void HandleAllyTurn()
    {
        // Placeholder: Replace this with your actual ally turn logic
        angel[0].playerRecord.ChangeState(RecordState.Record);
        currentTurn = TurnState.Angel;
        if (currentDemonActiveRePlayIndex < 0) return; // haven't touch door a single time yet

        ActiveReplayForDemon();
        GetDemonByID(currentDemonActiveRePlayIndex).playerRecord.ChangeState(RecordState.Replay);
    }

    private void HandleEnemyTurn()
    {
        currentDemonActiveRePlayIndex++;

        angel[0].playerRecord.ChangeState(RecordState.Replay);
        ActiveReplayForDemon();
        GetDemonByID(currentDemonActiveRePlayIndex).playerRecord.ChangeState(RecordState.Record);

        currentTurn = TurnState.Demon;
    }

    private void OnTouchDoor()
    {


        if (currentTurn == TurnState.Angel)
        {
            ChangeTurn(TurnState.Demon);
        }
        else
        {
            ChangeTurn(TurnState.Angel);

        }
    }
    public void ChangeTurn(TurnState turn)
    {
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

using UnityEngine;
public enum GameState
{
    WaitForInput, Win, Lose, SwitchController
}
public enum TurnState
{
    Angle, Demon
}
public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static public GameManager instance;
    public TurnState currentTurn = TurnState.Angle;

    void Update()
    {
        // Handle turn-specific logic
        switch (currentTurn)
        {
            case TurnState.Angle:
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
        if (Input.GetKeyDown(KeyCode.Space)) // Example of ending the turn
        {
            Debug.Log("Ally Turn Ended");
            EndTurn();
        }
    }

    private void HandleEnemyTurn()
    {
        // Placeholder: Replace this with your actual enemy AI logic
        Debug.Log("Enemy Turn in Progress");
        Invoke("EndTurn", 2f); // Example of ending turn after 2 seconds
    }

    public void EndTurn()
    {
        if (currentTurn == TurnState.Angle)
        {
            currentTurn = TurnState.Demon;
        }
        else
        {
            currentTurn = TurnState.Angle;
        }
    }
    public void ChangeTurn(TurnState turn)
    {
        switch (turn)
        {
            case TurnState.Angle:
                currentTurn = TurnState.Angle;
                break;
            case TurnState.Demon:
                currentTurn = TurnState.Demon;
                break;

            default: break;
        }
    }
}

using UnityEngine;

public class InputCommandShoot : ICommand
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Shooting shooting;
    public InputCommandShoot(Shooting shooting)
    {
        this.shooting = shooting;
    }
    public void Execute()
    {
        shooting.Shoot();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}

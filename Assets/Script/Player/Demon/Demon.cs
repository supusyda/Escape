using UnityEngine;

public class Demon : PlayerBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int ID;

    public int GetID()
    {
        return ID;
    }

}

using UnityEngine;

public class NexLevelBtn : Btnbase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void OnClick()
    {
        base.OnClick();
        LevelManager.Instance.GoToNextLevel();
    }
}

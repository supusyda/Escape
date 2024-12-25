using UnityEngine;

public class GoToLevelSelectBtn : Btnbase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    override protected void OnClick()
    {
        base.OnClick();
        LevelManager.Instance.TransistionToLevelSelection();
    }
}

using UnityEngine;

public class CloseBtn : Btnbase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform targetClose;
    protected override void OnClick()
    {
        base.OnClick();
        targetClose.gameObject.SetActive(false);
    }
}

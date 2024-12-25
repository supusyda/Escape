using UnityEngine;

public class openBtn : Btnbase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform target;
    override protected void OnClick()
    {
        target.GetComponent<iUIShow>().ShowUI();
    }
}

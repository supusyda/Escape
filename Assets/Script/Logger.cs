using UnityEngine;

public class Logger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Setting")]
    [SerializeField] private bool showLogs;
    [SerializeField] private string prefix;
    [SerializeField] private Color prefixColor;

    public void Log(object message, Object sender)
    {
        if (!showLogs) return;

        // Convert Color to a hex string
        string colorHex = ColorUtility.ToHtmlStringRGBA(prefixColor);

        Debug.Log($"<color=#{colorHex}>{message}</color> ", sender);
    }

}

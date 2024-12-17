using UnityEngine;
using UnityEngine.UI;

public class Btnbase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected Button button;

    protected virtual void Awake()
    {
        // Initialize the button component
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("No Button component found on " + gameObject.name);
        }
        else
        {
            button.onClick.AddListener(OnClick);
        }
    }

    // Override this method in derived classes for custom behavior
    protected virtual void OnClick()
    {
        Debug.Log("Button clicked: " + gameObject.name);
    }

    protected virtual void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnClick);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Error
{
    public string name;
    public GameObject panel;
    public TextMeshProUGUI errorText;
}

public class ErrorController : MonoBehaviour
{
    public static ErrorController Instance { get; private set; }

    [SerializeField]
    private List<Error> errors = new List<Error>();

    private Dictionary<string, Error> errorDictionary = new Dictionary<string, Error>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach (var error in errors)
        {
            if (!errorDictionary.ContainsKey(error.name))
                errorDictionary.Add(error.name, error);
        }
    }

    public void DisplayError(string errorName)
    {
        if (errorDictionary.ContainsKey(errorName))
        {
            Error error = errorDictionary[errorName];
            error.errorText.text = errorName;
            error.panel.SetActive(true);
            StartCoroutine(WaitForTimeAndDestroy(error.panel));
        }
        else
        {
            Debug.LogWarning("Error panel not found for error: " + errorName);
        }
    }

    private IEnumerator WaitForTimeAndDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextColorChanger : MonoBehaviour
{
    private TextMeshProUGUI textToChange;
    [SerializeField] private Color colorSelected;
    [SerializeField] private Color colorNonSelected;


    private void Awake()
    {
        if (textToChange == null)
        {
            textToChange = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (textToChange == null)
        {
            return;
        }

        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            textToChange.color = colorSelected;

        }
        else
        {
            textToChange.color = colorNonSelected;
        }
    }
}

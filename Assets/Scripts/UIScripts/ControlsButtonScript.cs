using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Script on controls button, switching controls and triggering main menu update method
public class ControlsButtonScript : MonoBehaviour
{
    [SerializeField]
    MainMenuScript mainMenuScript;

    [SerializeField]
    private TextMeshProUGUI textGUI;

    private ControlsBaseClass.ControlType currentControlType;

    private void Start()
    {
        currentControlType = ControlsBaseClass.ControlType.Keyboard;
        textGUI.text = "Управление: клавиатура";
    }


    public void SwitchControls()
    {
        if (currentControlType == ControlsBaseClass.ControlType.Keyboard)

        {
            currentControlType = ControlsBaseClass.ControlType.MouseKeyboard;
            textGUI.text = "Управление: клавиатура + мышь";
        }

        else

        {
            currentControlType = ControlsBaseClass.ControlType.Keyboard;
            textGUI.text = "Управление: клавиатура";
        }

        mainMenuScript.ChangeControlType(currentControlType);
    }
}

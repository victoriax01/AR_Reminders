using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrgencyButtonController : MonoBehaviour
{
    public GameObject ReminderMenu;
    private ReminderMenuController ReminderMenuScript;
    private GameObject redButton;
    private GameObject yellowButton;
    private GameObject greenButton;
    private Color red = new Color(255, 37, 0, 255);
    private Color yellow = new Color(255, 238, 0, 255);
    private Color green = new Color(49, 241, 0, 255);
    private Color red_t = new Color(255, 37, 0, 130);
    private Color yellow_t = new Color(255, 238, 0, 130);
    private Color green_t = new Color(49, 241, 0, 130);

    void Start()
    {
        ReminderMenuScript = ReminderMenu.GetComponent<ReminderMenuController>();

        Transform[] allChildren = GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.name == "RedButton")
            {
                redButton = child.gameObject;
            }
            else if (child.gameObject.name == "YellowButton")
            {
                yellowButton = child.gameObject;
                
            }
            else if (child.gameObject.name == "GreenButton")
            {
                greenButton = child.gameObject;
            }
        }
    }

    private void UnselectButton(GameObject button)
    {
        Color buttonC = button.GetComponent<Graphic>().color;
        Color newCol = new Color(buttonC.r, buttonC.g, buttonC.b, 0.5f);
        button.GetComponent<Graphic>().color = newCol;
    }

    public void UnselectAllButtons()
    {
        ReminderMenuScript.urgencyId = -1;
        Color red = redButton.GetComponent<Graphic>().color;
        Color newCol = new Color(red.r, red.g, red.b, 0.5f);
        redButton.GetComponent<Graphic>().color = newCol;

        Color yellow = yellowButton.GetComponent<Graphic>().color;
        newCol = new Color(yellow.r, yellow.g, yellow.b, 0.5f);
        yellowButton.GetComponent<Graphic>().color = newCol;

        Color green = greenButton.GetComponent<Graphic>().color;
        newCol = new Color(green.r, green.g, green.b, 0.5f);
        greenButton.GetComponent<Graphic>().color = newCol;
    }

    private void SelectButton(GameObject button, int id)
    {
        UnselectAllButtons();
        Color buttonC = button.GetComponent<Graphic>().color;
        Color newCol = new Color(buttonC.r, buttonC.g, buttonC.b, 1f);
        button.GetComponent<Graphic>().color = newCol;
        ReminderMenuScript.urgencyId = id;
    }

    public void RedButton()
    {
        if (ReminderMenuScript.urgencyId != 0)
        {
            SelectButton(redButton, 0);
        }
        else
        {
            UnselectAllButtons();
        }
    }

    public void YellowButton()
    {
        if (ReminderMenuScript.urgencyId != 1)
        {
            SelectButton(yellowButton, 1);
        }
        else
        {
            UnselectAllButtons();
        }
    }

    public void GreenButton()
    {
        if (ReminderMenuScript.urgencyId != 2)
        {
            SelectButton(greenButton, 2);
        }
        else
        {
            UnselectAllButtons();
        }
    }
}

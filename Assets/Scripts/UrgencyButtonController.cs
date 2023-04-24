using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrgencyButtonController : MonoBehaviour
{
    /*
    Script attached to 'UrgencyButtons' which is the parent object of the 3 colour indicator buttons which allow users to add colours to their reminders
    */
    public GameObject ReminderMenu;
    private ReminderMenuController ReminderMenuScript;
    private GameObject redButton;
    private GameObject yellowButton;
    private GameObject greenButton;

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
        // make button transparent again to indicate it is unselected
        Color buttonC = button.GetComponent<Graphic>().color;
        Color newCol = new Color(buttonC.r, buttonC.g, buttonC.b, 0.5f);
        button.GetComponent<Graphic>().color = newCol;
    }

    public void UnselectAllButtons()
    {
        ReminderMenuScript.urgencyId = -1; // set urgencyID back to -1 so no colours are selected

        // set all buttons to transparent to indicate none are selected
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
        UnselectAllButtons(); // ensure no other buttons are selected

        // change the transparency of the button to 1 so it is fully opaque
        Color buttonC = button.GetComponent<Graphic>().color;
        Color newCol = new Color(buttonC.r, buttonC.g, buttonC.b, 1f);
        button.GetComponent<Graphic>().color = newCol;

        ReminderMenuScript.urgencyId = id; // set urgencyId to correct ID
    }

    public void RedButton()
    {
        // called when the red colour button is pressed
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
        // called when the yellow colour button is pressed
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
        // called when the green colour button is pressed
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

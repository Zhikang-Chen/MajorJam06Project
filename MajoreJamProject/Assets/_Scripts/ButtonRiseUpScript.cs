using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ButtonRiseUpScript : MonoBehaviour
{
    // This will be ball when all button has been press in the right order just hook this to other code
    public UnityEvent doThings = new UnityEvent();

    // This will be ball when all button has been press in the right order just hook this to other code
    public UnityEvent updateThings = new UnityEvent();

    [SerializeField]
    List<ButtonScript> buttons;

    [SerializeField]
    private int currentNum = 0;

    public Material red;
    public Material green;

    [SerializeField]
    bool hasBeenSolve = false;

    [SerializeField]
    private GameObject theThing;

    private void Start()
    {
        theThing.SetActive(false);
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].number = i;
            buttons[i].render.material = red;
            buttons[i].onTouch = CheckNum;
        }
    }

    void CheckNum(int v)
    {
        if (hasBeenSolve)
            return;

        if(currentNum == v)
        {
            buttons[v].render.material = green;
            currentNum++;

            if (currentNum == buttons.Count)
            {
                hasBeenSolve = true;
                theThing.SetActive(true);
                doThings.Invoke();
            }
        }
        else
        {
            ResetState();
        }
    }

    void ResetState()
    {
        currentNum = 0;
        for (int i = 0; i < buttons.Count; ++i)
        {
            buttons[i].render.material = red;
            buttons[i].ResetTouch();
        }
    }
}

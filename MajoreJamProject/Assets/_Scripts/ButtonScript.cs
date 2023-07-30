using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ButtonScript : MonoBehaviour
{
    public int number;
    public UnityAction<int> onTouch = null;
    public bool hadBeenTouch = false;
    public Renderer render; 
    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !hadBeenTouch)
        {
            hadBeenTouch = true;

            if(onTouch != null)
                onTouch.Invoke(number);
        }
    }

    public void ResetTouch()
    {
        hadBeenTouch = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

    public Transform arrpoint;

    public Button[] buttons;

    float RotateAngel;
    // Use this for initialization
    void Start () {
        buttons = GetComponentsInChildren<Button>();
        Update1();
    }

    // Update is called once per frame
    void Update1 () {

        buttons[0].transform.Rotate(new Vector3(0,0,1),30);
        for (int i = 1; i < buttons.Length; i++)
        {
            if (i<=buttons.Length/2)
            {
                buttons[i].transform.Translate(new Vector3((300 / buttons.Length) * i, 100 / buttons.Length * i, 0));
            }
            else
            {
                buttons[i].transform.Translate(new Vector3((300/ buttons.Length) * i, 100 / buttons.Length * (buttons.Length-i), 0));
                Debug.Log(buttons[i].transform.localPosition);
            }
            
            buttons[i].transform.Rotate(new Vector3(0, 0, 1), 30 - ((90 / buttons.Length) * i));
            Debug.Log(buttons[i].transform.localEulerAngles.z);
            //HandCardAnimation(ListHandCard[i - 1], 30 - _FloRotateAngel * (float)i * ListHandCard.Count + 2.5F);
        }
        //buttons[buttons.Length-1].transform.RotateAround(arrpoint.position, new Vector3(0, 0, 1), -27.5F + RotateAngel);
        //HandCardAnimation(ListHandCard[ListHandCard.Count - 1], -27.5F + _FloRotateAngel);        
    }
    void Update()
    {
        //buttons[0].transform.Rotate(Vector3.forward, 40*Time.deltaTime);
    }
    public void aaaa()
    {
        RotateAngel= 55F / (float)buttons.Length / (float)buttons.Length;
    }
}

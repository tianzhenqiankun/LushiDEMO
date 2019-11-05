using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour {

    public Transform[] transforms;
    public Vector3 oldTrs;
    public Vector3[] lasTrs ;
    // Use this for initialization
    void Start () {
        lasTrs = new Vector3[4];
        oldTrs = transform.position;
        lasTrs[0] = oldTrs;
        for (int i = 1; i < transforms.Length+1; i++)
        {
            lasTrs[i] = transforms[i - 1].position;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            oldTrs = transform.position;
            transform.Translate(Vector3.left * 50 * Time.deltaTime);
            Vector3 v = transform.position - oldTrs;
            Debug.Log(v + "v");
            f(v);
        }
        if (Input.GetKey(KeyCode.A))
        {
            oldTrs = transform.position;
            transform.Translate(Vector3.up * 50 * Time.deltaTime);
            Vector3 v = transform.position - oldTrs;
            Debug.Log(v+"v");
            f(v);
        }
    }

    public void f(Vector3 v)
    {
        for (int i =0; i <transforms.Length ; i++)
        {
            Vector3 v1 = lasTrs[i] - transforms[i].position;
            Vector3 v2 = v1.normalized*v.magnitude;
            Debug.Log(v1);
            transforms[i].position += v2;
            if (Vector3.Distance(transforms[i].position,lasTrs[i])<0.8)
            {
                Debug.Log("111111");
                if (i==0)
                {
                    lasTrs[i] = transform.position;
                }
                else
                {
                    lasTrs[i] = transforms[i - 1].position;
                }
            }
        }
    }
}

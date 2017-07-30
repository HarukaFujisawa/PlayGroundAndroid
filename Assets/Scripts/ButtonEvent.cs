using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour {

    public int id;

    //private bool isFirstDown;

    // Use this for initialization
    void Start () {
        //isFirstDown = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onButtonDown()
    {
        Debug.Log("button down");

        //if (isFirstDown == false)
        //{
            //コールバック関数よびたい
            GetComponentInParent<RemoteTablet>().sendID(id.ToString());
            //isFirstDown = true;
        //}
    }

    public void onButtonUp()
    {
        Debug.Log("button up");

        //isFirstDown = false;
    }

    public void setID(int _id)
    {
        id = _id;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : EventTrigger {

    public int idX, idY;
    GameObject panel;
    float panelWidth, panelHeight; 

    // Use this for initialization
    void Start () {
        panel = GameObject.Find("Panel");
        panelWidth =  panel.GetComponent<RectTransform>().sizeDelta.x;
        panelHeight = panel.GetComponent<RectTransform>().sizeDelta.y;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void onButtonDown(PointerEventData data)
    public override void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("button down");

        //holo2台分送らないとだめ
        //GetComponentInParent<RemoteTablet>().sendID(idX, idY);
        GetComponentInParent<SendData>().sendIDtoHolo(idX, idY);

        //float x = eventData.position.x / Screen.width;
        //float y = eventData.position.y / Screen.height;
        //GetComponentInParent<RemoteTablet>().sendID(x.ToString(), y.ToString());

        base.OnPointerDown(eventData);
    }

    //public void onButtonUp()
    public override void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("button up");
        base.OnPointerUp(eventData);
    }

    public void setID(int _idx, int _idy)
    {
        idX = _idx;
        idY = _idy;
    }

}

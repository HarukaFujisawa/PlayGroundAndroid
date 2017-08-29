using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gogyo.Network;

public class SendData : MonoBehaviour {

    public RemoteTablet[] remoteTablets;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void sendIDtoHolo(int idx, int idy)
    {
        //remoteTablet1.sendID(idx, idy);
        //remoteTablet2.sendID(idx, idy);

        string sendData = "{\"idX\":\"" + idx.ToString() + "\",  \"idY\":\"" + idy.ToString() + "\"}";

        for (int i = 0; i < remoteTablets.Length; i++)
        {
            if (remoteTablets[i].Target.Count > 0)
            {
                remoteTablets[i].Send(sendData);
            }
        }

        //↑は以下と同じ
        //foreach (PeripheralConnectedDevice d in remoteTablet.Target)
        //{
        //    string sendData = "{\"idX\":\"" + idx.ToString() + "\",  \"idY\":\"" + idy.ToString() + "\"}";
        //    d.Send(sendData);
        //}

    }
}

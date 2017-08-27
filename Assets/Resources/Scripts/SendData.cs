using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gogyo.Network;

public class SendData : MonoBehaviour {

    public RemoteTablet remoteTablet1;
    //public RemoteTablet remoteTablet2;

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
        if (remoteTablet1.Target.Count > 0)
        {
            remoteTablet1.Send(sendData);
        }
        //if (remoteTablet2.Target.Count > 0)
        //{
        //    remoteTablet2.Send(sendData);
        //}

        //↑は以下と同じ
        //foreach (PeripheralConnectedDevice d in remoteTablet.Target)
        //{
        //    string sendData = "{\"idX\":\"" + idx.ToString() + "\",  \"idY\":\"" + idy.ToString() + "\"}";
        //    d.Send(sendData);
        //}

    }
}

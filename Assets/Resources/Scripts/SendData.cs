using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gogyo.Network;

public class SendData : MonoBehaviour {

    //public RemoteTablet remoteTablet1;
    //public RemoteTablet remoteTablet2;

    public RemoteTablet remoteTablet;

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

        if (remoteTablet.Target.Count > 0)
        {
            string sendData = "{\"idX\":\"" + idx.ToString() + "\",  \"idY\":\"" + idy.ToString() + "\"}";
            remoteTablet.Send(sendData);
        }
        //remoteTablet1.Send(sendData);
        //remoteTablet2.Send(sendData);

        //↑は以下と同じ
        //foreach (PeripheralConnectedDevice d in remoteTablet.Target)
        //{
        //    string sendData = "{\"idX\":\"" + idx.ToString() + "\",  \"idY\":\"" + idy.ToString() + "\"}";
        //    d.Send(sendData);
        //}

    }
}

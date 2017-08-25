using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendData : MonoBehaviour {

    public RemoteTablet remoteTablet1;
    public RemoteTablet remoteTablet2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void sendIDtoHolo(int idx, int idy)
    {
        remoteTablet1.sendID(idx, idy);
        remoteTablet2.sendID(idx, idy);

    }
}

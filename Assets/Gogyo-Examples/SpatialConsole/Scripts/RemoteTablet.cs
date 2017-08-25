using UnityEngine;
using Gogyo.Network;
using System;

public class RemoteTablet : PeripheralDevice
{

    public string[] ability;

    // Use this for initialization
    protected override void Start()
    {
        for (int i = 0; i < ability.Length; i++)
        {
            Ability.Add(ability[i]); //hololens1 <--> このタブレット って感じ
        }
        base.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void OnReceive(string data, string address, int port)
    {
    }

    public void sendID(int idx, int idy)
    {
        string sendData = "{\"idX\":\"" + idx.ToString() + "\",  \"idY\":\"" + idy.ToString() + "\"}";//id + " pressed!! to" + base.Target.Address;
        Debug.Log(sendData);
        if (base.Target != null)
        {
            Debug.Log(base.name + ": " + ability[0]);
            base.Send(sendData); //これでこの相手のPeripheralDeviceにデータが送れる
        }
        else
        {
            Debug.Log("target is null : " + ability[0]);
        }
    }
}

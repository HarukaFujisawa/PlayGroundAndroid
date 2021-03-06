﻿using UnityEngine;
using Gogyo.Network;

using System.IO;

public class RemoteMotionSensor : PeripheralDevice
{

    public Animator jumpAnimator;
    public AudioSource audioSource;

    public float parentWidth;
    public float parentHeight;
    public float duckscale;

    //public string[] ability;

    public float acc_thresh;
    public float footStep;

    //jumpの3段階分け用閾値
    public float jumpTime1;
    public float jumpTime2;

    public float jumpPress1;
    public float jumpPress2;

    private Vector3 acc_prev;
    private Vector3 static_acc;

    private Quaternion q_i;

    public string filename;
#if UNITY_EDITOR
    StreamWriter sw;
#endif

    protected override void Start()
    {
        //for (int i = 0; i < ability.Length; i++)
        //{
        //    Ability.Add(ability[i]);
        //}
        //Ability.Add("motion");
        //Ability.Add("push");
        base.Start();
        static_acc = new Vector3(-1, -1f, -1f);

        q_i = new Quaternion(0f, 0f, 0f, 1f);

        jumpAnimator = transform.GetChild(0).GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();

        //parentWidth = transform.parent.GetComponent<Vuforia.ImageTargetBehaviour>().GetSize().x;
        //parentHeight = transform.parent.GetComponent<Vuforia.ImageTargetBehaviour>().GetSize().y;
        duckscale = transform.localScale.x;


    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
#if UNITY_EDITOR

            if (sw != null)
            {
                sw.Flush();
                sw.Close();
            }
            sw = new StreamWriter("./" + filename, false);
            sw.WriteLine("ax,ay,az,rx,ry,rz,rw,gx,gy,gz");
#endif
        }

        //if (Input.GetKeyUp(KeyCode.L))
        //{
        //    base.Send("Key Pressed!! to " + base.Target.Address); //これでこの相手のPeripheralDeviceにデータが送れる
        //}

    }

    public void SetQi()
    {
        q_i *= Quaternion.Inverse(transform.rotation);
    }



    public override void OnReceive(string data, string address, int port)
    {
        var type = JsonUtility.FromJson<NekoProtocol>(data);


        switch (type.type)
        {
            case 0: //motionデータがきたとき
                {
                    var motion = JsonUtility.FromJson<SensorProtocol>(data);

                    Vector3 v = new Vector3(motion.acc.x, motion.acc.y, motion.acc.z);
                    //v.Normalize();

                    //Debug.Log(v.magnitude.ToString("F4"));
#if UNITY_EDITOR
                    if (sw != null)
                    {
                        sw.WriteLine(motion.acc.x + "," + motion.acc.y + "," + motion.acc.z + "," + motion.rot.x + "," + motion.rot.y + "," + motion.rot.z + "," + motion.rot.w + "," + motion.gyro.x + "," + motion.gyro.y + "," + motion.gyro.z);
                    }
#endif
                    // 右手系 → 左手系変換して代入
                    //transform.rotation = new Quaternion(-sensor.rot.x, sensor.rot.y, sensor.rot.z, -sensor.rot.w);
                    //        transform.rotation = new Quaternion(-sensor.rot.x, sensor.rot.y, -sensor.rot.z, sensor.rot.w);
                    transform.rotation = q_i * new Quaternion(motion.rot.x, -motion.rot.z, -motion.rot.y, motion.rot.w); //fuji
                    //DuckObj.transform.localRotation = q_i * new Quaternion(motion.rot.x, -motion.rot.z, -motion.rot.y, motion.rot.w); //fuji

                    //軸合わせ試す
                    //transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.z);
                    //transform.Rotate(new Vector3(0f, -90f, 0f));
                    //transform.Rotate(new Vector3(0f, 0f, -90f));

                    if (static_acc.x == -1f && static_acc.y == -1f && static_acc.z == -1f)
                    {
                        static_acc = new Vector3(motion.acc.x, motion.acc.y, motion.acc.z);
                    }


                    if (acc_prev == Vector3.zero)
                    {
                        //acc_prev = v;
                    }
                    else
                    {

                        Vector3 acc_diff = v - acc_prev;

                        //vを正規化しないで、nolmの1G(静止状態の重力分)からの差が閾値だったら、にする
                        if (v.magnitude < acc_thresh)
                        {
                            if (-parentWidth / 2 < transform.localPosition.x / duckscale && transform.localPosition.x / duckscale < parentWidth / 2
                             && -parentHeight / 2 < transform.localPosition.z / duckscale && transform.localPosition.z / duckscale < parentHeight / 2) //playgorund外に出ないように
                            {
                                transform.position += transform.forward * footStep;
                                transform.localPosition = new Vector3(transform.localPosition.x, 0.063f, transform.localPosition.z); //高さはplaygroundの高さに固定
                            }
                            else
                            {

                                if (-parentHeight / 2 > transform.localPosition.z / duckscale)
                                {
                                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -parentHeight / 2 * duckscale) + new Vector3(0f, 0f, 0.001f * duckscale);
                                }
                                else if (transform.localPosition.z / duckscale > parentHeight / 2)
                                {
                                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, parentHeight / 2 * duckscale) - new Vector3(0f, 0f, 0.001f * duckscale);
                                }

                                if (-parentWidth / 2 > transform.localPosition.x / duckscale)
                                {
                                    transform.localPosition = new Vector3(-parentWidth / 2 * duckscale, transform.localPosition.y, transform.localPosition.z) + new Vector3(0.001f * duckscale, 0f, 0f);
                                }
                                else if (transform.localPosition.x / duckscale > parentWidth / 2)
                                {
                                    transform.localPosition = new Vector3(parentWidth / 2 * duckscale, transform.localPosition.y, transform.localPosition.z) - new Vector3(0.001f * duckscale, 0f, 0f); ;
                                }

                            }
                        }

                    }
                    acc_prev = v;
                }
                break;
            case 1: //pushデータがきたとき
                {
                    var pressure = JsonUtility.FromJson<PressureProtocol>(data);
                    var push = pressure.push;

                    Debug.Log(push.result + ", " + push.time + ", " + push.MaxDiff);

                    switch (push.result)
                    {

                        case 1:
                            break;
                        case 2:

                            audioSource.Play();

                            if ((push.time >= 0f && push.time <= jumpTime1) || (push.MaxDiff >= 0f && push.MaxDiff <= jumpPress1))
                            {
                                Debug.Log("Low");
                                jumpAnimator.SetTrigger("lowTrigger");
                                //jumpAnimator.Play("Jump_low");
                            }
                            else if ((push.time >= jumpTime1 && push.time <= jumpTime2) || (push.MaxDiff >= jumpPress1 && push.MaxDiff <= jumpPress2))
                            {
                                Debug.Log("Mid");
                                jumpAnimator.Play("Jump_mid");
                            }
                            else if (push.time >= jumpTime2 || push.MaxDiff >= jumpPress2)
                            {
                                Debug.Log("High");
                                jumpAnimator.Play("Jump_high");
                            }
                            break;

                        default:
                            break;
                    }



                }
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        if (sw != null)
        {
            sw.Flush();
            sw.Close();
        }
#endif
    }

    [System.Serializable]
    public struct GyroProtocol
    {
        public float x;
        public float y;
        public float z;
    }

    [System.Serializable]
    public struct AccProtocol
    {
        public float x;
        public float y;
        public float z;
    }

    [System.Serializable]
    public struct PostureRotProtocol
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }

    [System.Serializable]
    public struct PushProtocol
    {
        public int result;
        public float time;
        public float MaxDiff;
    }


    [System.Serializable]
    public class NekoProtocol
    {
        public int type;
    }

    [System.Serializable]
    public class SensorProtocol : NekoProtocol
    {
        public int time;
        [SerializeField]
        public GyroProtocol gyro;
        [SerializeField]
        public AccProtocol acc;
        [SerializeField]
        public PostureRotProtocol rot;
    }

    [System.Serializable]
    public class PressureProtocol : NekoProtocol
    {
        [SerializeField]
        public PushProtocol push;
    }
}


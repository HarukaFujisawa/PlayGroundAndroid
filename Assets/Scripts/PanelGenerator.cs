using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGenerator : MonoBehaviour {

    public int divWidth = 3;
    public int divHeight = 2;
    public Canvas canvas;

	// Use this for initialization
	void Start () {
        float panelWidth = Screen.width / divWidth;
        float panelHeight = Screen.height / divHeight;

        for(int i= 0; i < divHeight; i++)
        {
            for (int j = 0; j < divWidth; j++)
            {
                Vector2 pos = new Vector2(panelWidth / 2 * j, panelHeight / 2 * i);
                // プレハブを取得
                GameObject panel = Resources.Load("Prefabs/Panel") as GameObject;
                // プレハブからインスタンスを生成
                Instantiate(panel, pos, Quaternion.identity);
                panel.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 3, Screen.height / 2);
                panel.transform.SetParent(canvas.transform, false);
                panel.transform.localPosition = Vector2.zero;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

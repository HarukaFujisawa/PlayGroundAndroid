using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGenerator : MonoBehaviour {

    public int divWidth;
    public int divHeight;
    public GameObject panel;

    float AreaWidth;
    float AreaHeight;

	// Use this for initialization
	void Start () {
        AreaWidth = panel.GetComponent<RectTransform>().sizeDelta.x;
        AreaHeight = panel.GetComponent<RectTransform>().sizeDelta.y;

        float buttonWidth = AreaWidth / divWidth;
        float buttonHeight = AreaHeight / divHeight;
        int id = 0;

        for (int i= 0; i < divHeight; i++)
        {
            for (int j = 0; j < divWidth; j++)
            {
                Vector2 pos = new Vector2(AreaWidth / 2 - buttonWidth * j - buttonWidth / 2,  AreaHeight / 2 - buttonHeight  * i  - buttonHeight / 2);
                // プレハブを取得
                GameObject prefab = Resources.Load("Prefabs/Button") as GameObject;
                // プレハブからインスタンスを生成
                GameObject button = Instantiate(prefab, pos, Quaternion.identity);
                button.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth, buttonHeight);
                button.transform.SetParent(this.transform, false);
                button.GetComponent<ButtonEvent>().setID(j, i);
                button.name = "Button" + id.ToString();
                button.GetComponentInChildren<Text>().text = button.name;
                id++;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

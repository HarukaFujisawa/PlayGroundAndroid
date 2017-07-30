using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGenerator : MonoBehaviour {

    public int divWidth;
    public int divHeight;

	// Use this for initialization
	void Start () {
        float buttonWidth = Screen.width / divWidth;
        float buttonHeight = Screen.height / divHeight;
        int id = 0;

        for(int i= 0; i < divHeight; i++)
        {
            for (int j = 0; j < divWidth; j++)
            {
                Vector2 pos = new Vector2(Screen.width / 2 - buttonWidth * j - buttonWidth / 2,  Screen.height / 2 - buttonHeight  * i  - buttonHeight / 2);
                // プレハブを取得
                GameObject prefab = Resources.Load("Prefabs/Button") as GameObject;
                // プレハブからインスタンスを生成
                GameObject button = Instantiate(prefab, pos, Quaternion.identity);
                button.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 3, Screen.height / 2);
                button.transform.SetParent(this.transform, false);
                button.GetComponent<ButtonEvent>().setID(id);
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

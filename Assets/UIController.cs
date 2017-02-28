using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    //ゲームオーバーテキスト
    private GameObject gameOverText;

    //走行距離テキスト
    private GameObject runLengthText;

    //走った距離
    private float len = 0;

    //走る速度（実際は移動しないがポイントを増やすため）
    private float speed = 0.03f;

    //ゲームオーバの判定
    private bool isGameOver = false;



	// Use this for initialization
	void Start () {
        //シーンビューからオブジェクトの実体(インスタンス)を検索する
        this.gameOverText = GameObject.Find("GameOver");
        this.runLengthText = GameObject.Find("RunLength");
	}
	
	// Update is called once per frame
	void Update () {
        if (this.isGameOver == false) {
            //走った距離を更新する
            this.len += this.speed;

            //走った距離を表示する
            //「using UnityEngine.UI;」を実装し忘れてると「<Text>」でエラーが出る。UIはTextを管理するためのもの
            this.runLengthText.GetComponent<Text>().text = "Distance: " + len.ToString("F2") + "m";
        }

        //ゲームオーバになった場合(isGameOverがtrueになった場合)
        if (this.isGameOver) {
            //クリックされたらシーンをロードする
            if (Input.GetMouseButtonDown(0)) {
                //GameSceneを読み込む
                SceneManager.LoadScene("GameScene");
            }
        }
	}

    public void GameOver() {
        //ゲームオーバになった時、画面上にゲームオーバを表示する
        //これがUnityChanControllerで呼び出されるということはつまり「isGameOver = true」となる
        this.gameOverText.GetComponent<Text>().text = "GameOver";
            this.isGameOver = true;
    }
}

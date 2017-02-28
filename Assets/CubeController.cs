using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    //キューブの移動速度
    private float speed = -0.2f;

    //消滅位置
    private float deadLine = -10;

    //音を入れるため、AudioSourceコンポーネント(？)を取得するためのメンバ変数を定義
    AudioSource blockSound;


	// Use this for initialization
	void Start () {

        //音を入れるため、AudioSourceのインスタンス（？）を取得
        blockSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //キューブを「移動させる」。位置を取得したいわけではないため「position～」の出番はない
        //また「Translate」は「移動させるもの」であり、「指定の座標で飛ばすもの」ではない。要注意
        transform.Translate(this.speed, 0, 0);

        //画面外に出たら破棄する
        if (transform.position.x < this.deadLine) {
            Destroy(gameObject);
        }	
	}

    //他のオブジェクトと接触した場合に音を鳴らすかを判定
    //「OnCollisionEnter2D」は2D用。「衝突した瞬間だけ」処理をする
    void OnCollisionEnter2D(Collision2D other){
        Debug.Log("ログは来ている");

        //地面・キューブ同士で接触した場合は音が鳴る。「0.4f」は適当
        if (other.gameObject.tag == "CubeTag" || other.gameObject.tag == "GroundTag") {
            blockSound.volume = 0.4f;
            //「Play(0)」は「0秒遅らせて再生するよ」という意味。0(中身が空っぽ)なので「Play()」でもいい
            blockSound.Play(0);
            Debug.Log("ログは来ている1");
        }
        //Unityちゃんと接触した場合は音が鳴らない
        if (other.gameObject.tag == "UnityChanTag") {
            blockSound.volume = 0;
            Debug.Log("ログは来ている2");
        }
    }
}

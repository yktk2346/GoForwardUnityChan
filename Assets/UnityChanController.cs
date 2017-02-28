using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour {
    //アニメーションするためのコンポーネントを入れるための設計図
    Animator animator;

    //Unityちゃんを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;

    //地面の位置
    private float groundLevel = -3.0f;

    //ジャンプの速度の減衰、「dump」は「取り除く」といった意味
    private float dump = 0.8f;

    //ジャンプの速度
    float jumpVelocity = 20;

    //ゲームオーバになる位置
    private float deadLine = -9;



	// Use this for initialization
	void Start () {
        //アニメータのコンポーネントを取得する
        this.animator = GetComponent<Animator>();
        //Rigidbody2Dのコンポーネントを取得する
        this.rigid2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //走るアニメーションを再生するために、Animatorのパラメータを調節する
        //「Horizontal」はアニメーション矢印（切替わり）に設定されてる。「一定数以上(0.1f)」で切替わる
        this.animator.SetFloat("Horizontal", 1);

        //着地しているかどうかを調べる、「isGround」はアニメーション矢印（切替わり）に設定されてる
        //falseでjumpアニメが再生
        //ジャンプ後、yの上昇が減速しgroundLevel(true)に戻ることでjumpアニメがキャンセルされる（？）
        //yがgroundLevelに戻るのはrigid2D.velocityにdump(減速)をかけてるから
        //この部分は三項演算子と呼ばれる部分。「条件式? 真で返す値(false):偽で返す値(true);」という形
        //「"isGround"」はAnimator内の矢印に初期設定されている。そこへ「isGround」を代入
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        this.animator.SetBool("isGround", isGround);

        //ジャンプ状態のときにはボリュームを0にする
        //AudioSourceコンポーネントのvolume変数に「? 真値1(isGroundである) : 偽値0(isGroundでない)」を代入
        GetComponent<AudioSource>().volume = (isGround) ? 1 : 0;

        //着地状態でクリックされた場合、「ボタンが押され、かつ地面にいる場合」
        //「(0)」はマウスのどのボタンかという意味。左ボタンは 0、右ボタンは 1、中ボタンは 2
        if (Input.GetMouseButtonDown (0) && isGround) {
            //上方向の力をかける、rigid2Dのvelocity(速度)に。
            this.rigid2D.velocity = new Vector2(0, this.jumpVelocity);
        }

        //クリックをやめたら上方向への速度を減速する。「false」=ボタンを押さなくなった時？
        if (Input.GetMouseButton(0) == false) {
            if (this.rigid2D.velocity.y > 0) {
                this.rigid2D.velocity *= this.dump;
            }
        }

        //デッドラインを超えた場合ゲームオーバにする
        if (transform.position.x < this.deadLine) {
            //UIControllerスクリプトのGameOver関数を呼び出して画面上に「GameOver」と表示する
            //「public void GameOver」関数を呼び出している
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            //Unityちゃんを破棄する
            Destroy(gameObject);
        }
	}
}

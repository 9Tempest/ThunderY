using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public static Life instance { get; private set; }
    public Text txt;
    public Text txt2;
    public Text txt3;
    public Text txt4;
    public static int x;
    public static int score;
    public float timer;
    // Start is called before the first frame update
    
    void Awake()
    {
        instance = this;
        score = 0;
    }
    void Update(){
        if (timer < 0) {
            txt4.text = "";
        }
        timer -= Time.deltaTime;
    }
    // Update is called once per frame
    public void SetValue(int x){
        txt.text = "X"+x;
    }
    public void SetScore(int x){
        score += x;
        txt2.text = "Score: "+score;
    }
    public void SetBoom(int x){
        txt3.text = "X"+x;
    }
}

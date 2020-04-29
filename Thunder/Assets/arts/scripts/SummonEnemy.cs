using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnemy : MonoBehaviour
{

    private GameController gameCtr;
    public static SummonEnemy instance { get; private set; }
    AudioSource audioSource;
    public GameObject type1;
    public GameObject type2;
    public GameObject Planetype1;
    public GameObject Planetype2;
    public GameObject Boss;
    public float SummonTime_ceiling_1;
    public float SummonTime_floor_1;
    public float SummonTime_ceiling_2;
    public float SummonTime_floor_2;
    public float Summonplane1time;
    public float hrz_range;
    public float vet_range;
    float timer_type1;
    float timer_type2;
    float timer_plane1;
    float timer_littleboss;
    float timer_littleboss_one = 70f;
    public float Summonlittleboss;
    bool haveboss = false;
    bool haveplayed = false;
    public float Bosstime;
    Rigidbody2D rigidbody2d;
    public AudioClip[] Clip;
    public bool iswin = false;
    public float countdowner;
    public GameObject ingameMenu;
    float timesong = 53.0f;
    float timer_song;
    bool imgmode = false;
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameCtr = GameController.instance;
        timer_type1 = 12.0f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer_type2 = 9.0f;
        timer_plane1 = 3.0f;
        timer_littleboss = Summonlittleboss;
        timer_song = 0;
        PlaySound(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (iswin){
            countdowner -= Time.deltaTime;
            if (countdowner < 0) {
                PlaySound(2);
                Pause();
                countdowner = 1000000000f;
            }
            if (Input.GetKeyDown(KeyCode.P)){
                Resume();
                imgmode = true;
                MapMovedown.instance.speed = 1.3f;
                }
            if (timer_song < 0 && imgmode){
                PlaySound(3);
                timer_song = timesong;
                
                
            }
            timer_song -= Time.deltaTime;
            return;
        }
        Vector2 region = gameCtr.object1.position;
        if (Bosstime < 0) {
            Vector2 planepos = region + Vector2.up * 8.5f;
            GameObject boss = Instantiate(Boss, planepos , Quaternion.identity);
            EnemyBoss newplane1 = boss.GetComponent<EnemyBoss>();
            Bosstime = 1000000f;
        }
        if (Bosstime < 12f) {
            haveboss = true;
            if (!haveplayed) {
            PlaySound(1);
            haveplayed = true;
            }
        }
        if (timer_type1 < 0 && !haveboss) {
            Vector2 newpos1 = region + Vector2.right * Random.Range(-hrz_range, hrz_range) + Vector2.up * 8.0f;
            GameObject enemy1 = Instantiate(type1, newpos1, Quaternion.identity);
            Enemy new_enemy1 = enemy1.GetComponent<Enemy>();
            timer_type1 = Random.Range(SummonTime_floor_1, SummonTime_ceiling_1);
        }
        if (timer_type2 < 0 && !haveboss) {
            Vector2 newpos2 = region + Vector2.up * Random.Range(4, vet_range) + Vector2.right * 3.8f;
            GameObject enemy2 = Instantiate(type2, newpos2 , Quaternion.identity);
            EnemyShipType1 new_enemy2 = enemy2.GetComponent<EnemyShipType1>();
            timer_type2 = Random.Range(SummonTime_floor_2, SummonTime_ceiling_2);
        }
        if (timer_plane1 < 0 && !haveboss) {
            Vector2 planepos = region + Vector2.up * 8.0f;
            GameObject plane1 = Instantiate(Planetype1, planepos , Quaternion.identity);
            EnemyPlane_01 newplane1 = plane1.GetComponent<EnemyPlane_01>();
            timer_plane1 = Summonplane1time;
        }
        if (timer_littleboss < 0) {
            Vector2 planepos1 = region + Vector2.up * 8.0f + Vector2.right * 1.0f;
            Vector2 planepos2 = region + Vector2.up * 8.0f + Vector2.left * 1.0f;
            GameObject plane1 = Instantiate(Planetype2, planepos1 , Quaternion.identity);
            EnemyPlane_02 newplane1 = plane1.GetComponent<EnemyPlane_02>();
            GameObject plane2 = Instantiate(Planetype2, planepos2 , Quaternion.identity);
            EnemyPlane_02 newplane2 = plane2.GetComponent<EnemyPlane_02>();
            Summonlittleboss -= 50f;
            timer_littleboss = Summonlittleboss;
            if (Summonlittleboss < 60f) {
                Summonlittleboss = 10000f;
            }
        }
        if (timer_littleboss_one < 0) {
            Vector2 planepos1 = region + Vector2.up * 8.0f; 
            GameObject plane1 = Instantiate(Planetype2, planepos1 , Quaternion.identity);
            EnemyPlane_02 newplane1 = plane1.GetComponent<EnemyPlane_02>();
            timer_littleboss_one = 100000f;
        }
        timer_type1 -= Time.deltaTime;
        timer_type2 -= Time.deltaTime;
        timer_plane1 -= Time.deltaTime;
        timer_littleboss_one -= Time.deltaTime;
        timer_littleboss -= Time.deltaTime;
        Bosstime -= Time.deltaTime;
    }
     public void PlaySound(int i)
{
    audioSource.clip = Clip[i];
    audioSource.Play();
}
    public void Pause(){
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
    }
    public void Resume(){
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
    }
}

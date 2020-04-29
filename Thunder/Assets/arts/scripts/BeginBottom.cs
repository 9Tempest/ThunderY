using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginBottom : MonoBehaviour
{
    public AudioSource audiosource;
    public void Awake(){
        audiosource = GetComponent<AudioSource>();
    }
    public void OnClick(){
        audiosource.Play();
        SceneManager.LoadScene("Game");
    }
}

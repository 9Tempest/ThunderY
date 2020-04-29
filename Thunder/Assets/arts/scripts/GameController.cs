using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    #region Singleton

    static public GameController instance;

    private void Awake() 
    {
        instance = this;
    }

    #endregion

    public Transform player;
    public Transform object1;

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealhtController : MonoBehaviour
{
    static public PlayerHealhtController instance;
    PlayerMovementScript playerMovement;

    public bool isAlive = true;


    

    private void Awake()
    {
        instance = this;
        playerMovement = GetComponent<PlayerMovementScript>();
        
    }

    private void Start()
    {

    }

    public bool IsAlive()
    {
        return isAlive;
    }


    public void DecreaseHealth()
    {                  
        StartCoroutine("GameSessionRoutine");
        playerMovement.ReverseMove();           
    }

    
    IEnumerator GameSessionRoutine()
    {
        yield return new WaitForSeconds(.5f);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
    
}

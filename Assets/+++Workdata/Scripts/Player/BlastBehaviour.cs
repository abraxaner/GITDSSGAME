using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBehaviour : MonoBehaviour
{
    [SerializeField] float selfDestructTimer;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool isShotByPlayer;
    [SerializeField] PlayerController pC;
    void Update()
    {
        #region selfdestruct
        //Instanzen der angriffe l�schen sich nach einiger zeit von selbst
        selfDestructTimer -= Time.deltaTime;
        if(selfDestructTimer <= 0)
        {
            Selfdestruct();
        }
        #endregion
        #region movement
        //bewegt sich im local space nach rechts wodurch die rotation mitberechnet wird im vergleich zum world space
        var locVel = transform.InverseTransformDirection(rb.velocity);
        locVel.x = speed;
        rb.velocity = transform.TransformDirection(locVel);
        #endregion
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //wenn ein gegner von einem projektil des spieler getroffen wird zerst�rt sich das projektil und der gegner
        if (collision.CompareTag("Enemy") == true && isShotByPlayer == true)
        {
            Object.Destroy(collision.gameObject);
            Selfdestruct();
        }
        //wenn der spieler von einem gegner getroffen wird �bernimmt das projektil den player controller und l�st einen game over aus
        else if (collision.CompareTag("Player") == true && isShotByPlayer == false)
        {
            pC = collision.GetComponentInChildren<PlayerController>();
            pC.GameOver();
        }
        //wenn das projektil eine wand trifft wird sie zerst�rt
        else if (collision.CompareTag("Enviroment") == true)
        {
            Selfdestruct();
        }
    }
    void Selfdestruct()
    {
        Object.Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    public float reloadTime;
    public GameObject blast;

    void Update()
    {
        Inputs();
        Move();
        //der spieler kann nur schießen nachdem er nachgeladen hat
        if (reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
        }
        else if(reloadTime <= 0)
        {
            reloadTime = 0;
        }
    }
    void Inputs()
    {
        #region movement
        //der spieler bewegt sich je nach dem welche taste gedrückt wird
        // durch .normalized bewegt er sich diagonal nicht schneller
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        #endregion
    }
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void GameOver()
    {
        Debug.Log("You dead");
        SceneManager.LoadScene(2);
    }
}

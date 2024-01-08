using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] Transform aimTransform;
    [SerializeField] Transform blastSpawnpoint;
    [SerializeField] GameObject blastStartLocation;
    [SerializeField] GameObject enemyBlast;
    [SerializeField] GameObject playerPosition;
    [SerializeField] float reloadTime; 
    private bool seesPlayer;
    [Header("Walking")]
    [SerializeField] GameObject[] points;
    [SerializeField] Rigidbody2D rB;
    [SerializeField] Transform currentPoint;
    [SerializeField] float speed;
    [SerializeField] int nextPoint;
    [SerializeField] int finalPoint;
    [SerializeField] int startPoint;
    [SerializeField] bool onWayBack;
    private void Update()
    {
        #region playerDetection
        //Schießt einen raycast um zu sehen ob der weg zwischen dem gegner und dem spieler frei ist
        //Wenn der weg frei ist kann der gegner auf den spieler schießen
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, playerPosition.transform.position - transform.position);
        if (hit.collider.gameObject.tag == "Player")
        {
            seesPlayer = true;
        }
        else
        {
            seesPlayer = false;
        }
        #endregion
        #region aiming
        //vergleicht die position zwischen spieler und sich selbst
        //der vector zwischen spieler und gegner wird zu einem winkel umgewandelt und danach zu einer rotation für die "waffe" des gegners damit dieser den spieler ins visier nehmen kann
        Vector3 aimDirection = (playerPosition.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        #endregion
        #region shooting
        //die gegner müssen auch nachladen und können nur schießen wenn sie nachgeladen haben und der spieler im sichtfeld ist
        //wenn beides der fall ist wird der spieler beschossen von einem prefab
        reloadTime -= Time.deltaTime;
        blastSpawnpoint = blastStartLocation.transform;
        if (reloadTime <= 0 && seesPlayer)
        {
            reloadTime = 1.75f;
            Instantiate(enemyBlast, blastSpawnpoint.position, aimTransform.rotation);
        }
        #endregion
        #region walking
        //der gegner bewegt sich von punkt zu punkt
        //wenn ein punkt erreicht wird geht er zum nächsten und dann wieder nächsten
        //wenn er den letzten punkt erreicht geht er den weg rückwärts ab
        Vector2 point = currentPoint.position - transform.position;
        rB.velocity = point.normalized * speed;
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            currentPoint = points[nextPoint].transform;
            if (!onWayBack)
            {
                nextPoint++;
            }
            else
            {
                nextPoint--;
            }
            if (nextPoint == finalPoint)
            {
                onWayBack = true;
            }
            else if(nextPoint == startPoint)
            {
                onWayBack = false;
            }
        }
        #endregion
    }
}

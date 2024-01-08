using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform blastSpawnpoint;
    [SerializeField] GameObject blastStartLocation;

    private void Update()
    {
        #region aiming
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        #endregion

        blastSpawnpoint = blastStartLocation.transform;
        if (Input.GetButtonDown("Fire1") == true && playerController.reloadTime <= 0)
        {
            playerController.reloadTime = 0.75f;
            Instantiate(playerController.blast, blastSpawnpoint.position, aimTransform.rotation);
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vect = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vect.z = 0f;
        return vect;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    [Header("Set In Inspector")]
    public GameObject   prefabProjectile;
    public float        velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject   launchPoint;
    public Transform    launchPointTransform;
    public Vector3      launchPos;
    public GameObject   projectile;
    public bool         aimingMode;
    private Rigidbody   projectileRigidBody;

    private void Awake()
    {
        launchPointTransform = transform.Find("LaunchPoint");
        launchPoint = launchPointTransform.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTransform.position;
    }
    private void OnMouseEnter()
    {
        //Debug.Log("Mouse Enter");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        launchPoint.SetActive(false);
        //Debug.Log("Mouse Exit");
    }

    private void OnMouseDown()
    {
        aimingMode = true;

        projectile = Instantiate<GameObject>(prefabProjectile);

        projectile.transform.position = launchPos;

        projectileRigidBody = projectile.GetComponent<Rigidbody>();
        projectileRigidBody.isKinematic = true;

    }

    public void Update()
    {
        if (!aimingMode)
        { 
            return; 
        }

        //obtener la posicion del raton en 2D
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //obtener la diferencia de launchPos a mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos;
        //limit mouseDelta to the radius of the slingshot Sphere Collider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude) 
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //movemos el proyectil a esa nueva pose
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        //leemos si se solto el mouse
        if (Input.GetMouseButtonUp(0)) 
        {
            aimingMode = false;
            projectileRigidBody.isKinematic = false;
            projectileRigidBody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
        }
    }
}

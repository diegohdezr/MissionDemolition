using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    [Header("Set In Inspector")]
    public GameObject prefabProjectile;

    [Header("Set Dynamically")]
    public GameObject   launchPoint;
    public Transform    launchPointTransform;
    public Vector3      launchPos;
    public GameObject   projectile;
    public bool         aimingMode;

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

        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}

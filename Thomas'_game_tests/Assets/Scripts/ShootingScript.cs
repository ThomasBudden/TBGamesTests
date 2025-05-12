using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] private GameObject proj;
    private GameObject lastProj;
    private float shotTime;
    public float bulletDiv;
    private Quaternion currentRotation;
    private GameObject aimPoint;
    private Vector3 currentEulerAngles;

    // Start is called before the first frame update
    void Start()
    {
        shotTime = -0.15f;
        aimPoint = this.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && shotTime + 0.15 < Time.time)
        {
            shotTime = Time.time;
            float randX = Random.Range(-bulletDiv, bulletDiv);
            float randY = Random.Range(-bulletDiv, bulletDiv);
            float randZ = Random.Range(-bulletDiv, bulletDiv);
            currentEulerAngles = new Vector3(randX, randY, randZ) + aimPoint.transform.rotation.eulerAngles;
            currentRotation.eulerAngles = currentEulerAngles;
            Instantiate(proj, this.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.position, currentRotation);
        }
    }
}

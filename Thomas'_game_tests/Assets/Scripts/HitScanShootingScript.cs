using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class HitScanShootingScript : MonoBehaviour
{
    public GameObject aimPoint;
    public float shotTime;
    public float shotSpeed;
    public float bulletDiv;
    private Quaternion currentRotation;
    private Vector3 currentEulerAngles;
    public float damage;
    public Transform muzzle;
    public GameObject lineTracer;
    private GameObject lastLine;
    private LineRenderer lineRenderer;
    public List<float> timeList = new List<float>();
    public List<GameObject> lineList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && shotTime + shotSpeed < Time.time)
        {
            float randX = Random.Range(-bulletDiv, bulletDiv);
            float randY = Random.Range(-bulletDiv, bulletDiv);
            float randZ = Random.Range(-bulletDiv, bulletDiv);
            currentEulerAngles = new Vector3(randX, randY, randZ) + aimPoint.transform.forward;
            currentRotation.eulerAngles = currentEulerAngles;
            RaycastHit hit;
            if (Physics.Raycast(aimPoint.transform.position, currentEulerAngles, out hit, 100))
            {
                Vector3 hitPoint = hit.point;
                if (hit.collider.gameObject.CompareTag("Target"))
                {   
                    string damageNumber = damage.ToString();
                    hit.collider.gameObject.GetComponent<TargetCubeScript>().damageTxt.text = "Damage is " + damageNumber;
                }
                lastLine = Instantiate(lineTracer, muzzle.transform.position, Quaternion.identity);
                lineRenderer = lastLine.GetComponent<LineRenderer>();
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.widthMultiplier = 0.02f;
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.yellow;
                lineRenderer.SetPosition(0, muzzle.transform.position);
                lineRenderer.SetPosition(1, hitPoint);
                timeList.Add(Time.time);
                lineList.Add(lastLine);
            }
            shotTime = Time.time;
        }
        for (int i = 0; i < timeList.Count; i++)
        {
            if (timeList[i] + 0.1 < Time.time)
            {
                Destroy(lineList[i].gameObject);
                lineList.Remove(lineList[i]);
                timeList.Remove(timeList[i]);
            }
        }
    }
}

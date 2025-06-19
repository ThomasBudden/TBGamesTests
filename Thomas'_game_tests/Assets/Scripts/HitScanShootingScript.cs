using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class HitScanShootingScript : MonoBehaviour
{
    public GameObject aimPoint;
    public GameObject aimPointOrigin;
    public GameObject playerCam;
    public GameObject weapon;
    public TMP_Text ammoCountTxt;
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
    public float recoilMult;
    public int maxAmmo;
    public int ammoCount;
    private bool reloading = false;
    public float reloadTime;
    private float reloadStart;
    public List<float> timeList = new List<float>();
    public List<GameObject> lineList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ammoCount = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && shotTime + shotSpeed < Time.time && ammoCount > 0 && reloading == false)
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
            else
            {
                lastLine = Instantiate(lineTracer, muzzle.transform.position, Quaternion.identity);
                lineRenderer = lastLine.GetComponent<LineRenderer>();
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.widthMultiplier = 0.02f;
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.yellow;
                lineRenderer.SetPosition(0, muzzle.transform.position);
                lineRenderer.SetPosition(1, muzzle.transform.forward * 100);
                timeList.Add(Time.time);
                lineList.Add(lastLine);
            }
            ammoCount -= 1;
            float recoilRandVert = Random.Range(0f, 1f);
            float recoilRandHori = Random.Range(-0.5f, 0.5f);
            aimPoint.transform.RotateAround(playerCam.transform.position, Vector3.left, (recoilRandVert * recoilMult));
            aimPoint.transform.RotateAround(playerCam.transform.position, Vector3.up, (recoilRandHori * recoilMult));
            shotTime = Time.time;
        }
        if ((Input.GetKeyDown(KeyCode.R) && ammoCount != maxAmmo) || Input.GetMouseButton(0) && ammoCount == 0)
        {
            reloadStart = Time.time;
            reloading = true;
        }
        else if (reloadStart + reloadTime < Time.time && reloading == true)
        {
            ammoCount = maxAmmo;
            reloading = false;
        }
        if (reloading == false)
        {
            ammoCountTxt.text = ammoCount.ToString();
        }
        else if (reloading == true)
        {
            ammoCountTxt.text = ("Reloading");
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
        float recoilDis = Vector3.Distance(aimPoint.transform.position, aimPointOrigin.transform.position);
        float recoilAng = Vector3.Angle(aimPoint.transform.eulerAngles, aimPointOrigin.transform.eulerAngles);
        aimPoint.transform.position = Vector3.Lerp(aimPoint.transform.position, aimPointOrigin.transform.position, 1 * Time.deltaTime);
        float x = Mathf.LerpAngle(aimPoint.transform.eulerAngles.x, aimPointOrigin.transform.eulerAngles.x, 1 * Time.deltaTime);
        float y = Mathf.LerpAngle(aimPoint.transform.eulerAngles.y, aimPointOrigin.transform.eulerAngles.y, 1 * Time.deltaTime);
        float z = Mathf.LerpAngle(aimPoint.transform.eulerAngles.z, aimPointOrigin.transform.eulerAngles.z, 1 * Time.deltaTime);
        Vector3 targetAngle = new Vector3(x, y, z);
        aimPoint.transform.eulerAngles = targetAngle;
        weapon.transform.rotation = aimPoint.transform.rotation;
    }
}

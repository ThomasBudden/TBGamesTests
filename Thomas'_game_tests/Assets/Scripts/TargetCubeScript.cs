using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetCubeScript : MonoBehaviour
{
    public TMP_Text damageTxt;
    public TMP_Text dpsTxt;
    public List<float> dps = new List<float>();
    private Vector3 startPos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, startPos, speed * Time.deltaTime);
    }
}

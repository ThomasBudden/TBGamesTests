using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float moveSpeed;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * (moveSpeed), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime + 30 < Time.time)
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float moveSpeed;
    private float startTime;
    public float damage;
    public string ignore;
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
        if (startTime + 10 < Time.time)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != ignore && other.gameObject.GetComponent<HealthManager>() != null)
        {
            other.gameObject.GetComponent<HealthManager>().health = other.gameObject.GetComponent<HealthManager>().health - damage;
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag != ignore)
        {
            Destroy(this.gameObject);
        }
    }
}

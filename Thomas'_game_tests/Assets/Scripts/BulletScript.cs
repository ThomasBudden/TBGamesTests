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
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<HealthManager>() != null && collision.gameObject.tag != ignore)
        {
            collision.gameObject.GetComponent<HealthManager>().health = collision.gameObject.GetComponent<HealthManager>().health - damage;
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag != ignore)
        {
            Destroy(this.gameObject);
        }
    }
}

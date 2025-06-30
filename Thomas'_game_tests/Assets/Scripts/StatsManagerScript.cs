using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManagerScript : MonoBehaviour
{
    public GameObject shopManager;
    public int currentCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCard = shopManager.GetComponent<ShopScript>().currentCard;
    }
}

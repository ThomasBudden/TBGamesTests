using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject[] shopSlot = new GameObject[3]; //child 0 is name, child 1 is stats
    public string[] names = new string[5];
    public string[] stats = new string[5];
    private bool[] cardUsed = new bool[5];
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RollTheShop();
        }
    }
    public void RollTheShop()
    {
        for (int i = 0; i< shopSlot.Length; i++)
        {
            int randCard = Random.Range(0, stats.Length);
            if (cardUsed[randCard] == false)
            {
                shopSlot[i].transform.GetChild(0).GetComponent<TMP_Text>().text = names[randCard];
                shopSlot[i].transform.GetChild(1).GetComponent<TMP_Text>().text = stats[randCard];
                cardUsed[randCard] = true;
            }
            else if (cardUsed[randCard] == true)
            {
                i -= 1;
            }
        }
    }
}

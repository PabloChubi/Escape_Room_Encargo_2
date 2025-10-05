using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public string[] itemNames;


    void Start()
    {
        itemNames = new string[3];

        itemNames[0] = null;
        itemNames[1] = null;
        itemNames[2] = null;

    }


    void Update()
    {
        
    }


    public void AddItem(string ItemNameToAdd)
    {
       for (int i = 0; i < itemNames.Length; i++)
       {
        if (itemNames[i] == null)
        {
            itemNames[i] = ItemNameToAdd;
            break;
        }
       }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MA3_Item>())
        {
            AddItem(other.GetComponent<MA3_Item>().ItemName);
            Destroy(other.gameObject);
        }
    }
}

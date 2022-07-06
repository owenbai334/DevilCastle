using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Charator")
        {
            InventoryManager.AddNewItem(thisItem);
            Destroy(this.gameObject);           
        }
    }
}

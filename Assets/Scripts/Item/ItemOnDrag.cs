using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public Inventory bag;
    int currentId;
    public static int ID;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentId = originalParent.GetComponent<Slot>().slotId;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject.name !=null)
        {
            if(eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
        {

            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position; 

            var temp = bag.itemList[currentId] ;
            bag.itemList[currentId] = bag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId];
            bag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = temp;
            
            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        if(eventData.pointerCurrentRaycast.gameObject.name =="OtherSlot(Clone)"||eventData.pointerCurrentRaycast.gameObject.name =="EquipSlot(Clone)"||eventData.pointerCurrentRaycast.gameObject.name =="WeaponSlot(Clone)"||eventData.pointerCurrentRaycast.gameObject.name =="PotionSlot(Clone)")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

            bag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = bag.itemList[currentId];
            if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId != currentId)
            {
                bag.itemList[currentId] = null;
            }
            for(int i=0;i<4;i++)
            {
                InventoryManager.Id = i;
                InventoryManager.Refresh();               
            }
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        }
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;   
    }
}

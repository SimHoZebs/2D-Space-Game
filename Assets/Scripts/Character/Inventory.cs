using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Inventory : NetworkBehaviour
{
    [SerializeField] private int inventorySize = 10;
    [SerializeField] public GameObject currItem;
    [SerializeField] public int currSlot = 1;
    [SerializeField] private List<GameObject> inventoryList;

    [Header("Debugging")]
    [SerializeField] public GameObject defaultGun;
    [SerializeField] private Vector3 heldItemOffset;

    private KeyCode[] numSlots = {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5};

    [Client]
    private void Start() {
        if (!isLocalPlayer){ return;}

        inventoryList = new List<GameObject>();
        for (int i = 0; i < inventorySize; i++){
            inventoryList.Add(null);
        }

        AddItem(defaultGun);
    }


    [Client]
    private void Update() {
        if (!isLocalPlayer){ return;}

        for (int slotNum = 0; slotNum < numSlots.Length; slotNum++){
            if (Input.GetKeyDown(numSlots[slotNum]) && currSlot != slotNum){
                UpdateCurrItem(slotNum);
            }
        }

        if (Input.GetButtonDown("UseItem")){
            PressUseItem();
        }

        if (Input.GetButton("UseItem")){
            HeldUseItem();
        }
    }

    private void UpdateCurrItem(int newSlotNum){
        Debug.Log("Updating current item display");
        currSlot = newSlotNum;
        var newItem = inventoryList[newSlotNum];

        if (currItem != null){
            Destroy(currItem);
        }
        currItem = newItem != null
            ? Instantiate(newItem, transform.position + heldItemOffset, transform.rotation, transform)
            : null;
    }

    public void HeldUseItem(){
        currItem.GetComponent<Item>().HeldUseItem();
    }

    public void PressUseItem(){
        currItem.GetComponent<Item>().PressUseItem();
    }

    public void AddItem(GameObject item){
        Debug.Log("Add item called");
        for (int slotNum = 0; slotNum < inventorySize; slotNum++){
            if (inventoryList[slotNum] != null){ continue;}
            inventoryList[slotNum] = item;

            if (currSlot == slotNum){ UpdateCurrItem(slotNum);}
            break;
        }
    }

    public void RemoveItem(Item item){

    }

    public void RemoveItem(int slotNum){

    }

}

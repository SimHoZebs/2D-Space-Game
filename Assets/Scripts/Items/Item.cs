using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Item : MonoBehaviour
{
    [SerializeField] protected bool isStackable = true;
    [SerializeField] protected int stackCount;

    public void PressUseItem(){

        PressUseItemInternal();
    }

    public void HeldUseItem(){
        HoldUseItemInternal();

    }

    protected virtual void PressUseItemInternal(){ }
    protected virtual void HoldUseItemInternal(){ }
}



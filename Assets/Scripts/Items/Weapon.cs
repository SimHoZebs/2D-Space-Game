using UnityEngine;

public class Weapon: Item
{
    private void Start() {
        isStackable = false;
        StartInternal();
    }

    protected virtual void StartInternal(){

    }
}
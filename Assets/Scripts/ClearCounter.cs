using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectsSO kitchenObjectSO;


    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // there is no kitchenObject here
            if (player.HasKitchenObject()) {
                // player is carrying sth
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // player has nothing
            }
        } else {
            // there is kitchenObject here
            if (player.HasKitchenObject()) {
                // player is carrying sth
            } else {
                // player is carrying nothing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // player is not carrying plate but sth else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectsSO())) {
                           player.GetKitchenObject().DestroySelf(); 
                        }
                    }
                }
            } else {
                // player is carrying nothing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
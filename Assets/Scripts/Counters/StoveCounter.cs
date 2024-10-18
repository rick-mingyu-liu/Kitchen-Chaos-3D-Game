using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter {
    [SerializeField] private FryingRecipeSO[] flyingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) {
            // there is no kitchenObject here
            if (player.HasKitchenObject()) {
                // player is carrying sth
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO())) {
                    // player is carrying sth that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
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

    
    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO) {
            return fryingRecipeSO.output;
        } else {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectsSO) {
        foreach (FryingRecipeSO fryingRecipeSO in flyingRecipeSOArray) {
            if (fryingRecipeSO.input == inputKitchenObjectsSO) {
                return fryingRecipeSO;
            }
        }

        return null;
    }

}

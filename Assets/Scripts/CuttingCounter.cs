using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) {
            // there is no kitchenObject here
            if (player.HasKitchenObject()) {
                // player is carrying sth
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO())) {
                    // player is carrying sth that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectsSO())) {
            // there is a KitchenObject here AND it can be cut
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
                KitchenObjectsSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectsSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO) {
            return cuttingRecipeSO.output;
        } else {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectsSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectsSO) {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}

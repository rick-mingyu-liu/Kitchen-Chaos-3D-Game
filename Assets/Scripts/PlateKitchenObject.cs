using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    [SerializeField] private List<KitchenObjectsSO> validKitchenObjectSOList;

    private List<KitchenObjectsSO> kitchenObjectsSOList;

    private void Awake() {
        kitchenObjectsSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO) {
        if (!validKitchenObjectSOList.Contains(kitchenObjectsSO)) {
            // not a valid ingredient
            return false;
        }
        if (kitchenObjectsSOList.Contains(kitchenObjectsSO)) {
            // already has this type
            return false;
        } else {
            kitchenObjectsSOList.Add(kitchenObjectsSO);
            return true;
        }
        
    }
}

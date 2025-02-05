using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipeAmount;


    private void Awake() {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipesMax > waitingRecipeSOList.Count) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; ++i) {
            RecipeSO waitingRecipeSO  = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectsSOList.Count == plateKitchenObject.GetKitchenObjectsSOList().Count) {
                // has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach(KitchenObjectsSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectsSOList) {
                    // cycling through all the ingredients in the recipe
                    bool ingredientFound = false;
                    foreach(KitchenObjectsSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectsSOList()) {
                        // cycle through all the ingredients on the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            // ingredient does match
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // this recipe ingredient is not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe) {
                    // player deliver the correct recipe
                    successfulRecipeAmount++;
                    
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // no matches found 
        // player did not delivery a correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount() {
        return successfulRecipeAmount;
    }

}

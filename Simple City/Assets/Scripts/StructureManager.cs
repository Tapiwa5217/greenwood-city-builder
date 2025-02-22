using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using SVS;

public class StructureManager : MonoBehaviour
{
   public StructurePrefabWeighted[] housePrefabs, specialPrefabs;
   public PlacementManager placementManager;

   private float[] houseWeights, specialWeights;

   private void Start() {
    houseWeights = housePrefabs.Select(prefabStats => prefabStats.weight).ToArray();
    specialWeights = specialPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
   }

   public void PlaceHouse(Vector3Int position) {
        if(CheckPositionBeforePlacement(position)) {
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, housePrefabs[randomIndex].prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
   }

   public void PlaceSpecial(Vector3Int position) {
        if(CheckPositionBeforePlacement(position)) {
            int randomIndex = GetRandomWeightedIndex(specialWeights);
            placementManager.PlaceObjectOnTheMap(position, specialPrefabs[randomIndex].prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
   }

   private int GetRandomWeightedIndex(float[] weights) {
        float sum = 0f;
        for(int i=0; i < weights.Length; i++) {
            sum += weights[i];
        }

        float randomValue = UnityEngine.Random.Range(0, sum);
        float tempSum = 0;

        for (int i=0; i < weights.Length; i++) {
            if(randomValue >= tempSum && randomValue < tempSum + weights[i]) {
                return i;
            }
            tempSum += weights[i];
        }

        return 0;
   }

   private bool CheckPositionBeforePlacement(Vector3Int position) {
        if(placementManager.CheckIfPositionInBound(position) == false) {
            Debug.Log("This position is out of bounds");
            return false;
        }
        if(placementManager.CheckIfPositionIsFree(position) == false) {
            Debug.Log("This position is already taken");
            return false;
        }
        if(placementManager.GetNeighboursOfTypeFor(position,CellType.Road).Count <= 0) {
            Debug.Log("Must be placed near a road");
            return false;
        }
        return true;
   }

}

[Serializable]

public struct StructurePrefabWeighted {
    public GameObject prefab;
    [Range(0,1)]
    public float weight;
}

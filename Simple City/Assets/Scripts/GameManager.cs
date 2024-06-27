using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVS;

public class GameManager : MonoBehaviour
{
   public CameraMovement cameraMovement;
   public RoadManager roadManager;
   public InputManager inputManager;

   public UIController uiController;

   public StructureManager structureManager;

   private void Start() {
      uiController.OnRoadPlacement += RoadPlacementHandler;
      uiController.OnHousePlacement += HousePlacementHandler;
      uiController.OnSpecialPlacement += SpecialPlacementHandler;
   }

   private void Update() {
      cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x,0,inputManager.CameraMovementVector.y));
   }

   private void RoadPlacementHandler() {
      clearInputActions();

      inputManager.OnMouseClick += roadManager.PlaceRoad;
      inputManager.OnMouseHold += roadManager.PlaceRoad;
      inputManager.OnMouseUp += roadManager.FinishPlacingRoad; 
   }

   private void SpecialPlacementHandler() {
      clearInputActions();
      inputManager.OnMouseClick += structureManager.PlaceSpecial;
   }

   private void HousePlacementHandler() {
      clearInputActions();
      inputManager.OnMouseClick += structureManager.PlaceHouse;
   }

   private void clearInputActions() {
      inputManager.OnMouseClick = null;
      inputManager.OnMouseHold = null;
      inputManager.OnMouseUp = null;
   }
}

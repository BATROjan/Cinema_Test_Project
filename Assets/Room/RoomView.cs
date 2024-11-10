using System;
using System.Collections;
using System.Collections.Generic;
using Room;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomView : MonoBehaviour
{
    [SerializeField] private WallView frontWall; 
    [SerializeField] private WallView backWall; 
    [SerializeField] private WallView leftWall; 
    [FormerlySerializedAs("rightWalls")] [SerializeField] private WallView rightWall;
    [SerializeField] private WallView floorWalls;
    [SerializeField] private UIWindow uiWindow;

    [SerializeField] private float wallsWinght = 0.2f;
    [SerializeField] private float wallsHeight = 3f;

    private float _currentLenght;
    private float _currentWinght;
    private float _lenghtWallsPosition;
    private float _winghtWallsPosition;
    private float _ofset;
    void Start()
    {
        uiWindow.ChangeRoomSizeUIButton.OnClick += ChangeRoomSize ;
    }

    private void ChangeRoomSize()
    {
        CalculateData();
        leftWall.WallTransform.localScale = 
            new Vector3(wallsWinght,wallsHeight,_currentLenght);
        leftWall.WallTransform.position = new Vector3(-_lenghtWallsPosition,0, 0); 
        
        rightWall.WallTransform.localScale = 
            new Vector3(wallsWinght,wallsHeight,_currentLenght);
        rightWall.WallTransform.position = new Vector3(_lenghtWallsPosition,0, 0);
      
        frontWall.WallTransform.localScale = 
            new Vector3(_currentWinght,wallsHeight,wallsWinght);
        frontWall.WallTransform.position = new Vector3( 0, 0,-_winghtWallsPosition);  
            
        backWall.WallTransform.localScale = 
            new Vector3(_currentWinght,wallsHeight,wallsWinght);
        backWall.WallTransform.position = new Vector3( 0, 0,_winghtWallsPosition);
        
        floorWalls.WallTransform.localScale = new Vector3(_currentWinght, wallsWinght, _currentLenght);
    }

    private void CalculateData()
    {
        _ofset = wallsWinght / 2;
        
        _lenghtWallsPosition = float.Parse(uiWindow.RoomWinghtField.text) / 2;
        _winghtWallsPosition = float.Parse(uiWindow.RoomLenghtField.text) / 2;
        
        _currentLenght = float.Parse(uiWindow.RoomLenghtField.text);
        _currentWinght = float.Parse(uiWindow.RoomWinghtField.text);
    }
}
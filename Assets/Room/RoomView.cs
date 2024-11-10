using System;
using System.Collections;
using System.Collections.Generic;
using Room;
using Screen;
using Sound;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomView : MonoBehaviour
{
    [Header("Room Settings")]
    [SerializeField] private WallView frontWall; 
    [SerializeField] private WallView backWall; 
    [SerializeField] private WallView leftWall; 
    [SerializeField] private WallView rightWall;
    [SerializeField] private WallView floorWalls;
    
    [SerializeField] private float wallsWinght = 0.2f;
    [SerializeField] private float wallsHeight = 3f;
    
    [Header("Screen Settings")]
    [SerializeField] private ScreenView screenView;
    [SerializeField] private GameObject screenPivot;
    
    [Header("Speakers Settings")]
    [SerializeField] private GameObject[] speakerPivots;
    [SerializeField] private SpeakerView speakerView;
    [SerializeField] private GameObject viewerPivot;
    
    [Header("UI Settings")]
    [SerializeField] private UIWindow uiWindow;

    private List<SpeakerView> _speakerViews = new List<SpeakerView>();
    
    private float _currentLenght;
    private float _currentWinght;
    private float _lenghtWallsPosition;
    private float _winghtWallsPosition;
    private float _ofset;
    void Start()
    {
        uiWindow.ChangeRoomSizeUIButton.OnClick += ChangeRoomSize;
        uiWindow.AddScreen.OnClick += AddScreen;
        uiWindow.AddSound.OnClick += AddSound;
    }

    private void AddSound()
    {
        foreach (var pivot in speakerPivots)
        {
            var view = Instantiate(speakerView);
            view.transform.LookAt(viewerPivot.transform);
            view.transform.position = pivot.transform.position;
            
            _speakerViews.Add(view);
        }
        uiWindow.AddSound.OnClick -= AddSound;
    }

    private void AddScreen()
    {
        ActivatePanel(true);
        uiWindow.AddScreen.OnClick -= AddScreen;
    }

    private void ActivatePanel(bool value)
    {
        uiWindow.AddScreen.gameObject.SetActive(!value);

        foreach (var text in uiWindow.ScreenSizeText)
        {
            text.gameObject.SetActive(value);
        }
        uiWindow.ScreenLenghtField.gameObject.SetActive(value);
        uiWindow.ScreenWinghtField.gameObject.SetActive(value);
        uiWindow.ChangeScreenSizeUIButton.gameObject.SetActive(value);
        
        uiWindow.ChangeScreenSizeUIButton.OnClick += ChangeScreenSize;
        screenView.gameObject.SetActive(value);
    }

    private void ChangeScreenSize()
    {
        screenView.ScreenTranform.localScale =
            new Vector3(float.Parse(uiWindow.ScreenLenghtField.text),
                float.Parse(uiWindow.ScreenWinghtField.text), 0.1f);
    }

    private void ChangeRoomSize()
    {
        CalculateData();
        
        ChangeLenghtRoom(leftWall, -1);
        ChangeLenghtRoom(rightWall, 1);
        ChangeWindhtRoom(backWall, 1);
        ChangeWindhtRoom(frontWall, -1);
        
        if (_speakerViews.Count > 0)
        {
            for (int i = 0; i < _speakerViews.Count; i++)
            {
                _speakerViews[i].transform.position = speakerPivots[i].transform.position;
                _speakerViews[i].transform.LookAt(viewerPivot.transform);
            } 
        }
        screenView.ScreenTranform.position = screenPivot.transform.position;
        floorWalls.WallTransform.localScale = new Vector3(_currentWinght, wallsWinght, _currentLenght);
    }

    private void ChangeWindhtRoom(WallView wallView, int value)
    {
        wallView.WallTransform.localScale = 
            new Vector3(_currentWinght,wallsHeight,wallsWinght);
        wallView.WallTransform.position = new Vector3( 0, 0,value * _winghtWallsPosition);
    }

    private void ChangeLenghtRoom(WallView wallView, int value)
    {
        wallView.WallTransform.localScale = 
            new Vector3(wallsWinght,wallsHeight,_currentLenght);
        wallView.WallTransform.position = new Vector3(value * _lenghtWallsPosition,0, 0);
    }

    private void CalculateData()
    {
        _ofset = wallsWinght / 2;
        
        _lenghtWallsPosition = float.Parse(uiWindow.RoomWinghtField.text) / 2 + _ofset;
        _winghtWallsPosition = float.Parse(uiWindow.RoomLenghtField.text) / 2 + _ofset;
        
        _currentLenght = float.Parse(uiWindow.RoomLenghtField.text);
        _currentWinght = float.Parse(uiWindow.RoomWinghtField.text);
    }
}
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIWindow : MonoBehaviour
{
    public InputField RoomLenghtField;
    public InputField RoomWinghtField;  
    public InputField RoomHeightField;
    public UIButton ChangeRoomSizeUIButton;
    
    public InputField ScreenLenghtField;
    public InputField ScreenWinghtField;
    public Text[] ScreenSizeText;
    public UIButton ChangeScreenSizeUIButton;

    public UIButton AddScreen;
    public UIButton AddSound;
    public UIButton AddLight;
    public UIButton RemoveButton;
}
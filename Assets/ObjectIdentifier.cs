using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectIdentifier : MonoBehaviour
{
    public Camera mainCamera;
    
    public ControllElement CurrentElement;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPoint = Input.mousePosition;
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                var element = hit.collider.GetComponent<ControllElement>();
                if (element)
                {
                    Debug.Log(element.name);
                    CurrentElement = element;
                }
            }
        }
    }
}
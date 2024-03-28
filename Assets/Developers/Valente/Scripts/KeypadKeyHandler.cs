using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KeypadKeyHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction<int> KeyPressed;

    [SerializeField] private int _keyNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        KeyPressed.Invoke(_keyNumber);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeypadKeyHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private string _keyNumber;

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
        if (SudokuManager.Instance.SelectedSlot)
            SudokuManager.Instance.SelectedSlot.SlotText.text = _keyNumber;
    }
}

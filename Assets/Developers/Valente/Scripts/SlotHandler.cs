using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SlotHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image SlotImage { get => _slotImage; set => _slotImage = value; }
    public TextMeshProUGUI SlotText { get => _slotText; set => _slotText = value; }
    public bool SlotLocked { get => _slotLocked; set => _slotLocked = LockSlot(value); }
    public bool Duplicated { get => _duplicated; set => _duplicated = value; }
    public int SlotNumber { get => _slotNumber; set => _slotNumber = ChangeSlotNumber(value); }
    public UnityAction<SlotHandler> SlotPressed;

    [SerializeField] private bool _slotLocked = false;

    private Image _slotImage;
    private TextMeshProUGUI _slotText;
    private int _slotNumber;
    private bool _duplicated = false;

    private void Awake() {
        SlotImage = GetComponent<Image>();
        SlotText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SlotPressed.Invoke(this);
    }

    private bool LockSlot(bool newValue) {
        if (newValue)
            _slotImage.color = new Color(0.7803922f, 0.3960784f, 0.1098039f, 0.35f); // Orange Color but more transparent than the normal one
        else
            ResetColor();

        return newValue;
    }

    private int ChangeSlotNumber(int newValue) {
        if (newValue == 0)
            _slotText.text = "";
        else
            _slotText.text = newValue.ToString();

        return newValue;
    }

    public void RevealSlot() {
        if (_slotLocked) return;

        _slotLocked = true;
        
        if (_duplicated)
            _slotImage.color = new Color(0.7803922f, 0.206578f, 0.1098039f, 0.35f); // Red Color
        else
            _slotImage.color = new Color(0.5102855f, 0.7803922f, 0.1098039f, 0.35f); // Green Color 
        
    }

    public void RestartSlot() {
        SlotNumber = 0;
        SlotLocked = false;
    }

    public void SelectSlot() {
        _slotImage.color = new Color(0.7803922f, 0.3960784f, 0.1098039f, 0.7843137f); // Orange Color
    }

    public void ResetColor() {
        _slotImage.color = new Color(0.7803922f, 0.3960784f, 0.1098039f, 0.003921569f); // Greyish Color
    }

}

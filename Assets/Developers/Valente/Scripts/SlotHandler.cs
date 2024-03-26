using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SlotHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image SlotImage { get => _slotImage; set => _slotImage = value; }
    public TextMeshProUGUI SlotText { get => _slotText; set => _slotText = value; }
    public bool SlotLocked { get => _slotLocked; set => _slotLocked = LockSlot(value); }
    public bool CorrectNumber { get => _correctNumber; set => _correctNumber = value; }

    [SerializeField] private bool _slotLocked = false;

    private Image _slotImage;
    private TextMeshProUGUI _slotText;
    private bool _correctNumber = true;


    private void Start() {
        SlotImage = GetComponent<Image>();
        SlotText = GetComponentInChildren<TextMeshProUGUI>();

        SudokuManager.Instance.GameFinished += RevealSlot;
        SudokuManager.Instance.GameRestarting += RestartSlot;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (SudokuManager.Instance.SelectedSlot)
            SudokuManager.Instance.SelectedSlot.SlotImage.color = new Color(0.7803922f, 0.3960784f, 0.1098039f, 0.003921569f);

        if (_slotLocked) {
            SudokuManager.Instance.SelectedSlot = null;
            return;
        }

        _slotImage.color = new Color(0.7803922f, 0.3960784f, 0.1098039f, 0.7843137f);

        SudokuManager.Instance.SelectedSlot = this;
    }

    private bool LockSlot(bool newValue) {
        if (newValue)
            _slotImage.color = new Color(0.7803922f, 0.3960784f, 0.1098039f, 0.35f);
        else
            _slotImage.color = new Color(0.7803922f, 0.3960784f, 0.1098039f, 0.003921569f);

        return newValue;
    }

    private void RevealSlot() {
        if (_slotLocked) return;

        _slotLocked = true;
        
        if (_correctNumber)
            _slotImage.color = new Color(0.5102855f, 0.7803922f, 0.1098039f, 0.35f);
        else
            _slotImage.color = new Color(0.7803922f, 0.206578f, 0.1098039f, 0.35f);
        
    }

    private void RestartSlot() {
        _slotText.text = "";
        SlotLocked = false;
    }

}

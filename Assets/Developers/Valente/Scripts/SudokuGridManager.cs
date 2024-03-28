using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGridManager : MonoBehaviour
{
    public SlotHandler[] Slots { get => _slots; set => _slots = value; }

    [Header("Prefabs")]
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _splitPrefab;

    [Header("Measures")]
    [SerializeField] private float _spacing;
    [SerializeField] private float _border;

    private SlotHandler[] _slots;
    private RectTransform _rect;

    private void Start() {
        _rect = GetComponent<RectTransform>();
    }

    public void CreateGrid(int gridRows, int gridColumns, int squareRows, int squareColumns) {
        DeleteAllSlots();
        
        _rect.offsetMin = new Vector2(_border, _border);
        _rect.offsetMax = new Vector2(-_border, -_border);
        
        _slots = new SlotHandler[gridRows * gridColumns];
        CreateSlots(gridRows, gridColumns, squareRows, squareColumns);
        CreateSplits(gridRows, gridColumns, squareRows, squareColumns);
    }


    public void CreateSlots(int gridRows, int gridColumns, int squareRows, int squareColumns) {
        float slotSize = (_rect.rect.width - _spacing * (gridColumns - 1)) / gridColumns;

        for (int r = 0; r < gridRows; r++) {
            float y = r * slotSize + r * _spacing;
            for (int c = 0; c < gridColumns; c++) {
                float x = c * slotSize + c * _spacing;

                _slots[gridColumns * r + c] = CreateGridObject(_slotPrefab, slotSize, slotSize, x, -y).GetComponentInChildren<SlotHandler>();
            }
        }
    }

    public void CreateSplits(int gridRows, int gridColumns, int squareRows, int squareColumns) {
        float slotSize = (_rect.rect.width - _spacing * (gridColumns - 1)) / gridColumns;

        for (int r = 1; r < gridRows / squareRows; r++) {
            float y = r * (slotSize * squareRows) + r * (_spacing * squareRows) - _spacing;

            CreateGridObject(_splitPrefab, _rect.rect.width + _border * 2, _spacing, -_border, -y);
        }

        for (int c = 1; c < gridColumns / squareColumns; c++) {
            float x = c * (slotSize * squareColumns) + c * (_spacing * squareColumns) - _spacing;

            CreateGridObject(_splitPrefab, _spacing, _rect.rect.height + _border * 2, x, _border);
        }
    }

    private GameObject CreateGridObject(GameObject prefab, float width, float height, float x, float y) {
        GameObject newSlot = Instantiate(prefab, transform);
        RectTransform newSlotRect = newSlot.GetComponent<RectTransform>();

        newSlotRect.anchorMin = Vector2.up;
        newSlotRect.anchorMax = Vector2.up;
        newSlotRect.pivot = Vector2.up;
        
        newSlotRect.anchoredPosition = new Vector2(x, y);

        newSlotRect.sizeDelta = new Vector2(width, height);

        return newSlot;
    }

    private void DeleteAllSlots() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}

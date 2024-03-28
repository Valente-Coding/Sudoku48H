using UnityEngine;
using UnityEngine.Events;

public class SudokuManager : MonoBehaviour
{
    public UnityAction GameFinished;
    public UnityAction GameRestarting;

    [Header("Sudoku Modes")]
    [Tooltip("A list of Sudoku Variants.")]
    [SerializeField] private SudokuVariant[] _variants;

    [Header("Grid Configs")]
    [Tooltip("The grid that will contain all the slots")]
    [SerializeField] private SudokuGridManager _gridManager;


    [Header("Others")]
    [Tooltip("A list of keys from a numerical keypad.")]
    [SerializeField] private KeypadKeyHandler[] _keypadKeys;


    private SlotHandler _selectedSlot = null;
    private int _currentVariant = 0;
    private int _gridColumns = 9;
    private int _gridRows = 9;
    private int _squareColumns = 3;
    private int _squareRows = 3;

    
    private void Start() {
        AssignActionToKeypadKeys();

        StartSudoku();
    }

    private void StartSudoku() {
        LoadSudokuVariantData();
        _gridManager.CreateGrid(_gridRows, _gridColumns, _squareRows, _squareColumns);
        ClearSlotsActions();
        AssignSlotsToActions();

        EmptySlots();
        DisplayRandomSLots(_variants[_currentVariant].InicialHints);
    }

    private void LoadSudokuVariantData() {
        _gridColumns = _variants[_currentVariant].GridColumns;
        _gridRows = _variants[_currentVariant].GridRows;
        _squareColumns = _variants[_currentVariant].SquareColumns;
        _squareRows = _variants[_currentVariant].SquareRows;

        DisplayKeypadKeys(_gridColumns > _gridRows ? _gridColumns : _gridRows);
    }

    private void DisplayRandomSLots(int amount) {
        int[] boardNumbers = GetBoardNumbers();
        int maxNumberAllowed = _squareRows * _squareColumns + 1;

        for (int i = 0; i < amount; i++) {
            int randomIndex = Random.Range(0, _gridManager.Slots.Length);
            int randomNumber = Random.Range(1, maxNumberAllowed);
            
            if (_gridManager.Slots[randomIndex].SlotLocked) {
                i--;
                continue;
            }

            boardNumbers[randomIndex] = randomNumber;
            
            while (IsNumberDuplicate(boardNumbers, randomIndex)) {
                randomNumber = Random.Range(1, maxNumberAllowed);
                boardNumbers[randomIndex] = randomNumber;
            }
            
            
            // Reveal and lock the slot
            _gridManager.Slots[randomIndex].SlotNumber = boardNumbers[randomIndex];
            _gridManager.Slots[randomIndex].SlotLocked = true;
        }
    }

    public void RestartBoard() {
        GameRestarting.Invoke();

        EmptySlots();
        DisplayRandomSLots(_variants[_currentVariant].InicialHints);
    }

    public void NextMode() {
        _currentVariant++;
        _currentVariant = _currentVariant >= _variants.Length ? 0 : _currentVariant;
        
        StartSudoku();
    }

    public void FinishBoard() {
        _selectedSlot = null;
        CheckForDuplicates();
        GameFinished.Invoke();
    }

    private void CheckForDuplicates() {
        int[] boardNumbers = GetBoardNumbers();
        
        for (int i = 0; i < boardNumbers.Length; i++) {
            if (IsNumberDuplicate(boardNumbers, i)) {
                _gridManager.Slots[i].Duplicated = true;
            }
        }
    }

    private void EmptySlots() {
        foreach (SlotHandler slot in _gridManager.Slots) {
            slot.Duplicated = false;
            slot.SlotNumber = 0;
            slot.SlotLocked = false;
        }
    }

    private bool IsNumberDuplicate(int[] numbers, int index) {
        int r = index / _gridRows; // 31 / 9 = 3 || 9 / 6 = 1
        int c = index % _gridColumns; // 31 % 9 = 4 || 9 % 6 = 3
        int square = _squareRows * (r/_squareRows) + c / _squareColumns;

        if (IsNumberInVector(GetNumbersInSquare(numbers, square, index), numbers[index]) ||
            IsNumberInVector(GetHorizontalNumbers(numbers, r, index), numbers[index]) ||
            IsNumberInVector(GetVerticalNumbers(numbers, c, index), numbers[index])) {
            return true;
        }

        return false;
    }


    private int[] GetNumbersInSquare(int[] numbers, int square, int skip) {
        int[] squares = new int[_squareColumns * _squareRows];

        for (int r = 0; r < _squareRows; r++) {
            for (int c = 0; c < _squareColumns; c++) {
                int currentIndex = _gridColumns * r + _squareColumns * (square % _squareRows) + ((_squareRows * _squareColumns) * (_gridColumns / _squareColumns)) * (square / _squareRows) + c;

                if (currentIndex == skip) continue;

                squares[_squareColumns*r + c] = numbers[currentIndex];
            }
        }

        return squares;
    }

    private int[] GetHorizontalNumbers(int[] numbers, int r, int skip) {
        int [] v = new int[_gridColumns];

        for (int c = 0; c < _gridColumns; c++) {
            int currentIndex = _gridColumns*r + c;

            if (currentIndex == skip) continue;

            v[c] = numbers[currentIndex];
        }

        return v;
    }

    private int[] GetVerticalNumbers(int[] numbers, int c, int skip) {
        int [] v = new int[_gridRows];

        for (int r = 0; r < _gridRows; r++) {
            int currentIndex = _gridColumns*r + c;

            if (currentIndex == skip) continue;

            v[r] = numbers[currentIndex];
        }

        return v;
    }

    private bool IsNumberInVector(int[] v, int number) {
        int left = 0, middle, right = v.Length - 1;

        System.Array.Sort(v);
    
        while (left <= right) {
            middle = (int)((left + right) / 2);
            
            if (v[middle] < number)
                left = middle + 1;

            if (v[middle] > number)
                right = middle - 1;
            
            if (v[middle] == number)
                return true;   
        }

        return false;
    }

    private int[] GetBoardNumbers() {
        int[] v = new int[_gridManager.Slots.Length];

        for (int i = 0; i < _gridManager.Slots.Length; i++) {
            v[i] = _gridManager.Slots[i].SlotNumber;
        }

        return v;
    }

    private void AssignSlotsToActions() {
        for (int i = 0; i < _gridManager.Slots.Length; i++) {
            GameRestarting += _gridManager.Slots[i].RestartSlot;
            GameFinished += _gridManager.Slots[i].RevealSlot;
            _gridManager.Slots[i].SlotPressed += OnSlotPressed;
        }
    }

    private void ClearSlotsActions() {
        GameRestarting = null;
        GameFinished = null;
    }

    private void OnSlotPressed(SlotHandler slot) {
        if (_selectedSlot) {
            _selectedSlot.ResetColor();
            _selectedSlot = null;
        }

        if (slot.SlotLocked) return;

        slot.SelectSlot();
        _selectedSlot = slot;
    }

    private void AssignActionToKeypadKeys() {
        for (int i = 0; i < _keypadKeys.Length; i++) {
            _keypadKeys[i].KeyPressed += OnKeypadKeyPressed;
        }
    }

    private void OnKeypadKeyPressed(int number) {
        if (_selectedSlot)
            _selectedSlot.SlotNumber = number;
    }

    private void DisplayKeypadKeys(int hideAfter) {
        for (int i = 0; i < _keypadKeys.Length; i++) {
            _keypadKeys[i].gameObject.SetActive(i < hideAfter ? true : false);
        }
    }
}

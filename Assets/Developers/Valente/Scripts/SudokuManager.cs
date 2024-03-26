using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class SudokuManager : MonoBehaviour
{
    public static SudokuManager Instance { get; private set; }
    public SlotHandler SelectedSlot { get => _selectedSlot; set => _selectedSlot = value; }
    public UnityAction GameFinished;
    public UnityAction GameRestarting;

    [SerializeField] private Transform _panel;
    private SlotHandler _selectedSlot = null;
    private SlotHandler[] _slots;

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    
    private void Start() {
        _slots = _panel.GetComponentsInChildren<SlotHandler>();

        EmptySlots();
        DisplayRandomSLots(8);
    }

    private void DisplayRandomSLots(int amount) {
        int[] boardNumbers = GetBoardNumbers();

        for (int i = 0; i < amount; i++) {
            int randomIndex = UnityEngine.Random.Range(0, _slots.Length);
            int randomNumber = UnityEngine.Random.Range(1, 10);

            boardNumbers[randomIndex] = randomNumber;
            
            while (IsNumberDuplicate(boardNumbers, randomIndex)) {
                randomNumber = UnityEngine.Random.Range(1, 10);
                boardNumbers[randomIndex] = randomNumber;
            }
            
            
            // Reveal and lock the slot
            _slots[randomIndex].SlotText.text = boardNumbers[randomIndex].ToString();
            _slots[randomIndex].SlotLocked = true;
        }
    }

    public void RestartBoard() {
        GameRestarting.Invoke();

        EmptySlots();
        DisplayRandomSLots(8);
    }

    public void TestBoard() {
        
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
                _slots[i].CorrectNumber = false;
            }
        }
    }

    private void EmptySlots() {
        foreach (SlotHandler slot in _slots) {
            slot.CorrectNumber = true;
            slot.SlotText.text = "";
            slot.SlotLocked = false;
        }
    }

    private bool IsNumberDuplicate(int[] numbers, int index) {
        int r = (int)math.floor(index / 9); // 33 / 9 = 3
        int c = (int)math.floor(index % 9); // 33 % 9 = 6
        int square = 3*(r/3) + c/3;

        if (IsNumberInVector(GetNumbersInSquare(numbers, square, index), numbers[index]) ||
            IsNumberInVector(GetHorizontalNumbers(numbers, r, index), numbers[index]) ||
            IsNumberInVector(GetVerticalNumbers(numbers, c, index), numbers[index])) {
            return true;
        }

        return false;
    }

    private int[] GetNumbersInSquare(int[] numbers, int square, int skip) {
        int[] squares = new int[9];

        for (int r = 0; r < 3; r++) {
            for (int c = 0; c < 3; c++) {
                int currentIndex = 9*r + 3*(square%3) + 27*(square/3) + c;

                if (currentIndex == skip) continue;

                squares[3*r + c] = numbers[currentIndex];
            }
        }

        return squares;
    }

    private int[] GetHorizontalNumbers(int[] numbers, int r, int skip) {
        int [] v = new int[9];

        for (int c = 0; c < 9; c++) {
            if (9*r + c == skip) continue;

            v[c] = numbers[9*r + c];
        }

        return v;
    }

    private int[] GetVerticalNumbers(int[] numbers, int c, int skip) {
        int [] v = new int[9];

        for (int r = 0; r < 9; r++) {
            if (9*r + c == skip) continue;

            v[r] = numbers[9*r + c];
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
        int[] v = new int[_slots.Length];

        for (int i = 0; i < _slots.Length; i++) {
            if (_slots[i].SlotText.text == "")
                v[i] = 0;
            else
                v[i] = int.Parse(_slots[i].SlotText.text);
        }

        return v;
    }
}

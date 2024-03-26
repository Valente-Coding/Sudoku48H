using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class SudokuManagerBlabla : MonoBehaviour
{
    /* public static SudokuManagerBlabla Instance { get; private set; }
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

        DisplayRandomSLots(8);
    }

    private void DisplayRandomSLots(int amount) {
        for (int i = 0; i < amount; i++) {
            int r = UnityEngine.Random.Range(0, 9);
            int c = UnityEngine.Random.Range(0, 9);

            // Checking if a number was already revealed
            while (_slots[9 * r + c].SlotLocked) {
                r = UnityEngine.Random.Range(0, 9);
                c = UnityEngine.Random.Range(0, 9);
            }
            
            // Reveal and lock the slot
            _slots[9 * r + c].SlotText.text = _solvedPuzzle[r,c].ToString();
            _slots[9 * r + c].SlotLocked = true;
        }
    }

    private void ShuffleBoard() {
        _solvedPuzzle = new int[9, 9];

        for (int r = 0; r < 9; r++) {
            for (int c = 0; c < 9; c++) {
                _solvedPuzzle[r,c] = GetNumberForSlot(r, c); 
                _slots[9 * r + c].CorrectNumber = _solvedPuzzle[r,c];
            }
        }
    }

    private int GetNumberForSlot(int r, int c) {
        List<int> availableNumbers = new List<int>() {1,2,3,4,5,6,7,8,9};

        for (int i = 1; i <= 9; i++) {  
            if (IsNumberInVector(GetHorizontalVectorFromMatrix(_solvedPuzzle, r, 9), i) || IsNumberInVector(GetVerticalVectorFromMatrix(_solvedPuzzle, c, 9), i))
                availableNumbers.Remove(i);

            
            if (IsNumberInVector(GetVectorFromSquare(_solvedPuzzle, (int)(Math.Floor(r/3f) * 3), (int)(Math.Floor(c/3f) * 3)), i))
                availableNumbers.Remove(i);
        }
        
        Debug.Log(availableNumbers.Count);
        return availableNumbers[UnityEngine.Random.Range(0, availableNumbers.Count)];
    }

    private int[] GetVectorFromSquare(int[,] m, int offsetRow, int offsetColumn) {
        int[] squareNumbers = new int[9];

        for (int r = 0; r < 3; r++) {
            for (int c = 0; c < 3; c++) {
                squareNumbers[3 * r + c] = _solvedPuzzle[offsetRow + r, offsetColumn + c];
            }
        }

        return squareNumbers;
    }

    private int[] GetHorizontalVectorFromMatrix(int[,] m, int r, int limit) {
        int[] v = new int[limit];

        for (int i = 0; i < limit; i++) {
            v[i] = m[r, i];
        }

        return v;
    }

    private int[] GetVerticalVectorFromMatrix(int[,] m, int c, int limit) {
        int[] v = new int[limit];

        for (int i = 0; i < limit; i++) {
            v[i] = m[i, c];
        }

        return v;
    }

    private bool IsNumberInVector(int[] v, int number) {
        int left = 0, middle, right = v.Length - 1;

        Array.Sort(v);
    
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

    public void RestartBoard() {
        GameRestarting.Invoke();
        
        ShuffleBoard(); // Param1: shift amount on X axis; Parent2: shift amount on Y axis;
        DisplayRandomSLots(8);
    }

    public void RevealBoard() {
        
    }

    public void FinishBoard() {
        _selectedSlot = null;
        CheckForDuplicates();

        GameFinished.Invoke();
    }

    private void CheckForDuplicates() {
        int[] boardNumbers = GetBoardNumbers();
        
        for (int i = 0; i < boardNumbers.Length; i++) {
            int r = (int)math.floor(i / 9); // 33 / 9 = 3
            int c = (int)math.floor(i % 9); // 33 % 9 = 6
            int square = 3*(r/3) + c/3;

            if (IsNumberInVector(GetNumbersInSquare(boardNumbers, square), boardNumbers[i]) ||
                IsNumberInVector(GetHorizontalNumbers(boardNumbers, r), boardNumbers[i])) {
                _slots[i].CorrectNumber = false;
                break;
            }
        }
    }

    private int[] GetNumbersInSquare(int[] numbers, int square) {
        int[] squares = new int[9];

        for (int r = 0; r < 3; r++) {
            for (int c = 0; c < 3; c++) {
                squares[3*r + c] = numbers[9*r + 3*square + c];
            }
        }

        return squares;
    }

    private int[] GetHorizontalNumbers(int[] numbers, int r) {
        int [] v = new int[9];

        for (int c = 0; c < 9; c++) {
            v[c] = numbers[9*r + c];
        }

        return v;
    }

    private int[] GetVerticalNumbers(int[] numbers, int c) {
        int [] v = new int[9];

        for (int r = 0; r < 9; r++) {
            v[r] = numbers[9*r + c];
        }

        return v;
    }

    private int[] GetBoardNumbers() {
        int[] v = new int[_slots.Length];

        for (int i = 0; i < _slots.Length; i++) {
            v[i] = int.Parse(_slots[i].SlotText.text);
        }

        return v;
    } */
}

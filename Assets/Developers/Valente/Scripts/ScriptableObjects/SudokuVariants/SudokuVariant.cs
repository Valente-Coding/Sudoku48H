using UnityEngine;

[CreateAssetMenu(fileName = "SV_", menuName = "ScriptableObjects/Sudoku Variant")]
public class SudokuVariant : ScriptableObject
{
    public int GridColumns { get => _gridColumns; }
    public int GridRows { get => _gridRows; }
    public int SquareColumns { get => _squareColumns; }
    public int SquareRows { get => _squareRows; }
    public int InicialHints { get => _inicialHints; set => _inicialHints = value; }

    [Header("Grid Config")]
    [SerializeField] private int _gridColumns;
    [SerializeField] private int _gridRows;

    [Header("Square Config")]
    [SerializeField] private int _squareColumns;
    [SerializeField] private int _squareRows;

    [Header("Others")]
    [Tooltip("Number of hints at the beginning of the game.")]
    [SerializeField] private int _inicialHints;

}

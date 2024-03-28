using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class SplitContainers : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float _splitPercentage = 50;

    private RectTransform _rect;
    
    void OnRectTransformDimensionsChange()
    {
        if (_rect == null)
            _rect = transform.GetComponent<RectTransform>();
        
        RectTransform childLeft = transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform childRight = transform.GetChild(1).GetComponent<RectTransform>();

        float totalWidth = _rect.rect.width;
        float totalHeight = _rect.rect.height;
        float percentageWidth = _rect.rect.width * _splitPercentage / 100;

        childLeft.sizeDelta = new Vector2(percentageWidth, totalHeight);
        childRight.sizeDelta = new Vector2(totalWidth - percentageWidth, totalHeight);
    }
}

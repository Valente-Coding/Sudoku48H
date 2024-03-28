using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AdvacedAspectRatioFitter : AspectRatioFitter
{
    private RectTransform _rectParent;
    private RectTransform _rect;
    private Vector2 offsetMin;
    private Vector2 offsetMax;

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();

        if (_rectParent == null)
            _rectParent = transform.parent.GetComponent<RectTransform>();

        if (_rect == null)
            _rect = transform.GetComponent<RectTransform>();

        if (offsetMin == null)
            offsetMin = _rect.offsetMin;

        if (offsetMax == null)
            offsetMax = _rect.offsetMax;

        if (_rectParent.rect.width > _rectParent.rect.height) {
            aspectMode = AspectMode.HeightControlsWidth;
            _rect.offsetMin = new Vector2(offsetMin.y, offsetMin.x);
            _rect.offsetMax = new Vector2(offsetMax.y, offsetMax.x);
        }
        else {
            aspectMode = AspectMode.WidthControlsHeight;
            _rect.offsetMin = new Vector2(offsetMin.x, offsetMin.y);
            _rect.offsetMax = new Vector2(offsetMax.x, offsetMax.y);
        }

        
        
    }
}

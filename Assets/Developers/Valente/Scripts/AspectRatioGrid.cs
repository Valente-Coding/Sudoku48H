using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatioGrid : GridLayoutGroup
{

    protected override void Start() {
        base.Start();

        if (constraint == Constraint.Flexible || constraintCount == 0) return;

        cellSize = new Vector2(rectTransform.rect.width / constraintCount - spacing.x, rectTransform.rect.height / constraintCount - spacing.y);
    }
}

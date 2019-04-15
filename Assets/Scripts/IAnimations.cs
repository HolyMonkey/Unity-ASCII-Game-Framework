using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimations
{
    IAnimationProperties Transit(int fromX, int fromY, int toX, int toY);
    IAnimationProperties FadeIn();
    IAnimationProperties FadeOut();
    IAnimationProperties StretchFigure();
}

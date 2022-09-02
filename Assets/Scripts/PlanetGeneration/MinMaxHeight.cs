using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxHeight
{
    public float min;
    public float max;

    public MinMaxHeight()
    {
        this.max = float.MinValue;
        this.min = float.MaxValue;
    }

    public void checkValue(float val)
    {
        if (val > max)
            max = val;
        if (val < min)
            min = val;
    }
}

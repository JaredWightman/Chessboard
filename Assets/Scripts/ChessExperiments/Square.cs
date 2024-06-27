using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    private int x;
    private int y;

    public int getX()
    {
        return x;
    }

    public void setX(int newX)
    {
        x = newX;
    }

    public int getY()
    {
        return y;
    }

    public void setY(int newY)
    {
        y = newY;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetonObject {

    public const int JETON_YELLOW = 1;
    public const int JETON_RED = 2;

    private int theJetonColor;

    //////////////////////////////////////////////////////////////////////////////////
    // Constructor
    //////////////////////////////////////////////////////////////////////////////////
    /// 
    public JetonObject(int color)
    {
        this.theJetonColor = color;
    }

    //////////////////////////////////////////////////////////////////////////////////
    // methods
    //////////////////////////////////////////////////////////////////////////////////
    /// 

    //////////////////////////////////////////////////////////////////////////////////
    // Getter / Setter
    //////////////////////////////////////////////////////////////////////////////////

    public int GetJetonColor()
    {
        return theJetonColor;
    }
    public void SetJetonColor(int color)
    {
        theJetonColor = color;
    }

}

//////////////////////////////////////////////////////////////////////////////////
///  All game type definitions
//////////////////////////////////////////////////////////////////////////////////

public struct PlatePosition
{
    public int x;
    public int y;

    // Constructor:
    public PlatePosition(int xx, int yy)
    {
        this.x = xx;
        this.y = yy;
    }
}

public struct CasePosition
{
    public int theCase;
    public int l;
    public int c;

    // Constructor:
    public CasePosition(int laCase, int c, int l)
    {
        this.c = c;
        this.l = l;
        this.theCase = laCase;
    }
}

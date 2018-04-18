using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseObject
{
    private bool bOccupied;
    private JetonObject theJeton;
    private PlatePosition thePosition;

    //////////////////////////////////////////////////////////////////////////////////
    // Constructor
    //////////////////////////////////////////////////////////////////////////////////
    /// 
    public CaseObject(bool bOcc, JetonObject jeton, PlatePosition pos)
    {
        this.bOccupied = bOcc;
        this.theJeton = jeton;
        this.thePosition = pos;
    }

    //////////////////////////////////////////////////////////////////////////////////
    // methods
    //////////////////////////////////////////////////////////////////////////////////
    /// 

    //////////////////////////////////////////////////////////////////////////////////
    // Getter / Setter
    //////////////////////////////////////////////////////////////////////////////////

    public bool GetCaseOccupied()
    {
        return bOccupied;
    }

    public void SetCaseOccupied(bool occup)
    {
        bOccupied = occup;
    }

    public JetonObject GetCaseJeton()
    {
        return theJeton;
    }
    public void SetCaseJeton(JetonObject obj)
    {
        theJeton = obj;
        SetCaseOccupied(true);
    }
    public PlatePosition GetCasePosition()
    {
        return thePosition;
    }
}

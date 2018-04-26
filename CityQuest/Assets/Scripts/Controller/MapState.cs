using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MapState : DefaultState
{
    public MapState(Controller c) : base(c)
    {
    }

    /// <summary>
    /// Pops a pop up to close the app
    /// </summary>
    public override void ReturnAction()
    {
        controller.AskLeave();
    }

    public new void OptionAction()
    {
    }
}
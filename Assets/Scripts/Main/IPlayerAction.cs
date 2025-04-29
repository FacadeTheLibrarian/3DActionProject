using System.Collections.Generic;
using UnityEngine;

internal interface IPlayerAction{
    public e_inputActions Act(ref GameObject handler, ref PlayerRoot parent);
}
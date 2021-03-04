using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Grid Actions/Multi Grid Action")]
public class GA_MultiGridAction : GridAction
{
    [SerializeField]
    GridAction[] gridActions;
    protected override void Action(PlayGrid grid)
    {
        foreach(GridAction action in gridActions)
        {
            action.Invoke(grid);
        }
    }
}

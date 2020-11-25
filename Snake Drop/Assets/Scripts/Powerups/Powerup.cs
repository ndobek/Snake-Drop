using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerup")]
public class Powerup : ScriptableObject
{
    public Sprite sprite;
    public GridAction OnActivate;

    public void Activate()
    {
        OnActivate.Invoke(GameManager.instance.playerManagers[0].playGrid);
    }
}

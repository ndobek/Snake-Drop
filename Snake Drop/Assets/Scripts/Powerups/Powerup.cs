using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerup")]
public class Powerup : ScriptableObject, ISaveable
{
    public Sprite sprite;
    public GridAction OnActivate;
    private string powerupName;
    public string Name { get { return powerupName; } set { powerupName = value; } }

    public void Activate()
    {
        OnActivate.Invoke(GameManager.instance.playerManagers[0].playGrid);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "List of Saveable Objects")]
public class SaveableObjects : ScriptableObject
{
    [SerializeField]
    List<Object> Objects = new List<Object>();

    public Object getObject(string name)
    {
        foreach(Object obj in Objects)
        {
            ISaveable saveable = null;
            if (obj.GetType() == typeof(ISaveable))
            {
                saveable = obj as ISaveable;
            } else if (obj.GetType() == typeof(GameObject))
            {
                GameObject gameObject = (GameObject)obj;
                saveable = gameObject.GetComponent<ISaveable>();
            }

            if (saveable != null && saveable.Name == name) return obj;
        }
        return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    private static Toolbox instance;
    private static Toolbox Instance {
        get
        {
            if (instance == null) {
                instance = FindObjectOfType<Toolbox>();
            }

            return instance;
        }
    }

    private List<Manager> managers;

    public static T GetManager<T>() where T : Manager
    {
        return instance.FindManager<T>();
    }

    private T FindManager<T>() where T : Manager
    {
        return managers.First(x => x is T) as T;
    }

    private void RegisterManagers()
    {
        managers = transform.GetComponentsInChildren<Manager>().ToList();
    }

    private void Start()
    {
        RegisterManagers();
    }

}

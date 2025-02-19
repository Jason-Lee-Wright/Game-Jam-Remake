using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void DontDestroy()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}

public class Singleton : MonoBehaviour
{
    static Singleton instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
    private static SurfaceManager _instance;

    [SerializeField] private List<SurfaceType> Surfaces = new List<SurfaceType>();
    [SerializeField] private int DefaultPoolSizes = 10;
    [SerializeField] private Surface DefaultSurface;


    public static SurfaceManager Instance
    {
        get
        {
            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Больше одного менеджера поверхности активно в сцене! Уничтожение последнего менеджера : " + name);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}

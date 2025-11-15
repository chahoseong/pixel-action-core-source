using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    [SerializeField] private GameplayTagManager gameplayTagManager;
    
    private static ServiceLocator instance;
    
    public static GameplayTagManager GameplayTagManager => instance.gameplayTagManager;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

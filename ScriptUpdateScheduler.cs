using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScriptUpdateScheduler : MonoBehaviour
{
    public interface IFixedUpdatable
    {
        int FixedUpdateOrder => 100;
        void OnFixedUpdate(float fixedDeltaTime);
    }
    
    public interface IUpdatable
    {
        int UpdateOrder => 100;
        void OnUpdate(float deltaTime);
    }
    
    public interface ILateUpdatable
    {
        int LateUpdateOrder => 100;
        void OnLateUpdate(float deltaTime);
    }

    private List<IFixedUpdatable> fixedUpdateQueue;
    private List<IUpdatable> updateQueue;
    private List<ILateUpdatable> lateUpdateQueue;

    private void Awake()
    {
        // fixed updatable
        fixedUpdateQueue = GetComponents<IFixedUpdatable>()
            .OrderBy(x => x.FixedUpdateOrder)
            .ToList();
        
        // updatable
        updateQueue = GetComponents<IUpdatable>()
            .OrderBy(x => x.UpdateOrder)
            .ToList();
        
        // late updatable
        lateUpdateQueue = GetComponents<ILateUpdatable>()
            .OrderBy(x => x.LateUpdateOrder)
            .ToList();
    }
    
    private void FixedUpdate()
    {
        foreach (var fixedUpdatable in fixedUpdateQueue)
        {
            fixedUpdatable.OnFixedUpdate(Time.fixedDeltaTime);
        }
    }
    
    private void Update()
    {
        foreach (var updatable in updateQueue)
        {
            updatable.OnUpdate(Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        foreach (var lateUpdatable in lateUpdateQueue)
        {
            lateUpdatable.OnLateUpdate(Time.deltaTime);
        }
    }
}

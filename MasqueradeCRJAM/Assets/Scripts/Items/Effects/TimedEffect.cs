using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedEffect : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float duration;
    [SerializeField] private float tick;

    [SerializeField] private UnityEvent<Entity> onStart;
    [SerializeField] private UnityEvent<Entity> onTick;
    [SerializeField] private UnityEvent<Entity> onExit;

    public void Execute(Entity self)
    {
        StartCoroutine(DoExecute(self));
    }

    IEnumerator DoExecute(Entity self)
    {
        float timer = delay;
        float tickTimer = 0;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        onStart?.Invoke(self);
        timer = duration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            tickTimer -= Time.deltaTime;
            if (tickTimer < 0)
            {
                onTick?.Invoke(self);
                tickTimer = tick;
            }
            yield return null;
        }
        onExit?.Invoke(self);
    }
}

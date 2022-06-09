using UnityEngine;
using UnityEngine.Events;

namespace Actions
{
    public abstract class BaseAction : ScriptableObject
    {
        public UnityAction onEventRaised;

        public virtual void EventRaise()
        {
            onEventRaised?.Invoke();
        }

        protected virtual void ClearDelegates()
        {
            if ((object)onEventRaised != null)
                foreach (var @delegate in onEventRaised.GetInvocationList())
                {
                    if (@delegate == null)
                        continue;
                    onEventRaised -= (UnityAction)@delegate;
                }
        }
    }

    public abstract class BaseAction<T> : ScriptableObject
    {
        public UnityAction<T> onEventRaised;

        public virtual void EventRaise(T obj)
        {
            onEventRaised?.Invoke(obj);
        }

        protected virtual void ClearDelegates()
        {
            if ((object)onEventRaised != null)
                foreach (var @delegate in onEventRaised.GetInvocationList())
                {
                    if (@delegate == null)
                        continue;
                    onEventRaised -= (UnityAction<T>)@delegate;
                }
        }
    }

    public abstract class BaseAction<T1, T2> : ScriptableObject
    {
        public UnityAction<T1, T2> onEventRaised;

        public virtual void EventRaise(T1 obj1, T2 obj2)
        {
            onEventRaised?.Invoke(obj1, obj2);
        }

        protected virtual void ClearDelegates()
        {
            if ((object)onEventRaised != null)
                foreach (var @delegate in onEventRaised.GetInvocationList())
                {
                    if (@delegate == null)
                        continue;
                    onEventRaised -= (UnityAction<T1, T2>)@delegate;
                }
        }
    }

    public abstract class BaseAction<T1, T2, T3> : ScriptableObject
    {
        public UnityAction<T1, T2, T3> onEventRaised;

        public virtual void EventRaise(T1 obj1, T2 obj2, T3 obj3)
        {
            onEventRaised?.Invoke(obj1, obj2, obj3);
        }

        protected virtual void ClearDelegates()
        {
            if ((object)onEventRaised != null)
                foreach (var @delegate in onEventRaised.GetInvocationList())
                {
                    if (@delegate == null)
                        continue;
                    onEventRaised -= (UnityAction<T1, T2, T3>)@delegate;
                }
        }
    }

    public abstract class BaseAction<T1, T2, T3, T4> : ScriptableObject
    {
        public UnityAction<T1, T2, T3, T4> onEventRaised;

        public virtual void EventRaise(T1 obj1, T2 obj2, T3 obj3, T4 obj4)
        {
            onEventRaised?.Invoke(obj1, obj2, obj3, obj4);
        }

        protected virtual void ClearDelegates()
        {
            if ((object)onEventRaised != null)
                foreach (var @delegate in onEventRaised.GetInvocationList())
                {
                    if (@delegate == null)
                        continue;
                    onEventRaised -= (UnityAction<T1, T2, T3, T4>)@delegate;
                }
        }
    }
}
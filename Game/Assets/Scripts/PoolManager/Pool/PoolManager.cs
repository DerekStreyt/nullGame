using System;
using System.Collections.Generic;

namespace Engine
{
    public class PoolManager<TPool> 
        where TPool : IPoolObject
    {
        public virtual int MaxInstances { get; protected set; }
        public virtual int Count => objects.Count;

        protected Dictionary<Type, Stack<TPool>> objects;

        public PoolManager(int maxInstance)
        {
            MaxInstances = maxInstance;
            objects = new Dictionary<Type, Stack<TPool>>();
        }

        public virtual bool CanPush()
        {
            return Count + 1 < MaxInstances;
        }

        public virtual bool Push(TPool value)
        {
            bool result = false;
            if (!value.IsPushed && CanPush())
            {
                value.IsPushed = true;
                value.OnPush();
                Type type = value.GetType();
                if (objects.TryGetValue(type, out Stack<TPool> collection))
                {
                    collection.Push(value);
                }
                else
                {
                    Stack<TPool> newCollection = new Stack<TPool>();
                    newCollection.Push(value);
                    objects.Add(type, newCollection);
                }

                result = true;
            }
            else
            {
                value.FailedPush();
            }

            return result;
        }

        public virtual TResult Pop<TResult>() where TResult : TPool
        {
            TResult result = default;
            Type type = typeof(TResult);
            if (objects.TryGetValue(type, out Stack<TPool> collection) && collection.Count > 0)
            {
                result = (TResult) collection.Pop();
                if (collection.Count == 0)
                {
                    objects.Remove(type);
                }
                result.IsPushed = false;
                result.Create();
            }

            return result;
        }

        public virtual bool Contains(Type type)
        {
            return objects.ContainsKey(type);
        }

        public virtual void Clear()
        {
            objects.Clear();
        }
    }
}
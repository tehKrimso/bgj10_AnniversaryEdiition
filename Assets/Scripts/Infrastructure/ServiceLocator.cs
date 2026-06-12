using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, IService> _services = new();


        public void Register<TType>(IService service) where TType : class, IService
        {
            _services.Add(typeof(TType), service);
        }

        public TType Resolve<TType>() where TType : class, IService
        {
            if (!_services.TryGetValue(typeof(TType), out IService service))
            {
                Debug.Log($"No service of type {typeof(TType)}");
            }
            
            return service as TType;
        }
    }
}
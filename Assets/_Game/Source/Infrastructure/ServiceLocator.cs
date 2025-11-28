using System;

namespace _Game.Source.Infrastructure
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Container => _instance ??= new ServiceLocator();

        public void RegisterSingle<TContract>(TContract implementation)
        {
            Implementation<TContract>.Instance = implementation;
        }
        public void RegisterSingle<TContract>(Func<ServiceLocator, TContract> factory)
        {
            Implementation<TContract>.Instance = factory.Invoke(_instance);
        }
        public TContract Resolve<TContract>()
        {
            return Implementation<TContract>.Instance;
        }
        private static class Implementation<TContract>
        {
            public static TContract Instance;
        } 
    }
}
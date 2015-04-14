﻿using NUnit.Framework;
using Solid.Data.Fake.Core;
using Solid.Fake.Builders;
using Solid.Practices.IoC;

namespace Tests.Core
{
    public abstract class IntegrationTestsBase<TContainer, TFakeFactory> 
        where TContainer : IIocContainer 
        where TFakeFactory : IFakeFactory, new()
    {
        protected TContainer IocContainer;
        protected TFakeFactory FakeFactory;

        public IntegrationTestsBase()
        {
            FakeFactory = new TFakeFactory();
        }

        protected void RegisterService<TService>(TService service) where TService : class
        {
            IocContainer.RegisterInstance(service);
        }

        protected void RegisterBuilder<TService>(FakeBuilderBase<TService> builder) where TService : class
        {
            RegisterService(builder.GetService());
        }

        protected void RegisterStub<TService>() where TService : class
        {
            RegisterFake(FakeFactory.CreateFake<TService>());
        }

        protected void RegisterFake<TService>(IFake<TService> fake) where TService : class
        {
            RegisterService(fake.Object);
        }

        protected TService Resolve<TService>()
        {
            return IocContainer.Resolve<TService>();
        }
    }

    public abstract class IntegrationTestsBase<TContainer, TFakeFactory, TRootObject> : 
        IntegrationTestsBase<TContainer, TFakeFactory> 
        where TContainer : IIocContainer, new()
        where TFakeFactory : IFakeFactory, new()
    {
        protected TRootObject CreateRootObject()
        {
            return Resolve<TRootObject>();
        }

        [SetUp]
        protected void Setup()
        {
            SetupCore();
            SetupOverride();
        }

        [TearDown]
        protected void TearDown()
        {
            TearDownCore();
            TearDownOverride();
        }

        private void SetupCore()
        {
            IocContainer = new TContainer();
            new TestBootstrapper<TContainer>(IocContainer);
        }

        protected virtual void SetupOverride()
        {
            
        }

        private void TearDownCore()
        {
            //Dispose();
        }

        protected virtual void TearDownOverride()
        {
            
        }


    }
}
﻿using System.Linq;
using NUnit.Framework;
using PCLStorage;

namespace Solid.Practices.Composition.Tests
{
    [TestFixture]
    class CompositionContainerTests
    {
        [Test]
        public void RootPathContainsCompositionModules_CompositionModulesAreImported()
        {
            var rootPath = GetRootPath();

            var compositionContainer = new CompositionContainer(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(1, modulesCount);
        }

        [Test]
        public void RootPathContainsCustomModules_CustomModulesAreImported()
        {
            var rootPath = GetRootPath();

            var compositionContainer = new CompositionContainer<ICustomModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        [Test]
        public void RootPathContainsCustomModulesAndCorrectPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetRootPath();

            var compositionContainer = new CompositionContainer<ICustomModule>(rootPath, new[] { "Solid" });
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        [Test]
        public void RootPathContainsCustomModulesAndIncorrectPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetRootPath();

            var compositionContainer = new CompositionContainer<ICustomModule>(rootPath, new[] { "Incorrect" });
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(0, modulesCount);
        }

        [Test]
        public void RootPathContainsCustomModulesAndNoPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetRootPath();

            var compositionContainer = new CompositionContainer<ICustomModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        private string GetRootPath()
        {            
            return FileSystem.Current.LocalStorage.Path;            
        }
    }
}

﻿using Moq;
using NUnit.Framework;
using Pikit.Shared;
using Pikit.Shared.Configuration;
using Pikit.Shared.UnitOfWork;
using Pikit.Tests.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests
{
    [TestFixture]
    public class TestBase
    {
        public virtual bool UseDatabase { get { return false; } }

        private IUnitOfWork _uow;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_uow == null)
                {
                    _uow = Kernel.Get<IUnitOfWork>();
                }
                return _uow;
            }
        }

        public void RefreshDbContext()
        {
            _uow = null;
        }

        [SetUp]
        public void BaseSetUp()
        {
            Kernel.Initialize();

            Kernel.Load(new TestModules());

            if (UseDatabase)
            {
                Kernel.Load(new TestDatabaseModules());

                new DatabaseInitializer().Go();
            }
            else
            {
                Kernel.Load(new TestInMemoryModules());
            }

            ClearFileDropDirectory();
            RefreshDbContext();
        }

        private void ClearFileDropDirectory()
        {
            var directory = Kernel.Get<IConfigurationProperties>().FileDropDirectory;
            if (!string.IsNullOrEmpty(directory))
            {
                if (Directory.Exists(directory))
                {
                    Directory.Delete(directory, true);
                }
                Directory.CreateDirectory(directory);
            }
        }
    }
}
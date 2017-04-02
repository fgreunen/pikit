﻿using NUnit.Framework;
using Pikit.Shared;
using Pikit.Shared.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests.FunctionalTests
{
    [TestFixture]
    public abstract class FunctionalTestBase
        : TestBase
    {
        public override bool UseDatabase { get { return true; } }

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
    }
}

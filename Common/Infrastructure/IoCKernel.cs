﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace Common.Infrastructure
{
    public class IoCKernel
    {
        public static Container IoC { get; private set; } = null!;

        public IoCKernel(Container container)
        {
            IoC = container;
        }
    }
}
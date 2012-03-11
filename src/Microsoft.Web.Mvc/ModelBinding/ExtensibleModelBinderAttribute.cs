﻿using System;

namespace Microsoft.Web.Mvc.ModelBinding
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class ExtensibleModelBinderAttribute : Attribute
    {
        public ExtensibleModelBinderAttribute(Type binderType)
        {
            BinderType = binderType;
        }

        public Type BinderType { get; private set; }

        public bool SuppressPrefixCheck { get; set; }
    }
}

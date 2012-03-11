﻿using System.Web;

namespace Microsoft.Internal.Web.Utils
{
    internal sealed class VirtualPathUtilityWrapper : IVirtualPathUtility
    {
        public string Combine(string basePath, string relativePath)
        {
            return VirtualPathUtility.Combine(basePath, relativePath);
        }

        public string ToAbsolute(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }
    }
}

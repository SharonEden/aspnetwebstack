﻿using System.Web.Mvc;

namespace Microsoft.Web.Mvc
{
    public class DynamicViewPage : ViewPage
    {
        public new dynamic Model
        {
            get { return DynamicReflectionObject.Wrap(base.Model); }
        }

        public new dynamic ViewData
        {
            get { return DynamicViewDataDictionary.Wrap(base.ViewData); }
        }
    }
}

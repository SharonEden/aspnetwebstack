﻿using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Web.UnitTestUtil;
using Xunit;

namespace Microsoft.Web.Mvc.ModelBinding.Test
{
    public class CollectionModelBinderProviderTest
    {
        [Fact]
        public void GetBinder_CorrectModelTypeAndValueProviderEntries_ReturnsBinder()
        {
            // Arrange
            ExtensibleModelBindingContext bindingContext = new ExtensibleModelBindingContext
            {
                ModelMetadata = new EmptyModelMetadataProvider().GetMetadataForType(null, typeof(IEnumerable<int>)),
                ModelName = "foo",
                ValueProvider = new SimpleValueProvider
                {
                    { "foo[0]", "42" },
                }
            };

            CollectionModelBinderProvider binderProvider = new CollectionModelBinderProvider();

            // Act
            IExtensibleModelBinder binder = binderProvider.GetBinder(null, bindingContext);

            // Assert
            Assert.IsType<CollectionModelBinder<int>>(binder);
        }

        [Fact]
        public void GetBinder_ModelTypeIsIncorrect_ReturnsNull()
        {
            // Arrange
            ExtensibleModelBindingContext bindingContext = new ExtensibleModelBindingContext
            {
                ModelMetadata = new EmptyModelMetadataProvider().GetMetadataForType(null, typeof(int)),
                ModelName = "foo",
                ValueProvider = new SimpleValueProvider
                {
                    { "foo[0]", "42" },
                }
            };

            CollectionModelBinderProvider binderProvider = new CollectionModelBinderProvider();

            // Act
            IExtensibleModelBinder binder = binderProvider.GetBinder(null, bindingContext);

            // Assert
            Assert.Null(binder);
        }

        [Fact]
        public void GetBinder_ValueProviderDoesNotContainPrefix_ReturnsNull()
        {
            // Arrange
            ExtensibleModelBindingContext bindingContext = new ExtensibleModelBindingContext
            {
                ModelMetadata = new EmptyModelMetadataProvider().GetMetadataForType(null, typeof(IEnumerable<int>)),
                ModelName = "foo",
                ValueProvider = new SimpleValueProvider()
            };

            CollectionModelBinderProvider binderProvider = new CollectionModelBinderProvider();

            // Act
            IExtensibleModelBinder binder = binderProvider.GetBinder(null, bindingContext);

            // Assert
            Assert.Null(binder);
        }
    }
}

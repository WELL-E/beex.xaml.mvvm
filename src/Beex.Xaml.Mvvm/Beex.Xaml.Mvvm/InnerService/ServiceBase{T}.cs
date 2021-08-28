// Copyright (c) well-e.
// Licensed under the MIT License.

using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Data;

namespace Beex.Xaml.Mvvm
{
    public abstract class ServiceBase<T> : Behavior<T> where T : DependencyObject
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            ServiceBase<T>.ServiceBindingHelper.SetServiceBinding(this);
        }

        protected override void OnDetaching()
        {
            ServiceBase<T>.ServiceBindingHelper.ClearServiceBinding(this);
            base.OnDetaching();
        }

        protected virtual void OnServicesChanged(ISupportServices oldServiceClient, ISupportServices newServiceClient)
        {
            if (oldServiceClient != null)
                oldServiceClient.InnerContainer.UnregisterService(this);
            if (newServiceClient != null)
                newServiceClient.InnerContainer.RegisterService(this);
        }

        [FieldTargetAttribute]
        private static readonly DependencyProperty ServicesReBingingProperty =
        DependencyProperty.Register("ServicesReBinging", typeof(object), typeof(ServiceBase<T>),
        new PropertyMetadata(null, delegate (DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ServiceBase<T>)d).OnServicesChanged(e.OldValue as ISupportServices, e.NewValue as ISupportServices);
        }));

        internal static class ServiceBindingHelper
        {
            public static void SetServiceBinding(ServiceBase<T> service)
            {
                BindingOperations.SetBinding(service, ServicesReBingingProperty, new Binding
                {
                    Path = new PropertyPath("DataContext", new object[0]),
                    Source = service.AssociatedObject
                });
            }

            public static void ClearServiceBinding(ServiceBase<T> service)
            {
                BindingOperations.ClearBinding(service, ServicesReBingingProperty);
            }

            public static bool IsServiceBindingSet(ServiceBase<T> service)
            {
                return BindingOperations.IsDataBound(service, ServicesReBingingProperty);
            }
        }
    }
}
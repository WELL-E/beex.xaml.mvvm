// Copyright (c) well-e.
// Licensed under the MIT License.

using System.Windows;

namespace Beex.Xaml.Mvvm
{
    public abstract class ServiceBase : ServiceBase<FrameworkElement>
    {
        private bool _isLoaded;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.Subscribe();
        }

        protected override void OnDetaching()
        {
            this.Unsubscribe();
            base.OnDetaching();
        }

        private void Subscribe()
        {
            if (base.AssociatedObject == null)
            {
                return;
            }
            base.AssociatedObject.Loaded += this.OnLoaded;
            base.AssociatedObject.Unloaded += this.OnUnloaded;
        }

        private void Unsubscribe()
        {
            if (base.AssociatedObject == null)
            {
                return;
            }
            base.AssociatedObject.Loaded -= this.OnLoaded;
            base.AssociatedObject.Unloaded -= this.OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this._isLoaded)
            {
                return;
            }
            this._isLoaded = true;
            if (!ServiceBindingHelper.IsServiceBindingSet(this))
            {
                ServiceBindingHelper.SetServiceBinding(this);
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (!this._isLoaded)
            {
                return;
            }
            this._isLoaded = false;
            ServiceBindingHelper.ClearServiceBinding(this);
        }

    }
}
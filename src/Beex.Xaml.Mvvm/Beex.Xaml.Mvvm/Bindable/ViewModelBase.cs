// Copyright (c) well-e.
// Licensed under the MIT License.

namespace Beex.Xaml.Mvvm
{
    public abstract class ViewModelBase : BindableBase, ISupportServices
    {
        private IViewContainer _innerContainer;
        IViewContainer ISupportServices.InnerContainer
        {
            get { return InnerContainer; }
        }


        protected virtual IViewContainer CreateInnerContainer()
        {
            return new ViewContainer();
        }


        protected IViewContainer InnerContainer
        {
            get
            {
                IViewContainer result;
                if ((result = _innerContainer) == null)
                {
                    result = _innerContainer = CreateInnerContainer();
                }
                return result;
            }
        }


        protected virtual T GetService<T>() where T : class
        {
            return InnerContainer.GetService<T>();
        }


    }
}

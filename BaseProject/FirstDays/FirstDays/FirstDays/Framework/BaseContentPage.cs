using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FirstDays.Framework
{
    public abstract class BaseContentPage : ContentPage
    {
        internal void ForceOnAppearing() => this.OnAppearing();

        internal void ForceOnDisappearing() => this.OnDisappearing();

        public virtual void MyOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }

    public abstract class BaseContentPage<T> : BaseContentPage
            where T : BaseViewModel
    {
        public BaseContentPage()
        {
            this.BindingContext = Locator.Resolve<T>();
        }

        public event EventHandler Resuming;

        protected virtual T ViewModel => this.BindingContext as T;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.PropertyChanged += MyOnPropertyChanged;
            await this.ViewModelAppearingAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.PropertyChanged -= MyOnPropertyChanged;
            await this.ViewModel?.LeavingAsync();
        }

        private async Task ViewModelAppearingAsync()
        {
            if (this.ViewModel == null)
            {
                return;
            }

            if (this.ViewModel.IsLoaded)
            {
                this.Resuming?.Invoke(this, EventArgs.Empty);
                await this.ViewModel?.ResumingAsync();
            }
            else
            {
                this.ViewModel.IsLoaded = true;
                await this.ViewModel?.EnteringAsync();
            }
        }
    }
}

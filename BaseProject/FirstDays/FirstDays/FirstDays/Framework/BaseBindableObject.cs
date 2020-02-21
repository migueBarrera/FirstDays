using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FirstDays.Framework
{
    public abstract class BaseBindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetAndRaisePropertyChanged<TRef>(
    ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            UpdateProperty(propertyName);
        }

        protected void SetAndRaisePropertyChangedIfDifferentValues<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
            where TRef : class
        {
            if (field == null || !field.Equals(value))
            {
                SetAndRaisePropertyChanged(ref field, value, propertyName);
            }
        }

        protected void UpdateProperty([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

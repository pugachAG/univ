using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.CompilerServices;

namespace QueuingSystemsModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public const string EmptyProperty = "";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = EmptyProperty)
        {
            var handlers = PropertyChanged;
            if (handlers != null)
            {
                handlers(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = ReflectionHelper.ExtractPropertyName(propertyExpression);
            VerifyPropertyName(propertyName);
            this.RaisePropertyChanged(propertyName);
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                Debug.Fail(msg);
            }
        }

    }

}


using System;

namespace UnityFPS.Tools.ReactiveVariable
{
    public interface IObservableVariable<T>
    {
        public Action<T> OnValueChanged { get; set; }
    }
}

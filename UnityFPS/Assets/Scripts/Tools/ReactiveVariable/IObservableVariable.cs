
using System;

namespace UnityFPS.Tools.ReactiveVariable
{
    public interface IObservableVariable<T>
    {
        public T Value { get; }
        public Action<T> OnValueChanged { get; set; }
    }
}

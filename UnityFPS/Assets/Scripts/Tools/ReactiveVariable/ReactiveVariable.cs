using System;

namespace UnityFPS.Tools.ReactiveVariable
{
    public abstract class ReactiveVariable<T> : IObservableVariable<T>
    {
        public Action<T> OnValueChanged { get; set; }

        public T Value {
            get => currentValue;
            set => SetValue(value);
        }

		private T currentValue;

        private void SetValue(T newValue)
		{
            currentValue = newValue;
            OnValueChanged?.Invoke(newValue);
        }
    }
}
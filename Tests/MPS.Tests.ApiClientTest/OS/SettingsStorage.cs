using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MPS.Tests.ApiClientTest.OS
{
    public class SettingsStorage : Core.Lib.OS.ISettingsStorage

    {
        static Dictionary<string, object> storage = new Dictionary<string, object>();
        public T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (!storage.ContainsKey(propertyName)) return default;
            return (T)storage[propertyName];
        }

        public void SetValue<T>(T newValue = default, [CallerMemberName] string propertyName = null)
        {
            storage[propertyName] = newValue;
        }
    }
}

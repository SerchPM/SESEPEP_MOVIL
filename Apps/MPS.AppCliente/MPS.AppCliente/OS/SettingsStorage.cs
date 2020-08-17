using System.Runtime.CompilerServices;
using Xamarin.Essentials;

namespace MPS.AppCliente.Views.OS
{
    public class SettingsStorage : Core.Lib.OS.ISettingsStorage
    {
        public T GetValue<T>([CallerMemberName] string propertyName = null) =>
            propertyName switch
            {
                "WebAPIUrl" => (T)(object)Preferences.Get(propertyName, "https://localhost:44328/"),
                //"IntSetting" => (T)(object)Preferences.Get(propertyName, 10),
                _ => default
            };

        public void SetValue<T>(T newValue = default, [CallerMemberName] string propertyName = null)
        {
            switch (propertyName)
            {
                case "WebAPIUrl":
                    Preferences.Set(propertyName, newValue.ToString());
                    break;
                //case "IntSetting":
                //Preferences.Set(propertyName, int.Parse(newValue.ToString());
                //break;
                default:
                    break;
            }
        }
    }
}
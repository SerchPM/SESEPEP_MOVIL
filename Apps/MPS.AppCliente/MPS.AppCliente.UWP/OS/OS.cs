using Sysne.Core.OS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;

namespace MPS.AppCliente.UWP.OS
{
    public class OS : IOS
    {
        public void HideNavigation(bool show)
        {
            //UWP no tiene NavigationBar
        }

        public void SetStatusBarColor(string color)
        {
            SolidColorBrush GetSolidColorBrush(string hex)
            {
                hex = hex.Replace("#", string.Empty);
                byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
                byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
                byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
                byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
                SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
                return myBrush;
            };
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            var foregroundColor = Colors.White;
            var backgroundColor = GetSolidColorBrush(color).Color;

            // Set active window colors
            titleBar.ForegroundColor = foregroundColor;
            titleBar.InactiveForegroundColor = foregroundColor;
            titleBar.BackgroundColor = backgroundColor;
            titleBar.ButtonForegroundColor = foregroundColor;
            titleBar.ButtonBackgroundColor = backgroundColor;
            titleBar.ButtonHoverForegroundColor = foregroundColor;
            titleBar.ButtonHoverBackgroundColor = backgroundColor;
            titleBar.ButtonPressedForegroundColor = foregroundColor;
            titleBar.ButtonPressedBackgroundColor = backgroundColor;

            // Set inactive window colors
            titleBar.InactiveForegroundColor = foregroundColor;
            titleBar.InactiveBackgroundColor = backgroundColor;
            titleBar.ButtonInactiveForegroundColor = foregroundColor;
            titleBar.ButtonInactiveBackgroundColor = backgroundColor;
        }

        public void ShowToast(string text)
        {
            throw new NotImplementedException();
        }
    }
}

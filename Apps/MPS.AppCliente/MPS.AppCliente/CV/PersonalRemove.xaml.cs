using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppCliente.Views.CV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalRemove : ContentView
    {
        public PersonalRemove()
        {
            InitializeComponent();
        }

        public string Nombre
        {
            get => (string)GetValue(NombreProperty);
            set => SetValue(NombreProperty, value);
        }

        public static readonly BindableProperty NombreProperty = BindableProperty.Create(nameof(Nombre), typeof(string), typeof(PersonalRemove), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalRemove)bindable;
            me.Nombre = (string)newValue;
            me.nombrePersonal.Text = me.Nombre;
        });

        public string NombreEmpresa
        {
            get => (string)GetValue(NombreEmpresaProperty);
            set => SetValue(NombreEmpresaProperty, value);
        }

        public static readonly BindableProperty NombreEmpresaProperty = BindableProperty.Create(nameof(NombreEmpresa), typeof(string), typeof(PersonalRemove), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalRemove)bindable;
            me.NombreEmpresa = (string)newValue;
            me.nombreEmpresa.Text = me.NombreEmpresa;
        });

        public string Imagen
        {
            get => (string)GetValue(ImagenProperty);
            set => SetValue(ImagenProperty, value);
        }

        public static readonly BindableProperty ImagenProperty = BindableProperty.Create(nameof(Imagen), typeof(string), typeof(PersonalRemove), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalRemove)bindable;
            me.Imagen = (string)newValue;
            me.perfil.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(me.Imagen)));
        });

        public string Edad
        {
            get => (string)GetValue(EdadProperty);
            set => SetValue(EdadProperty, value);
        }

        public static readonly BindableProperty EdadProperty = BindableProperty.Create(nameof(Edad), typeof(string), typeof(PersonalRemove), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalRemove)bindable;
            me.Edad = (string)newValue;
            me.edad.Text = $"{me.Edad} años";
        });

        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }

        public static readonly BindableProperty RemoveCommandProperty = BindableProperty.Create(nameof(RemoveCommand), typeof(ICommand), typeof(PersonalRemove), default(ICommand),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (PersonalRemove)bindable;
                me.RemoveCommand = (ICommand)newValue;
                me.remove.Command = me.RemoveCommand;
            });

        public object RemoveCommandParameter
        {
            get => (object)GetValue(RemoveCommandParameterProperty);
            set => SetValue(RemoveCommandParameterProperty, value);
        }

        public static readonly BindableProperty RemoveCommandParameterProperty = BindableProperty.Create(nameof(RemoveCommandParameter), typeof(object), typeof(PersonalRemove), default(object),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (PersonalRemove)bindable;
                me.RemoveCommandParameter = (object)newValue;
                me.remove.CommandParameter = me.RemoveCommandParameter;
            });

        public double Ranking
        {
            get => (double)GetValue(RankingProperty);
            set => SetValue(RankingProperty, value);
        }

        public static readonly BindableProperty RankingProperty = BindableProperty.Create(nameof(Ranking), typeof(double), typeof(PersonalRemove), default(double),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalRemove)bindable;
            me.Ranking = (double)newValue;
            var ranking = Math.Round(me.Ranking);
            for (int i = 1; i <= 5; i++)
            {
                if (i <= ranking)
                    me.ranking.Children.Add(new Image { Source = "estrellaon.png", Aspect = Aspect.AspectFit });
                else
                    me.ranking.Children.Add(new Image { Source = "estrellaoff.png", Aspect = Aspect.AspectFit });
            }
        });
    }
}
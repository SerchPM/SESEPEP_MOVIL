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
    public partial class PersonalItem : ContentView
    {
        public PersonalItem()
        {
            InitializeComponent();
        }

        public double Ranking
        {
            get => (double)GetValue(RankingProperty);
            set => SetValue(RankingProperty, value);
        }

        public static readonly BindableProperty RankingProperty = BindableProperty.Create(nameof(Ranking), typeof(double), typeof(PersonalItem), default(double),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalItem)bindable;
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

        public string Nombre
        {
            get => (string)GetValue(NombreProperty);
            set => SetValue(NombreProperty, value);
        }

        public static readonly BindableProperty NombreProperty = BindableProperty.Create(nameof(Nombre), typeof(string), typeof(PersonalItem), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalItem)bindable;
            me.Nombre = (string)newValue;
            me.nombrePersonal.Text = me.Nombre;
        });

        public string NombreEmpresa
        {
            get => (string)GetValue(NombreEmpresaProperty);
            set => SetValue(NombreEmpresaProperty, value);
        }

        public static readonly BindableProperty NombreEmpresaProperty = BindableProperty.Create(nameof(NombreEmpresa), typeof(string), typeof(PersonalItem), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalItem)bindable;
            me.NombreEmpresa = (string)newValue;
            me.nombreEmpresa.Text = me.NombreEmpresa;
        });

        public string Especialidades
        {
            get => (string)GetValue(EspecialidadesProperty);
            set => SetValue(EspecialidadesProperty, value);
        }

        public static readonly BindableProperty EspecialidadesProperty = BindableProperty.Create(nameof(Especialidades), typeof(string), typeof(PersonalItem), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalItem)bindable;
            me.Especialidades = (string)newValue;
            me.especialidades.Text = me.Especialidades;
        });

        public string Imagen
        {
            get => (string)GetValue(ImagenProperty);
            set => SetValue(ImagenProperty, value);
        }

        public static readonly BindableProperty ImagenProperty = BindableProperty.Create(nameof(Imagen), typeof(string), typeof(PersonalItem), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalItem)bindable;
            me.Imagen = (string)newValue;
            me.perfil.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(me.Imagen)));
        });

        public string SourseSelected
        {
            get => (string)GetValue(SourseSelectedProperty);
            set => SetValue(SourseSelectedProperty, value);
        }

        public static readonly BindableProperty SourseSelectedProperty = BindableProperty.Create(nameof(SourseSelected), typeof(string), typeof(PersonalItem),
        defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalItem)bindable;
            me.activo.Source = (string)newValue;
        });

        public string Edad
        {
            get => (string)GetValue(EdadProperty);
            set => SetValue(EdadProperty, value);
        }

        public static readonly BindableProperty EdadProperty = BindableProperty.Create(nameof(Edad), typeof(string), typeof(PersonalItem), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalItem)bindable;
            me.Edad = (string)newValue;
            me.edad.Text = $"{me.Edad} años";
        });

        public ICommand SelectedCommand
        {
            get => (ICommand)GetValue(SelectedCommandProperty);
            set => SetValue(SelectedCommandProperty, value);
        }

        public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create(nameof(SelectedCommand), typeof(ICommand), typeof(PersonalItem), default(ICommand),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (PersonalItem)bindable;
                me.SelectedCommand = (ICommand)newValue;
                me.activo.Command = me.SelectedCommand;
            });

        public object SelectedCommandParameter
        {
            get => (object)GetValue(SelectedCommandParameterProperty);
            set => SetValue(SelectedCommandParameterProperty, value);
        }

        public static readonly BindableProperty SelectedCommandParameterProperty = BindableProperty.Create(nameof(SelectedCommandParameter), typeof(object), typeof(PersonalItem), default(object),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (PersonalItem)bindable;
                me.SelectedCommandParameter = (object)newValue;
                me.activo.CommandParameter = me.SelectedCommandParameter;
            });
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Seleccionado
        {
            get => (bool)GetValue(SeleccionadoProperty);
            set => SetValue(SeleccionadoProperty, value);
        }

        public static readonly BindableProperty SeleccionadoProperty = BindableProperty.Create(nameof(Seleccionado), typeof(bool), typeof(PersonalItem),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (PersonalItem)bindable;
            me.Seleccionado = (bool)newValue;
            if (me.Seleccionado)
                me.activo.Source = "checkin.png";
            else
                me.activo.Source = "checkoff.png";
        });
    }
}
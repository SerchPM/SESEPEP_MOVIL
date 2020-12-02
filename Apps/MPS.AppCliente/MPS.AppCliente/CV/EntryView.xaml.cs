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
    public partial class EntryView : ContentView
    {
        public EntryView()
        {
            InitializeComponent();
            entry.TextChanged += (s, e) =>
            {
                Text = entry.Text;
            };
        }

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(EntryView), default(Keyboard),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (EntryView)bindable;
            me.Keyboard = (Keyboard)newValue;
            me.entry.Keyboard = me.Keyboard;
        });

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryView), default(string),
        defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (EntryView)bindable;
            me.Text = (string)newValue;
            me.entry.Text = me.Text;
        });

        public string Etiqueta
        {
            get => (string)GetValue(EtiquetaProperty);
            set => SetValue(EtiquetaProperty, value);
        }

        public static readonly BindableProperty EtiquetaProperty = BindableProperty.Create(nameof(Etiqueta), typeof(string), typeof(EntryView), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (EntryView)bindable;
            me.Etiqueta = (string)newValue;
            me.label.Text = me.Etiqueta;
        });

        public bool IsEnableText
        {
            get => (bool)GetValue(IsEnableTextProperty);
            set => SetValue(IsEnableTextProperty, value);
        }

        public static readonly BindableProperty IsEnableTextProperty = BindableProperty.Create(nameof(IsEnableText), typeof(bool), typeof(EntryView), default(bool),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (EntryView)bindable;
            me.IsEnableText = (bool)newValue;
            me.entry.IsEnabled = me.IsEnableText;
        });

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(EntryView), default(bool),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (EntryView)bindable;
            me.IsPassword = (bool)newValue;
            me.entry.IsPassword = me.IsPassword;
        });

        public Color TextColor
        {
            get => (Color)GetValue(TextColorTextProperty);
            set => SetValue(TextColorTextProperty, value);
        }

        public static readonly BindableProperty TextColorTextProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(EntryView), default(Color),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (EntryView)bindable;
            me.TextColor = (Color)newValue;
            me.entry.TextColor = me.TextColor;
        });

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(EntryView), default(int),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (EntryView)bindable;
            me.MaxLength = (int)newValue;
            me.label.MaxLines = me.MaxLength;
        });

        public string EtiquetaObligatorio
        {
            get => (string)GetValue(EtiquetaObligatorioProperty);
            set => SetValue(EtiquetaObligatorioProperty, value);
        }

        public static readonly BindableProperty EtiquetaObligatorioProperty = BindableProperty.Create(nameof(EtiquetaObligatorio), typeof(string), typeof(EntryView), default(string),
        defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (EntryView)bindable;
            me.EtiquetaObligatorio = (string)newValue;
            me.labelObligatorio.Text = me.EtiquetaObligatorio;
        });

        public Color TextColorEtiquetaO
        {
            get => (Color)GetValue(TextColorEtiquetaOProperty);
            set => SetValue(TextColorEtiquetaOProperty, value);
        }

        public static readonly BindableProperty TextColorEtiquetaOProperty = BindableProperty.Create(nameof(TextColorEtiquetaO), typeof(Color), typeof(EntryView), default(Color),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (EntryView)bindable;
            me.TextColorEtiquetaO = (Color)newValue;
            me.labelObligatorio.TextColor = me.TextColorEtiquetaO;
        });
    }
}
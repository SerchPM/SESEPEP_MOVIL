﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppCliente.Views.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormaDePago : ContentPage
    {
        public FormaDePago()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            modalFormPay.IsVisible = true;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            modalFormPay.IsVisible = false;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoOrNotToDo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoOrNotToDo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUpdateItemView : ContentPage
    {
        public AddUpdateItemView(AddUpdateItemViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }
    }
}
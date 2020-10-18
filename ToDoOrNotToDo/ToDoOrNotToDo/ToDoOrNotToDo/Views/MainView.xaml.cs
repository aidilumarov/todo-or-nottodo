using System;
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
    public partial class MainView : ContentPage
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;

            // When item is selected, it opens another view with detailed info
            // Item should be de-selected so we can select it again without
            // selecting another item
            ItemsListView.ItemSelected += (s, e) =>
            {
                ItemsListView.SelectedItem = null;
            };
        }
    }
}
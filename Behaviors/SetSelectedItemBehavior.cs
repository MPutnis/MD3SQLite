using MD3SQLite.Models;
using MD3SQLite.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System.Windows.Input;

namespace MD3SQLite.Behaviors
{
    public class SetSelectedItemBehavior : Behavior<Button>
    {
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(SetSelectedItemBehavior), null);

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        protected override void OnAttachedTo(Button bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Clicked += OnButtonClicked;
        }

        protected override void OnDetachingFrom(Button bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Clicked -= OnButtonClicked;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is StudentViewModel viewModel)
            {
                viewModel.SelectedStudent = SelectedItem as Student;
            }
        }
    }
}

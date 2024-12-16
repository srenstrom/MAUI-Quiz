using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace MAUI_Quiz.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? title;
    }
}

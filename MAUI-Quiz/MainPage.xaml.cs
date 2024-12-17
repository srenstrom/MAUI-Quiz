using MAUI_Quiz.ViewModels;

namespace MAUI_Quiz
{
    public partial class MainPage : ContentPage
    {    
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            vm.Title = "Trivia quiz!";
        }
    }
}

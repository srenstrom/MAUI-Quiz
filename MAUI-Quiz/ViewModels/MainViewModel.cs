using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_Quiz.Models;
using MAUI_Quiz.Services;
using System.Collections.ObjectModel;

namespace MAUI_Quiz.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly TriviaService triviaService;

        [ObservableProperty]
        private string selectedCategory;

        [ObservableProperty]
        string answer;

        [ObservableProperty]
        private bool isAnswerVisible;

        [ObservableProperty]
        ObservableCollection<Trivia> trivias = new ObservableCollection<Trivia>();

        [ObservableProperty]
        List<string> categories = new()
            {
                "artliterature",
                "language",
                "sciencenature",
                "general",
                "fooddrink",
                "peopleplaces",
                "geography",
                "historyholidays",
                "entertainment",
                "toysgames",
                "music",
                "mathematics",
                "religionmythology",
                "sportsleisure",                            
            };

        [RelayCommand]
        void ShowAnswer(Trivia trivia)
        {
            Answer = trivia.Answer;
            IsAnswerVisible = true;
        }
        public MainViewModel(TriviaService triviaService)
        {
            this.triviaService = triviaService;
        }
        
        [RelayCommand]
        async Task Get(string category = "")
        {
            IsAnswerVisible = false;
            Trivias.Clear();
            var triviaList = await triviaService.GetTriviaAsync(category);

            foreach (var trivia in triviaList)
            {
                Trivias.Add(trivia);
            }
        }
    }
}

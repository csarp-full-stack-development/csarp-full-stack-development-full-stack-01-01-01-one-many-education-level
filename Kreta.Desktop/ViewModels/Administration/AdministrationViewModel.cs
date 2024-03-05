using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.Administration
{
    public partial class AdministrationViewModel : BaseViewModel
    {
        private EducationLevelViewModel _educationLevelViewModel=new EducationLevelViewModel();

        [ObservableProperty]
        private BaseViewModel _currentAdministrationChildView;

        public AdministrationViewModel()
        {
            _currentAdministrationChildView = _educationLevelViewModel;
        }

        public AdministrationViewModel(EducationLevelViewModel educationLevelViewModel)
        {
            _educationLevelViewModel= educationLevelViewModel;
            CurrentAdministrationChildView= _educationLevelViewModel;            
            CurrentAdministrationChildView.InitializeAsync();
        }

        [RelayCommand]
        private async Task ShowEducationLevel()
        {
            CurrentAdministrationChildView = _educationLevelViewModel;
            await _educationLevelViewModel.InitializeAsync();
        }
    }
}

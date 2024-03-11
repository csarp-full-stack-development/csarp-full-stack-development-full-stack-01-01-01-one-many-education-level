using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;
using Kreta.Shared.Models;
using Kreta.Shared.Models.SchoolCitizens;
using Kreta.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.Administration
{
    public partial class EducationLevelViewModel : BaseViewModel
    {
        private readonly IEducationLevelService? _educationLevelService;
        private readonly IStudentService? _studentService;
        
        [ObservableProperty]
        private ObservableCollection<EducationLevel> _educationLevels = new();

        [ObservableProperty]
        private EducationLevel _selectedEducationLevel;

        [ObservableProperty]
        private ObservableCollection<Student> _studentWithEducationLevel = new();

        [ObservableProperty]
        private Student _selectedStudentWithEducationLevel=new();
        
        [ObservableProperty]
        private ObservableCollection<Student> _studentNoEducationLevel = new();

        [ObservableProperty]
        private Student _selectedStudentWithNoEducationLevel=new();

        public string Title { get; set; } = "Tanulmányi szint kezelése";

        public EducationLevelViewModel()
        {
            _selectedEducationLevel = new EducationLevel();
        }

        public EducationLevelViewModel(IEducationLevelService? educationLevelService, IStudentService? studentService)
        {
            _educationLevelService = educationLevelService;
            _studentService = studentService;
            SelectedEducationLevel = new EducationLevel();
        }

        public async override Task InitializeAsync()
        {
            await UpdateView();
        }

        [RelayCommand]
        private async Task DoSave(EducationLevel educationLevels)
        {
            if (_educationLevelService is not null)
            {
                ControllerResponse result;
                if (educationLevels.HasId)
                    result = await _educationLevelService.UpdateAsync(educationLevels);
                else
                    result = await _educationLevelService.InsertAsync(educationLevels);

                if (!result.HasError)
                {
                    await UpdateView();
                }
            }
        }

        [RelayCommand]
        private async Task DoRemove(EducationLevel educationLevel)
        {
            if (_educationLevelService is not null)
            {
                ControllerResponse result = await _educationLevelService.DeleteAsync(educationLevel.Id);
                if (result.IsSuccess)
                {
                    await UpdateView();
                }
            }
        }

        [RelayCommand]
        private void DoNew()
        {
            SelectedEducationLevel = new EducationLevel();
        }
        
        [RelayCommand]
        private async Task GetStudentsByEducationLevelId()
        {
            //if (_educationLevelService is not null && SelectedEducationLevel.HasId)
            if (_studentService is not null && SelectedEducationLevel is not null &&  SelectedEducationLevel.HasId)
            {
                //List<Student> studentByEducationLevelId = await _educationLevelService.GetStudentsBy(SelectedEducationLevel.Id);
                List<Student> studentByEducationLevelId = await _studentService.GetStudentsBy(SelectedEducationLevel.Id);
                StudentWithEducationLevel = new ObservableCollection<Student>(studentByEducationLevelId);
            }
        }

        [RelayCommand]
        private async Task MoveStudentToNoEducationLevel()
        {
            if (SelectedStudentWithEducationLevel != null && _studentService is not null)
            {
                SelectedStudentWithEducationLevel.EducationLevelId = Guid.Empty;                
                await _studentService.UpdateAsync(SelectedStudentWithEducationLevel);
                await UpdateView();
            }
        }

        [RelayCommand]
        private async Task MoveStudentToSelectedEducationLevel()
        {
            if (SelectedEducationLevel is not null && _studentService is not null)
            {
                SelectedStudentWithNoEducationLevel.EducationLevelId = SelectedEducationLevel.Id;
                await _studentService.UpdateAsync(SelectedStudentWithNoEducationLevel);
                await UpdateView();
            }
        }

        private async Task UpdateView()
        {
            if (_educationLevelService is not null)
            {
                List<EducationLevel> educationLevels = await _educationLevelService.SelectAllAsync();
                EducationLevels = new ObservableCollection<EducationLevel>(educationLevels);
                // Jobb lenne nem az első hanem az előző
                SelectedEducationLevel = educationLevels.FirstOrDefault() ?? new EducationLevel();
            }
            if (_studentService is not null)
            {
                List<Student> studentNoEducationLevel = await _studentService.SelectAllStudentNoEducationLevelAsync();
                StudentNoEducationLevel = new ObservableCollection<Student>(studentNoEducationLevel);
            }
        }
    }
}

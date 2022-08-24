using Prism.Commands;

namespace Learn.AvaloniaProgressRing.ViewModels
{
  public class ShellWindowViewModel : ViewModelBase
  {
    private bool _isLoading;

    public ShellWindowViewModel()
    {
      Title = "Sample Prism.Avalonia - Navigation";

      IsLoading = true;
    }

    DelegateCommand CmdStart => new DelegateCommand(() =>
    {
      IsLoading = true;
    });

    DelegateCommand CmdStop => new DelegateCommand(() =>
    {
      IsLoading = false;
    });

    public bool IsLoading
    {
      get => _isLoading;
      set => SetProperty(ref _isLoading, value);
    }
  }
}

namespace UexCorpDataRunner.DesktopClient.Services;

public interface INavigationService
{
    Task NavigateBack();
    Task NavigateToActiveInterface();
    Task NavigateToHiddenInterface();
}

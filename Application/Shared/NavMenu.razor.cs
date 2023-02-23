using Microsoft.AspNetCore.Components;

namespace JoensuuWebAssemblyApp.Shared;

public partial class NavMenu
{
    private bool collapseNavMenu = true;
    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
	[Inject] NavigationManager NavigationManager { get; set; } = default!;
	public string CurrUrl = "";
	private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }


	protected override void OnInitialized()
	{
		CurrUrl = NavigationManager.Uri.Replace(NavigationManager.BaseUri, "");
		Console.WriteLine(CurrUrl);
	}
}
using Microsoft.AspNetCore.Components.Forms;

namespace JoensuuWebAssemblyApp.Pages;

public partial class TicketSubmission
{
	public int step { get; set; } = 1;
	public const int maxSteps = 4;

}
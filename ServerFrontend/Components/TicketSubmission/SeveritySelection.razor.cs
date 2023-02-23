using Microsoft.AspNetCore.Components;

namespace ServerFrontend.Components.TicketSubmission;

public partial class SeveritySelection
{
	public int Selected { get; set; } = 1;
	[Parameter] public Action<int> ExternalMethod { get; set; } = default!;
	public void ClickHandler(int nr)
	{
		ExternalMethod.Invoke(nr);
		Selected = nr;
	}
}
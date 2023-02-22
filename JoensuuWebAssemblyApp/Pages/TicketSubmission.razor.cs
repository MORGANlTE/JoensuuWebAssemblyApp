using Microsoft.AspNetCore.Components.Forms;

namespace JoensuuWebAssemblyApp.Pages;

public partial class TicketSubmission
{
	public int step { get; set; } = 1;
	public const int maxSteps = 4;
	public bool NextStep { get; set; } = false;
	public int Sel { get; set; } = 1;
	public void ChangeStep(int nr)
	{
		step = nr;
	}

	public void ChangeSelected(int nr)
	{
		Console.WriteLine("hi");
		Sel = nr;
		Console.WriteLine(Sel);
		Console.WriteLine(nr);
	}
}
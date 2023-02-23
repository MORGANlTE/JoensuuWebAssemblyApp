namespace ServerFrontend.Pages;

public partial class TicketSubmission
{
	public int step { get; set; } = 1;
	public string ProbableName { get; set; } = "No title";
	public const int maxSteps = 4;
	public bool NextStep { get; set; } = false;
	public int Sel { get; set; } = 1;
	public void ChangeStep(int nr)
	{
		step = nr;
	}

	public void AIDataReceived(string probableName)
	{
		Console.WriteLine("data received! " + probableName);
	}

	public void ChangeSelected(int nr)
	{
		Console.WriteLine("hi");
		Sel = nr;
		Console.WriteLine(Sel);
		Console.WriteLine(nr);
	}
}
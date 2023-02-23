using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace JoensuuWebAssemblyApp.Components.TicketSubmission;

public partial class Description
{
	[Parameter] public string Name { get; set; }

	protected override void OnInitialized()
	{
		_model.Naam = Name;
	}
	private class FormModel
	{
		public string Naam { get; set; }
		public string Description { get; set; }
	}

	private FormModel _model = new FormModel();

	private async Task OnValidSubmit()
	{

		// Do something with the form data
		// For example: save it to a database or send it in an email
		// You can access the form data using the _model object
	}
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace JoensuuWebAssemblyApp.Components.TicketSubmission;

public partial class SelectPicture
{
	[Inject] HttpClient Http { get; set; } = default!;
	private string _name { get; set; } = default!;
	[Parameter] public Action<string> ExternalMethod { get; set; } = default!;
	public string ImgBS64 = "./img/noimg.png";
	public IBrowserFile ThePicture { get; set; } = default!;

	public async Task HandleSelected(InputFileChangeEventArgs e)
	{
		try
		{
			var files = e.GetMultipleFiles();
			using var content = new MultipartFormDataContent();
			if (files.Count == 1)
			{
				foreach (var file in files)
				{
					await using MemoryStream fs = new MemoryStream();
					await file.OpenReadStream(maxAllowedSize: 1048576).CopyToAsync(fs);
					byte[] somBytes = GetBytes(fs);
					string bs64 = Convert.ToBase64String(somBytes, 0, somBytes.Length);
					ImgBS64 = $"data:{file.ContentType};base64,{bs64}";
					var fileContent = new StreamContent(file.OpenReadStream());
					fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
					content.Add(
						content: fileContent,
						name: "img",
						fileName: file.Name
					);

					var boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
					content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("boundary", boundary));


					//content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

					var response = await Http.PostAsync("/prediction", content);
					Console.WriteLine("req send");
					var results = await response.Content.ReadFromJsonAsync<List<ImageResult>>();
					Console.WriteLine("res received");
					Console.WriteLine(results);
					ExternalMethod.Invoke("test");
				}

			}
			else
			{
				throw new ArgumentException("Too many files");
			}
				
		}

		catch (Exception r)
		{
			System.Diagnostics.Debug.Print("ERROR: " + r.Message + Environment.NewLine);
		}

	}

	public class ImageResult
	{
		public string fileName { get; set; }
		public string fileSize { get; set; }
		public string label { get; set; }

	}

	public static byte[] GetBytes(Stream stream)
	{
		var bytes = new byte[stream.Length];
		stream.Seek(0, SeekOrigin.Begin);
		stream.ReadAsync(bytes, 0, bytes.Length);
		stream.Dispose();
		return bytes;
	}
}
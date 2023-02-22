using Microsoft.AspNetCore.Components.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Buffers.Text;
using System.Net.NetworkInformation;
using System;

namespace JoensuuWebAssemblyApp.Pages;

public partial class Ticket
{
	private string _name { get; set; } = default!;
	public string ImgBS64 = "./img/noimg.png";
	public IBrowserFile ThePicture { get; set; } = default!;
	public async Task HandleSelected(InputFileChangeEventArgs e)
	{
		try
		{
			var files = e.GetMultipleFiles();
			if (files.Count <= 1)
				foreach (var file in files)
				{
					await using MemoryStream fs = new MemoryStream();
					await file.OpenReadStream(maxAllowedSize: 1048576).CopyToAsync(fs);
					byte[] somBytes = GetBytes(fs);
					string bs64 = Convert.ToBase64String(somBytes, 0, somBytes.Length);
					ImgBS64 = $"data:{file.ContentType};base64,{bs64}";
					Console.WriteLine("Imatge 64: " + ImgBS64 + Environment.NewLine);
				}
		}

		catch (Exception r)
		{
			System.Diagnostics.Debug.Print("ERROR: " + r.Message + Environment.NewLine);
		}

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
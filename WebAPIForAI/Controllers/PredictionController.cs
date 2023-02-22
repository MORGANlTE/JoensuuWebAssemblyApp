using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

[ApiController]
[Route("[controller]")]
public class PredictionController : ControllerBase
{
	private readonly PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> _predictionEnginePool;

	public PredictionController(PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> predictionEnginePool)
	{
		_predictionEnginePool = predictionEnginePool;
	}

	[HttpGet]
	public async Task<IActionResult> Brioooo()
	{
		return Ok();
	}

	[HttpPost]
	public async Task<IActionResult> PredictAsync([FromForm] IFormFile img)
	{
		if (img != null && img.Length > 0)
		{
			// Get the file name and extension
			string fileName = Path.GetFileNameWithoutExtension(img.FileName);
			string extension = Path.GetExtension(img.FileName);

			// Generate a unique file name to avoid overwriting existing files
			string uniqueFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";

			// Define the directory to save the file
			string uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

			// Create the directory if it does not exist
			if (!Directory.Exists(uploadsDirectory))
			{
				Directory.CreateDirectory(uploadsDirectory);
			}

			// Combine the uploads directory and unique file name to get the full path to the file
			string filePath = Path.Combine(uploadsDirectory, uniqueFileName);

			// Copy the file to the file stream on the server
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await img.CopyToAsync(stream);
			}

			// Return a response indicating that the file was successfully saved
			return Ok(new
			{
				fileName = uniqueFileName,
				fileSize = img.Length
			});
		}
		else
		{
			// Return an error response if no file was uploaded
			return BadRequest("No file was uploaded.");
		}
	}
}

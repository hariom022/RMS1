
using Microsoft.AspNetCore.Mvc;
using OCRProj.Models;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Diagnostics;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Drawing.Imaging;


namespace OCRProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public const string folderName = "images/";
        public const string trainedDataFolderName = "tessdata";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public string Index([FromForm] OcrModel request)
        {
            string name = request.Image.FileName;
            var image = request.Image;

            if (image.Length > 0)
            {
                using (var fileStream = new FileStream(folderName + image.FileName, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            string tessPath = Path.Combine(trainedDataFolderName, "");
            string result = ""; 
            if (request.Image.FileName.Split(".")[1].ToLower() == "pdf")
            {
                result = ConvertPDFToImage(Path.Combine(folderName, image.FileName), request.DestinationLanguage);

                goto returnResult;

            }

            using (var engine = new TesseractEngine(tessPath, request.DestinationLanguage, EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(folderName + name))
                {
                    var page = engine.Process(img);
                    result = page.GetText();
                    Console.WriteLine(result);
                }
            }

            returnResult:
            return String.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;

        }

        public string ConvertPDFToImage(string filePath, string detedctedLang)
        {
            try
            {
                PdfDocument pdfDocument = new PdfDocument();
                pdfDocument.LoadFromFile(filePath);

                string returnString = "";

                for (int i = 0; i < pdfDocument.Pages.Count; i++)
                {
                    //Convert all pages to images and set the image Dpi
                    System.Drawing.Image image = pdfDocument.SaveAsImage(i, PdfImageType.Bitmap, 500, 500);

                    //Save images as PNG format to a specified folder 
                    String file = String.Format($"{folderName}\\ToImage-{0}.png", i);
                    image.Save(file, System.Drawing.Imaging.ImageFormat.Png);

                    returnString += Read_File($"ToImage-{i}.png", detedctedLang);

                }

                return returnString;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string Read_File(string fileName,string detectedLang)
        {
            try
            {

                

                string tessPath = Path.Combine(trainedDataFolderName, "");
                string result = "";

                using (var engine = new TesseractEngine(tessPath, detectedLang, EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(folderName + fileName))
                    {
                        var page = engine.Process(img);
                        result = page.GetText();
                        Console.WriteLine(result);
                    }
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SchoolProject.Service.Implementations
{
    public class FileService : IFileService
    {
        #region Fileds
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion
        #region Constructors
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Handle Functions

        //public async Task<string> UploadImage(string Location, IFormFile file)
        //{
        //    var path = _webHostEnvironment.WebRootPath + "/" + Location + "/";
        //    var extention = Path.GetExtension(file.FileName);
        //    var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extention;
        //    if (file.Length > 0)
        //    {
        //        try
        //        {
        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }
        //            using (FileStream filestreem = File.Create(path + fileName))
        //            {
        //                await file.CopyToAsync(filestreem);
        //                await filestreem.FlushAsync();
        //                return $"/{Location}/{fileName}";
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return "FailedToUploadImage";
        //        }
        //    }
        //    else
        //    {
        //        return "NoImage";
        //    }
        //}
        public async Task<string> UploadImage(string location, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return "NoImage";
                }

                // Validate file size (max 3MB)
                const long maxSize = 3 * 1024 * 1024; // 3MB
                if (file.Length > maxSize)
                {
                    return "FileTooLarge";
                }

                // Validate file extension
                var allowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    return "InvalidFileType";
                }

                // Secure path construction
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, location);
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Ensure directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Save file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return $"/{location}/{fileName}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image: {ex.Message}");
                return "FailedToUploadImage";
            }
        }

        #endregion
    }
}
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;

namespace ApiUploadFiles.Application
{
    public class FileApplication
    {
        private readonly IWebHostEnvironment _environment;

        public FileApplication(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task CopyToFolderClienteAsync(IFormFile file, int clienteId)
        {
            try
            {
                if (file != null)
                {
                    var path = Path.Combine(_environment.ContentRootPath, "Uploads", "Clientes", clienteId.ToString("00000000"));

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var pathFile = Path.Combine(path, file.FileName);

                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task CopyToFolderSedexAsync(IFormFile file, int sedexId)
        {
            try
            {
                if (file != null)
                {
                    var path = Path.Combine(_environment.ContentRootPath, "Uploads", "Sedex", sedexId.ToString("00000000"));

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var pathFile = Path.Combine(path, file.FileName);

                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

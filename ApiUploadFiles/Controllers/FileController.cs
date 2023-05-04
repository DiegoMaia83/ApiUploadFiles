using ApiUploadFiles.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiUploadFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileApplication _fileApplication;

        public FileController(IWebHostEnvironment environment, FileApplication fileApplication)
        {
            _fileApplication = fileApplication;
        }

        [HttpPost]
        [Route("upload/cliente")]
        public async Task<IActionResult> UploadFilesCliente([FromForm] List<IFormFile> files)
        {
            try
            {
                // Pegar o id do cliente pela Claims
                var clienteId = 1234;

                var errorsUpload = new Dictionary<string, string>();
                var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".gif", ".png" };

                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length == 0)
                        {
                            errorsUpload.Add(file.FileName, "O arquivo está vazio ou é inválido.");
                            continue;
                        }

                        if (file.Length > 5000000)
                        {
                            errorsUpload.Add(file.FileName, "O tamanho do arquivo excede o limite máximo permitido.");
                            continue;
                        }

                        if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                        {
                            errorsUpload.Add(file.FileName, $"O tipo de arquivo não é suportado. Arquivos suportados ( {String.Join(", ", allowedExtensions)} ).");
                            continue;
                        }

                        await _fileApplication.CopyToFolderClienteAsync(file, clienteId);
                    }
                }

                if (errorsUpload.Count > 0)
                    return(BadRequest(errorsUpload));


                return Ok("Arquivos enviados com sucesso!");   
            }
            catch
            {
                return StatusCode(500, "Houve um erro ao processar a rotina!");
            }
         
        }

        [HttpPost]
        [Route("upload/sedex")]
        public async Task<IActionResult> UploadFilesSedex([FromForm] List<IFormFile> files, [FromForm] int sedexId)
        {
            try
            {
                var errorsUpload = new Dictionary<string, string>();
                var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".gif", ".png" };

                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length == 0)
                        {
                            errorsUpload.Add(file.FileName, "O arquivo está vazio ou é inválido.");
                            continue;
                        }

                        if (file.Length > 5000000)
                        {
                            errorsUpload.Add(file.FileName, "O tamanho do arquivo excede o limite máximo permitido.");
                            continue;
                        }

                        if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                        {
                            errorsUpload.Add(file.FileName, $"O tipo de arquivo não é suportado. Arquivos suportados ( {String.Join(", ", allowedExtensions)} ).");
                            continue;
                        }

                        await _fileApplication.CopyToFolderSedexAsync(file, sedexId);
                    }
                }

                if (errorsUpload.Count > 0)
                    return (BadRequest(errorsUpload));


                return Ok("Arquivos enviados com sucesso!");
            }
            catch
            {
                return StatusCode(500, "Houve um erro ao processar a rotina!");
            }
        }
    }
}

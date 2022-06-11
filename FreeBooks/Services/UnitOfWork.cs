// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :::::: I N S T I T U T O :: P O L I T É C N I C O :: D E :: T O M A R ::::::
// ::::::::::::::::: E N G E N H A R I A :: I N F O R M Á T I C A :::::::::::::
// :::::::::::::: D E S E N V O L V I M E N T O :: W E B :: 2021/2022 :::::::::
// ::::::::::::::::::::::::::::::: Copyright(C) :::::::::::::::::::::::::::::::
// :::::::::: aluno19169@ipt.pt :::::::::::::: aluno21425@ipt.pt ::::::::::::::
// ::: https://github.com/dplt1992 https://github.com/Flavio-Oliveira-21425 :::
// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// ////////////////////////////////////////////////////////////////////////////

using FreeBooks.Infrastructure;

namespace FreeBooks.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Diretoria wwwroot na aplicação
        /// </summary>
        private IWebHostEnvironment _webHostEnvironment;

        public UnitOfWork(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// Recebe, processa e guarda o ficheiro no servidor
        /// </summary>
        /// <param name="file">Ficheiro recebido do browser</param>
        public async void SaveFile(IFormFile file)
        {
            long totalBytes = file.Length;
            string fileName = file.FileName.Trim('"');
            string serverPath = _webHostEnvironment.WebRootPath + "\\Images\\";

            if (fileName.Contains("\\"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            }

            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            byte[] buffer = new byte[16 * 1024];

            using (FileStream output = System.IO.File.Create(serverPath + fileName))
            {
                using (Stream input = file.OpenReadStream())
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, readBytes);
                        totalBytes += readBytes;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SerPro.Core.Entity;
using SerPro.Core.IManagers;
using SerPro.Core.Managers.Picture;

namespace SerPro.Core.Managers
{
    public class PictureManager : IPictureManager
    {

        private string WorkingFolder { get; set; }

        public PictureManager()
        {

        }

        public PictureManager(string workingFolder)
        {
            this.WorkingFolder = workingFolder;
            CheckTargetDirectory();
        }

        public async Task<IEnumerable<PictureView>> Get()
        {
            List<PictureView> pictures = new List<PictureView>();

            DirectoryInfo picFolder = new DirectoryInfo(this.WorkingFolder);

            await Task.Factory.StartNew(() =>
            {
                pictures = picFolder.EnumerateFiles()
                                            .Where(fi => new[] { ".jpg", ".bmp", ".png", ".gif", ".tiff" }.Contains(fi.Extension.ToLower()))
                                            .Select(fi => new PictureView
                                            {
                                                Name = fi.Name,
                                                Created = fi.CreationTime,
                                                Modified = fi.LastWriteTime,
                                                Size = fi.Length / 1024
                                            })
                                            .ToList();
            });

            return pictures;
        }

        public async Task<PictureActionResult> Delete(string fileName)
        {
            try
            {
                var filePath = Directory.GetFiles(this.WorkingFolder, fileName)
                                .FirstOrDefault();

                await Task.Factory.StartNew(() =>
                {
                    File.Delete(filePath);
                });

                return new PictureActionResult { Successful = true, Message = fileName + "deleted successfully" };
            }
            catch (Exception ex)
            {
                return new PictureActionResult { Successful = false, Message = "error deleting fileName " + ex.GetBaseException().Message };
            }
        }

        public async Task<byte[]> GetImagebyte(HttpRequestMessage request)
        {
            byte[] bytes = null;

            var provider = new PictureMultipartFormDataStreamProvider(this.WorkingFolder);

            await request.Content.ReadAsMultipartAsync(provider);

            foreach (var file in provider.FileData)
            {
                var fileInfo = new FileInfo(file.LocalFileName);

                bytes = System.IO.File.ReadAllBytes(file.LocalFileName);

            }

            return bytes;

        }

        public async Task<IEnumerable<PictureView>> Add(HttpRequestMessage request)
        {
            var provider = new PictureMultipartFormDataStreamProvider(this.WorkingFolder);

            await request.Content.ReadAsMultipartAsync(provider);

            //string targetPath = @"C:\Users\Public\TestFolder\SubDir";


            var pictures = new List<PictureView>();

            foreach (var file in provider.FileData)
            {
                var fileInfo = new FileInfo(file.LocalFileName);

                byte[] bytes = System.IO.File.ReadAllBytes(file.LocalFileName);

                pictures.Add(new PictureView
                {
                    Name = fileInfo.Name,
                    Created = fileInfo.CreationTime,
                    Modified = fileInfo.LastWriteTime,
                    Size = fileInfo.Length / 1024
                });
            }

            return pictures;
        }

        public bool FileExists(string fileName)
        {
            var file = Directory.GetFiles(this.WorkingFolder, fileName)
                                .FirstOrDefault();

            return file != null;
        }

        private void CheckTargetDirectory()
        {
            if (!Directory.Exists(this.WorkingFolder))
            {
                throw new ArgumentException("the destination path " + this.WorkingFolder + " could not be found");
            }
        }
    }
}
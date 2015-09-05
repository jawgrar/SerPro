using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SerPro.Core.Entity;
using SerPro.Core.IManagers;

namespace SerPro.Core.Managers
{
    public class PhotoManager : IPhotoManager
    {

        private string WorkingFolder { get; set; }

        public PhotoManager()
        {

        }

        public PhotoManager(string workingFolder)
        {
            this.WorkingFolder = workingFolder;
            CheckTargetDirectory();
        }

        public async Task<IEnumerable<PhotoViewModel>> Get()
        {
            List<PhotoViewModel> photos = new List<PhotoViewModel>();

            DirectoryInfo photoFolder = new DirectoryInfo(this.WorkingFolder);

            await Task.Factory.StartNew(() =>
            {
                photos = photoFolder.EnumerateFiles()
                                            .Where(fi => new[] { ".jpg", ".bmp", ".png", ".gif", ".tiff" }.Contains(fi.Extension.ToLower()))
                                            .Select(fi => new PhotoViewModel
                                            {
                                                Name = fi.Name,
                                                Created = fi.CreationTime,
                                                Modified = fi.LastWriteTime,
                                                Size = fi.Length / 1024
                                            })
                                            .ToList();
            });

            return photos;
        }

        public async Task<PhotoActionResult> Delete(string fileName)
        {
            try
            {
                var filePath = Directory.GetFiles(this.WorkingFolder, fileName)
                                .FirstOrDefault();

                await Task.Factory.StartNew(() =>
                {
                    File.Delete(filePath);
                });

                return new PhotoActionResult { Successful = true, Message = fileName + "deleted successfully" };
            }
            catch (Exception ex)
            {
                return new PhotoActionResult { Successful = false, Message = "error deleting fileName " + ex.GetBaseException().Message };
            }
        }

        public async Task<IEnumerable<PhotoViewModel>> Add(HttpRequestMessage request)
        {
            var provider = new PhotoMultipartFormDataStreamProvider(this.WorkingFolder);

            await request.Content.ReadAsMultipartAsync(provider);

            //string targetPath = @"C:\Users\Public\TestFolder\SubDir";


            var photos = new List<PhotoViewModel>();

            foreach (var file in provider.FileData)
            {
                var fileInfo = new FileInfo(file.LocalFileName);

                photos.Add(new PhotoViewModel
                {
                    Name = fileInfo.Name,
                    Created = fileInfo.CreationTime,
                    Modified = fileInfo.LastWriteTime,
                    Size = fileInfo.Length / 1024
                });
            }

            return photos;
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
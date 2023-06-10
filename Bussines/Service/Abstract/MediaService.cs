using Bussines.Service.Concrete;
using DataAccess.Models;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Repositories.Concrete;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Abstract;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;

namespace Bussines.Service.Abstract
{
    public class MediaService : IMediaService
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly IGenericRepository<Media> _genRepository;
        private readonly IConfiguration _config;
        public MediaService(IGenericRepository<Media> genericRepository, IConfiguration config)
        {
            _genRepository = genericRepository;
            _config = config;
        }
        public async Task<Media> SaveMedia(IFormFile formFile)
        {
          
            try
            {
                var todayDate = DateTime.Now.ToString("yyyyMMdd");
                var todayTime = DateTime.Now.ToString("HHmmss");
                var rootPath = _config["MediaStorage:FileRootPath"];
                var filePath = $"capella/{todayDate}/{todayTime}";
                var fullPath = $"{rootPath}/{filePath}";
                var filenamehash = new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());
                Media media = new Media();
                media.RealFilename = formFile.FileName;
                media.EncodedFilename = filenamehash;
                media.Extension = Path.GetExtension(formFile.FileName);
                media.FilePath = filePath;
                media.RootPath = rootPath;
                media.Size = formFile.Length;
                media.Mime = formFile.ContentType;
                var absolutePath = $"{filePath}/{filenamehash}{Path.GetExtension(formFile.FileName)}";
                media.AbsolutePath = absolutePath;
                media.ServePath = absolutePath;
                await _genRepository.Add(media);
                return media;
            }
            catch (Exception error)
            {

                throw error;
            }
           
        }
    }
}

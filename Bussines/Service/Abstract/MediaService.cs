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
using Bussines.DTO;
using AutoMapper;

namespace Bussines.Service.Abstract
{
    public class MediaService : IMediaService
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly IGenericRepository<Media> _genRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public MediaService(IGenericRepository<Media> genericRepository, IConfiguration config,IMapper mapper)
        {
            _genRepository = genericRepository;
            _mapper = mapper;
            _config = config;
        }
        public async Task<Media> SaveMedia(IFormFile formFile)
        {
            try
            {
                var todayDate = DateTime.Now.ToString("yyyyMMdd");
                var todayTime = DateTime.Now.ToString("HHmmss");
                var rootPath = _config["MediaStorage:FileRootPath"];

                var filePath = $"myapp/{todayDate}/{todayTime}";
                var fullPath = $"{rootPath}/{filePath}";
                var filenamehash = new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());
                Media media = new Media
                {
                    RealFilename = formFile.FileName,
                    EncodedFilename = filenamehash,
                    FilePath = filePath,
                    RootPath = rootPath,
                    AbsolutePath = $"{filePath}/{filenamehash}{Path.GetExtension(formFile.FileName)}",
                    Size = formFile.Length,
                    Deleted = false,
                };
                var resultMedia = await _genRepository.AddModel(media);
                return resultMedia;
            }
            catch (Exception error)
            {

                throw error;
            }
           
        }

        public async Task<Media> SaveUserMedia(IFormFile formFile, int id)
        {
            try
            {
                var todayDate = DateTime.Now.ToString("yyyyMMdd");
                var todayTime = DateTime.Now.ToString("HHmmss");
                var rootPath = _config["MediaStorage:FileRootPath"];

                var filePath = $"myapp/{todayDate}/{todayTime}";
                var fullPath = $"{rootPath}/{filePath}";
                var filenamehash = new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());
                Media media = new Media
                {
                    RealFilename = formFile.FileName,
                    EncodedFilename = filenamehash,
                    FilePath = filePath,
                    RootPath = rootPath,
                    AbsolutePath = $"{filePath}/{filenamehash}{Path.GetExtension(formFile.FileName)}",
                    Size = formFile.Length,
                    Deleted = false,
                };
                var haveMedia = _genRepository.GetWhere(x => x.userId == id).FirstOrDefault();
                if (haveMedia != null)
                {
                    haveMedia.RealFilename = media.RealFilename;
                    haveMedia.EncodedFilename = media.EncodedFilename;
                    haveMedia.FilePath = media.FilePath;
                    haveMedia.RootPath = media.RootPath;
                    haveMedia.AbsolutePath = media.AbsolutePath;
                    haveMedia.Size = media.Size;
                    haveMedia.Deleted = media.Deleted;

                    var resultMedia = await _genRepository.UpdateModel(haveMedia);
                    return resultMedia;
                }
                else
                {
                    var resultMedia = await _genRepository.AddModel(media);
                    return resultMedia;
                }



            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public async Task<Media> SaveTeamMedia(IFormFile formFile, int id)
        {
            try
            {
                var todayDate = DateTime.Now.ToString("yyyyMMdd");
                var todayTime = DateTime.Now.ToString("HHmmss");
                var rootPath = _config["MediaStorage:FileRootPath"];

                var filePath = $"myapp/{todayDate}/{todayTime}";
                var fullPath = $"{rootPath}/{filePath}";
                var filenamehash = new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());

                Directory.CreateDirectory(fullPath);
                using (var stream = new FileStream(Path.Combine(fullPath, formFile.FileName), FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                Media media = new Media
                {
                    RealFilename = formFile.FileName,
                    EncodedFilename = filenamehash,
                    FilePath = filePath,
                    RootPath = rootPath,
                    AbsolutePath = $"{filePath}/{filenamehash}{Path.GetExtension(formFile.FileName)}",
                    Size = formFile.Length,
                    Deleted = false,
                };
                var haveMedia = _genRepository.GetWhere(x => x.teamId == id).FirstOrDefault();
                if (haveMedia != null)
                {
                    haveMedia.RealFilename = media.RealFilename;
                    haveMedia.EncodedFilename = media.EncodedFilename;
                    haveMedia.FilePath = media.FilePath;
                    haveMedia.RootPath = media.RootPath;
                    haveMedia.AbsolutePath = media.AbsolutePath;
                    haveMedia.Size = media.Size;
                    haveMedia.Deleted = media.Deleted;

                    var resultMedia = await _genRepository.UpdateModel(haveMedia);
                    return resultMedia;
                }
                else
                {
                    var resultMedia = await _genRepository.AddModel(media);
                    return resultMedia;
                }



            }
            catch (Exception error)
            {

                throw error;
            }
        }

        
        
    }
}

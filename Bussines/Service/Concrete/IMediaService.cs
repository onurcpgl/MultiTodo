using Bussines.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Service.Concrete
{
    public interface IMediaService
    {
        Task<Media> SaveMedia(IFormFile formFile);
        Task<Media> SaveUserMedia(IFormFile formFile, int id);
        Task<Media> SaveTeamMedia(IFormFile formFile, int id);
    }
}

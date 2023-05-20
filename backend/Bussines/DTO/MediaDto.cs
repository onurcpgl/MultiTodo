using DataAccess.Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.DTO
{
    public class MediaDto
    {
        public int? id { get; set; }
        public string RealFilename { get; set; }
        public string? EncodedFilename { get; set; }
        public string FilePath { get; set; }
        public string RootPath { get; set; }
        public string ServePath { get; set; }
        public string AbsolutePath { get; set; }
        public string Mime { get; set; }
        public string? Extension { get; set; }
        public int? teamId { get; set; }
        public TeamDto? team { get; set; }
        public int? userId { get; set; }
        public UserDto? user { get; set; }
    }
}

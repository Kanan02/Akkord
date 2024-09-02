using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Response
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public string Base64 { get; set; }
        public PhotoDto() { }

        public PhotoDto(Photo photo, string path = null)
        {
            Id = photo.Id;
            Src = path!=null ? $"{path}{photo.Src}" : photo.Src;
            Base64 = photo.Base64;
        }
    }
}

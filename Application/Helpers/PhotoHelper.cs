using Application.Config;
using Application.Entities;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Helpers
{
    public class PhotoHelper
    {

        public static void SaveFiles(List<Photo> photo) => photo.ForEach(p => SaveToFile(p));
        public static void DeleteFiles(List<Photo> photo) => photo.ForEach(p => DeleteFile(p));

        //public static void SaveToFile(Photo old, Photo @new)
        //{
        //    old.Base64 = @new.Base64;
        //    SaveToFile(old);
        //}

        public static void SaveToFile(Photo photo)
        {

            if (photo!=null && !string.IsNullOrEmpty(photo.Base64))
            {
                SetSrc(photo);
                var base64 = photo.Base64.Split(',')[1];
                var bytes = Convert.FromBase64String(base64);
                var photoUrl = $"{ProjectSetting.PhotoSavePath}{photo.Src}";
                try
                {
                    using (var imageFile = new FileStream(photoUrl, FileMode.OpenOrCreate))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                }
                catch (Exception ex)
                {

                    throw new AkkordException($"Akkord add photo file error. Path : {photoUrl}; ex {ex}");
                }
                
            }
        }

        public static void DeleteFile(Photo photo)
        {
            var photoUrl = $"{ProjectSetting.PhotoSavePath}{photo.Src}";
            try
            {
                File.Delete(photoUrl);
            }
            catch (Exception ex)
            {
                throw new AkkordException($"Akkord delete photo file error. Path : {photoUrl}; ex {ex}");
            }
                
        }

        private static void SetSrc(Photo photo)
        {
            if (photo.Id == 0)
                photo.Src = $"{Guid.NewGuid()}.jpg";
        }
    }
}

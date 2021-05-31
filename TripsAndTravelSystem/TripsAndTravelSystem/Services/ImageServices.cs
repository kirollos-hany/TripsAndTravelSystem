using System;
using TripsAndTravelSystem.Models;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace TripsAndTravelSystem.Services
{
    public class ImageServices
    {
        private ImageFormat GetImageFormat(string extension)
        {
            extension = extension.ToLower();
            if (extension.Equals("jpg") || extension.Equals("jpeg"))
            {
                return ImageFormat.Jpeg;
            }else if (extension.Equals("png"))
            {
                return ImageFormat.Png;
            }else if (extension.Equals("tiff"))
            {
                return ImageFormat.Tiff;
            }else if (extension.Equals("bmp"))
            {
                return ImageFormat.Bmp;
            }
            return null;
        }
        public async Task<string> SavePhoto(string encodedImage, string extension,bool isTripImage, string role, HttpServerUtilityBase Server)
        {
            byte[] imageBytes = Convert.FromBase64String(encodedImage);
            Image img = null;
            ImageFormat imgFormat = GetImageFormat(extension);
            if(imgFormat == null)
            {
                return null;
            }
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                try
                {
                    await Task.Run(() => {
                        img = Image.FromStream(ms);
                    });
                    string image = UploadPhoto(extension, isTripImage, role, Server);
                    await Task.Run(() => img.Save(image, imgFormat));
                    return image;
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
        }

        private  string UploadPhoto(string extension, bool isTripImage, string role, HttpServerUtilityBase Server)
        {
                string fileName = Guid.NewGuid().ToString() +"."+ extension;
                string path = "";
                if (role.Equals(User.UserRoles.Agency.ToString()) && !isTripImage)
                {
                    path = Path.Combine(Server.MapPath("~/images/UploadedPhotos/AgencyPhotos"));
                }
                else if (isTripImage && role.Equals(User.UserRoles.Agency.ToString()))
                {
                    path = Path.Combine(Server.MapPath("~/images/UploadedPhotos/PostPhotos"));
                }
                else if (role.Equals(User.UserRoles.Admin.ToString()))
                {
                    path = Path.Combine(Server.MapPath("~/images/UploadedPhotos/AdminPhotos"));
                }
                else if (role.Equals(User.UserRoles.Traveler.ToString()))
                {
                    path = Path.Combine(Server.MapPath("~/images/UploadedPhotos/TravelerPhotos"));
                }
                return Path.Combine(path, fileName);
        }
        

        public async Task DeleteOldPhoto(string oldPhotoPath)
        {
            if (File.Exists(oldPhotoPath))
            {
                await Task.Run(() => File.Delete(oldPhotoPath));
            }
        }
        
    }
}
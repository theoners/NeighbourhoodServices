using System.Collections.Generic;

namespace NeighbourhoodServices.Common
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryApp
    {
        public static async Task<string> UploadFileAsync(Cloudinary cloudinary, IFormFile file)
        {
            using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            var destinationFile = memoryStream.ToArray();

            using var destinationStream = new MemoryStream(destinationFile);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, destinationStream),
                Transformation = new Transformation().Width(200).Height(200),

            };
            var result = await cloudinary.UploadAsync(uploadParams);

            return result.Uri.AbsoluteUri;
        }

        public static async Task DeleteFile(Cloudinary cloudinary, string url)
        {
            var publicId = GetCloudinaryPublicIdFromUrl(url);

            var deletionParams = new DeletionParams(publicId);
            await cloudinary.DestroyAsync(deletionParams);
        }

        private static string GetCloudinaryPublicIdFromUrl(string url)
        {
            var startIndex = url.IndexOf('/') + 1;
            var length = url.LastIndexOf('.') - startIndex;
            var publicId = url.Substring(startIndex, length);

            return publicId;
        }
    }
}
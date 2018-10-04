using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Helpers
{
    public class AzureBlobHelper
    {
        public AzureBlob AzureBlob;

        public AzureBlobHelper()
        {
            AzureBlob = new AzureBlob();
        }

        public async void SetPermissionsToPublic()
        {
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await AzureBlob.CloudBlobContainer.SetPermissionsAsync(permissions);
        }

        public string GetImageUri(string imageName)
        {
            AzureBlob.CloudBlockBlob = AzureBlob.CloudBlobContainer.GetBlockBlobReference(imageName);
            return AzureBlob.CloudBlockBlob.Uri.ToString();
        }
    }
}
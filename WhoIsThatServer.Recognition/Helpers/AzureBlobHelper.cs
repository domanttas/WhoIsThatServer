using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WhoIsThatServer.Recognition.Models;

namespace WhoIsThatServer.Recognition.Helpers
{
    public class AzureBlobHelper : IAzureBlobHelper
    {
        public AzureBlob AzureBlob;

        public AzureBlobHelper()
        {
            AzureBlob = new AzureBlob();
        }

        ///<inheritdoc/>
        public async void SetPermissionsToPublic()
        {
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await AzureBlob.CloudBlobContainer.SetPermissionsAsync(permissions);
        }

        ///<inheritdoc/>
        public string GetImageUri(string imageName)
        {
            AzureBlob.CloudBlockBlob = AzureBlob.CloudBlobContainer.GetBlockBlobReference(imageName);
            return AzureBlob.CloudBlockBlob.Uri.ToString();
        }

        ///<inheritdoc/>
        public async void DeletePhoto(string name)
        {
            var blob = AzureBlob.CloudBlobContainer.GetBlockBlobReference(name);
            await blob.DeleteIfExistsAsync();
        }
    }
}
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoIsThatServer.Storage.Models
{
    public class AzureBlob
    {
        public string StorageConnectionString { get; set; }
        public CloudStorageAccount StorageAccount { get; set; }
        public CloudBlobContainer CloudBlobContainer { get; set; }
        public CloudBlobClient CloudBlobClient { get; set; }
        public CloudBlockBlob CloudBlockBlob { get; set; }

        public AzureBlob()
        {
            //Getting storage string to Azure Storage
            StorageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");

            //Parse storage account
            StorageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            //Create the CloudBlobClient that represents the Blob storage endpoint for the storage account
            CloudBlobClient = StorageAccount.CreateCloudBlobClient();

            //Get container's 'images' reference
            CloudBlobContainer = CloudBlobClient.GetContainerReference("images");
        }
    }
}
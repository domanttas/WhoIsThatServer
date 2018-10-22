using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoIsThatServer.Recognition.Controllers;
using WhoIsThatServer.Recognition.Recognition.RecUtils;
using WhoIsThatServer.Recognition.Helpers;
using System.IO;
using WhoIsThatServer.Recognition.Models;

namespace WhoIsThatServer.Recognition.Recognition
{
    public class RecognitionServices
    {
        private const string _subscriptionKey = "b7923707fb7f44d091bb8d86d47a2988";
        private const string _recognitionApi = "https://northeurope.api.cognitive.microsoft.com/face/v1.0/";
        private const string _groupId = "people";

        private FaceServiceClient _faceServiceClient;
        private DatabaseController _databaseController;

        public RecognitionServices()
        {
            _faceServiceClient = new FaceServiceClient(_subscriptionKey, _recognitionApi);
            _databaseController = new DatabaseController();
        }

        /// <summary>
        /// Creates person group in Azure face recognition API
        /// </summary>
        /// <returns>boolean</returns>
        public async Task<bool> CreateGroup()
        {
            var people = await _databaseController.GetImageObjects();

            try
            {
                await _faceServiceClient.CreatePersonGroupAsync(_groupId, "friends");
            }

            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            foreach (var person in people)
            {
                CreatePersonResult result = await _faceServiceClient.CreatePersonAsync(_groupId, person.PersonFirstName);

                await _faceServiceClient.AddPersonFaceAsync(_groupId, result.PersonId, RecUtil.GetStreamFromUri(person.ImageContentUri));
            }

            await _faceServiceClient.TrainPersonGroupAsync(_groupId);

            TrainingStatus trainingStatus = null;
            while (true)
            {
                trainingStatus = await _faceServiceClient.GetPersonGroupTrainingStatusAsync(_groupId);

                if (trainingStatus.Status != Status.Running)
                {
                    break;
                }

                await Task.Delay(1000);
            }

            return true;
        }

        /// <summary>
        /// Identifies person from temporary image taken from App
        /// </summary>
        /// <returns>Name of identified person</returns>
        public async Task<string> Identify()
        {
            //This is needed only for first time, left it for reference
            //var isSuccessful = await CreateGroup();

            var azureBlobHelper = new AzureBlobHelper();
            string takenImageUri = null;

            takenImageUri = azureBlobHelper.GetImageUri("temp.jpg");
            if (takenImageUri == null)
                throw new ArgumentException("Photo was not successfully taken!");

            var memoryStream = new MemoryStream();
            memoryStream = RecUtil.GetStreamFromUri(takenImageUri);

            var faces = await _faceServiceClient.DetectAsync(memoryStream);

            if (faces.Length == 0 || faces == null)
                return "No faces were detected!";

            var faceIds = faces.Select(face => face.FaceId).ToArray();

            var results = await _faceServiceClient.IdentifyAsync(_groupId, faceIds);
            
            if (results.Length == 0 || results == null || results[0].Candidates.Length == 0 || results[0].Candidates[0] == null)
                return "No one was indetified!";

            var candidateId = results[0].Candidates[0].PersonId;
            var person = await _faceServiceClient.GetPersonAsync(_groupId, candidateId);

            azureBlobHelper.DeletePhoto("temp.jpg");

            return person.Name;
        }

        //TODO: checks if inerstion was successful

        /// <summary>
        /// Inserts new image into person group
        /// </summary>
        /// <param name="imageModel">Object to insert into person group</param>
        /// <returns>boolean</returns>
        public async Task<bool> InsertPersonInToGroup(ImageModel imageModel)
        {
            CreatePersonResult result = await _faceServiceClient.CreatePersonAsync(_groupId, imageModel.PersonFirstName);

            await _faceServiceClient.AddPersonFaceAsync(_groupId, result.PersonId, RecUtil.GetStreamFromUri(imageModel.ImageContentUri));

            await _faceServiceClient.TrainPersonGroupAsync(_groupId);

            TrainingStatus trainingStatus = null;
            while (true)
            {
                trainingStatus = await _faceServiceClient.GetPersonGroupTrainingStatusAsync(_groupId);

                if (trainingStatus.Status != Status.Running)
                {
                    break;
                }

                await Task.Delay(1000);
            }

            return true;
        }
    }
}

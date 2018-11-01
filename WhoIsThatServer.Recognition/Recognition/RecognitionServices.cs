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
using WhoIsThatServer.Recognition.Exceptions;
using WhoIsThatServer.Recognition.ErrorMessages;

namespace WhoIsThatServer.Recognition.Recognition
{
    public class RecognitionServices
    {
        private const string _subscriptionKey = "b7923707fb7f44d091bb8d86d47a2988";
        private const string _recognitionApi = "https://northeurope.api.cognitive.microsoft.com/face/v1.0/";
        private const string _groupId = "game";

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

            await _faceServiceClient.CreatePersonGroupAsync(_groupId, "fun");
            
            foreach (var person in people)
            {
                CreatePersonResult result = await _faceServiceClient.CreatePersonAsync(_groupId, person.Id.ToString());

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
            var azureBlobHelper = new AzureBlobHelper();
            string takenImageUri = null;

            takenImageUri = azureBlobHelper.GetImageUri("temp.jpg");
            if (takenImageUri == null)
                throw new ManagerException(RecognitionErrorMessages.WrongUriError);

            var memoryStream = new MemoryStream();
            memoryStream = RecUtil.GetStreamFromUri(takenImageUri);

            var faces = await _faceServiceClient.DetectAsync(memoryStream);

            if (faces.Length == 0 || faces == null)
                throw new ManagerException(RecognitionErrorMessages.NoFacesFoundError);

            var faceIds = faces.Select(face => face.FaceId).ToArray();

            var results = await _faceServiceClient.IdentifyAsync(_groupId, faceIds);

            if (results.Length == 0 || results == null || results[0].Candidates.Length == 0 || results[0].Candidates[0] == null)
                throw new ManagerException(RecognitionErrorMessages.NoOneIdentifiedError);

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
            CreatePersonResult result = await _faceServiceClient.CreatePersonAsync(_groupId, imageModel.Id.ToString());

            if (result == null)
                throw new ManagerException(RecognitionErrorMessages.PersonNotCreatedError);

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

        /// <summary>
        /// Detects features of face
        /// </summary>
        /// <param name="imageModel">Object which is sent from frontend</param>
        /// <returns>Age, gender of face and whether glasses and facialhair are present</returns>
        public async Task<FaceFeaturesModel> DetectFeaturesOfFace(ImageModel imageModel)
        {
            try
            {
                var faces = await _faceServiceClient.DetectAsync(RecUtil.GetStreamFromUri(imageModel.ImageContentUri), false, true, new FaceAttributeType[] {
                    FaceAttributeType.Age,
                    FaceAttributeType.Gender
                });

                return new FaceFeaturesModel()
                {
                    PersonId = imageModel.Id,
                    Age = Convert.ToInt32(faces.ElementAt(0).FaceAttributes.Age),
                    Gender = faces.ElementAt(0).FaceAttributes.Gender
                };
            }

            catch (ArgumentNullException argumentNullException)
            {
                throw new ManagerException(RecognitionErrorMessages.NoFacesFoundError);
            }

            catch (ArgumentOutOfRangeException outOfRangeException)
            {
                throw new ManagerException(RecognitionErrorMessages.NoFacesFoundError);
            }
        }
    }
}

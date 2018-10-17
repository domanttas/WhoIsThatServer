using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Recognition.Utils;

namespace WhoIsThatServer.Storage.Recognition
{
    public class RecognitionServices
    {
        private const string _subscriptionKey = "2fa6b187506040b5834d6999b3bd58b2";
        private const string _recognitionApi = "https://northeurope.api.cognitive.microsoft.com/face/v1.0/";
        private const string _groupId = "People";

        public FaceServiceClient _faceServiceClient;
        private DatabaseImageElementHelper _databaseImageElementHelper;

        public RecognitionServices()
        {
            _faceServiceClient = new FaceServiceClient(_subscriptionKey, _recognitionApi);
            _databaseImageElementHelper = new DatabaseImageElementHelper();
        }

        public async Task<bool> CreateGroup()
        {
            var people = _databaseImageElementHelper.GetAllImages();

            foreach (var person in people)
            {
                CreatePersonResult tempPerson = await _faceServiceClient.CreatePersonInPersonGroupAsync(_groupId, person.PersonFirstName);

                try
                {
                    await _faceServiceClient.AddPersonFaceInPersonGroupAsync(_groupId, tempPerson.PersonId, RecUtil.GetStreamFromUri(person.ImageContentUri));
                }

                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    return false;
                }
            }

            try
            {
                await _faceServiceClient.TrainPersonGroupAsync(_groupId);
                return true;
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        public async Task<Face[]> DetectFacesInPhoto(Stream stream)
        {
            var faces = await _faceServiceClient.DetectAsync(stream);
            return faces.ToArray();
        }

        public async Task<string> Identify()
        {
            var azureBlobHelper = new AzureBlobHelper();
            string takenImageUri = null;

            takenImageUri = azureBlobHelper.GetImageUri("temp.jpg");
            if (takenImageUri == null)
                throw new ArgumentException("Photo was not successfully taken!");

            bool isSuccess = await CreateGroup();
            if (!isSuccess)
                return null;

            Face[] faces = await DetectFacesInPhoto(RecUtil.GetStreamFromUri(takenImageUri));
            var faceIds = faces.Select(face => face.FaceId).ToArray();

            foreach (var identifyResult in await _faceServiceClient.IdentifyAsync(_groupId, faceIds))
            {
                if (identifyResult.Candidates.Length != 0)
                {
                    var candidateId = identifyResult.Candidates[0].PersonId;
                    var person = await _faceServiceClient.GetPersonInPersonGroupAsync(_groupId, candidateId);
                    return person.Name;
                }
            }

            return null;
        }
        /*
        public async Task<bool> CreateGroup()
        {
            await _faceServiceClient.CreatePersonGroupAsync(_groupId, "Friends");

            var people = _databaseImageElementHelper.GetAllImages();

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

        public async Task<string> Identify()
        {
            var isSuccessful = await CreateGroup();

            var azureBlobHelper = new AzureBlobHelper();
            string takenImageUri = null;

            takenImageUri = azureBlobHelper.GetImageUri("temp.jpg");
            if (takenImageUri == null)
                throw new ArgumentException("Photo was not successfully taken!");

            var memoryStream = new MemoryStream();
            memoryStream = RecUtil.GetStreamFromUri(takenImageUri);

            var faces = await _faceServiceClient.DetectAsync(memoryStream);
            var faceIds = faces.Select(face => face.FaceId).ToArray();

            var results = await _faceServiceClient.IdentifyAsync(_groupId, faceIds);

            if (results.Length == 0)
                return "No one was indetified!";

            var candidateId = results[0].Candidates[0].PersonId;
            var person = await _faceServiceClient.GetPersonAsync(_groupId, candidateId);

            azureBlobHelper.DeletePhoto("temp.jpg");

            return person.Name;
        }
        */
    }
}
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Recognition.Utils;

namespace WhoIsThatServer.Storage.Recognition
{
    public class RecognitionServices
    {
        private const string _subscriptionKey = "b7923707fb7f44d091bb8d86d47a2988";
        private const string _recognitionApi = "https://northeurope.api.cognitive.microsoft.com/face/v1.0";
        private const string _groupId = "People";

        private FaceServiceClient _faceServiceClient;
        private DatabaseImageElementHelper _databaseImageElementHelper;

        public RecognitionServices()
        {
            _faceServiceClient = new FaceServiceClient(_subscriptionKey);
            _databaseImageElementHelper = new DatabaseImageElementHelper();
        }

        public async void CreateGroup()
        {
            await _faceServiceClient.CreatePersonGroupAsync(_groupId, _groupId);

            var people = _databaseImageElementHelper.GetAllImages();

            foreach (var person in people)
            {
                CreatePersonResult result = await _faceServiceClient.CreatePersonAsync(_groupId, person.PersonFirstName);

                await _faceServiceClient.AddPersonFaceAsync(_groupId, result.PersonId, RecUtil.GetStreamFromUri(person.ImageContentUri));
            }

            await _faceServiceClient.TrainPersonGroupAsync(_groupId);
        }

    }
}
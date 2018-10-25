using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhoIsThatServer.Recognition.Models
{
    public class ImageModel
    {
        /// <summary>
        /// Primary key of ImageElement table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the image
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Refernce to image in storage
        /// </summary>
        public string ImageContentUri { get; set; }

        /// <summary>
        /// Name of person related with image
        /// </summary>
        public string PersonFirstName { get; set; }

        /// <summary>
        /// Last name of person related with image
        /// </summary>
        public string PersonLastName { get; set; }

        /// <summary>
        /// Sentence which describes person
        /// </summary>
        public string DescriptiveSentence { get; set; }

        /// <summary>
        /// Score of person
        /// </summary>
        public int Score { get; set; }
    }
}

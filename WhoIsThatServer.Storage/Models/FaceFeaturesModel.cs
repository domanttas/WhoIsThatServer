using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhoIsThatServer.Storage.Models
{
    [Table("FaceFeatures")]
    public class FaceFeaturesModel
    {
        /// <summary>
        /// Primary key of table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of person from DatabaseImageElements table
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Age feature
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gender feature
        /// </summary>
        public string Gender { get; set; }
    }
}
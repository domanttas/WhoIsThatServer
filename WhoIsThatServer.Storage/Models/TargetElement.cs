using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WhoIsThatServer.Storage.Models
{
    [Table("TargetElements")]
    public class TargetElement
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Person's ID which is assigned with target
        /// </summary>
        public int HunterPersonId { get; set; }

        /// <summary>
        /// Target's ID
        /// </summary>
        public int PreyPersonId { get; set; }

        /// <summary>
        /// True if hunter caught his prey
        /// </summary>
        public bool IsHunted { get; set; }
    }
}
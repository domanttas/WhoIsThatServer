using System.ComponentModel.DataAnnotations.Schema;

namespace WhoIsThatServer.Storage.Models
{
    [Table("History")]
    public class HistoryModel
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID of user
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// ID of target
        /// </summary>
        public int TargetId { get; set; }
        
        /// <summary>
        /// Constant string whether target is hunted
        /// </summary>
        public string Status { get; set; }
    }
}
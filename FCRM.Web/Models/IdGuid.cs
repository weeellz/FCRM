using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCRM.Web.Models
{
    public abstract class IdGuid
    {
        public const int DEFAULT_LENGTH = 100;
        public const int MIN_LENGTH = 50;
        public const int BYTE_LENGTH = 256;
        public const int MAX_LENGTH = 1024;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

    }
}
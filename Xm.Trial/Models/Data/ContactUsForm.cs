using System;
using System.ComponentModel.DataAnnotations;

namespace Xm.Trial.Models.Data
{
    public class ContactUsForm
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; }

        [MaxLength(150)]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTimeOffset SentDate { get; set; }

        public string ScreenshotsPath { get; set; }
    }
}
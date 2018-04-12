using System.ComponentModel.DataAnnotations;

namespace keepr_final.Models
{
    public class Keep
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string MediaUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Description { get; set; }

        public int AddedToBoard { get; set; }

        public int KeepViews { get; set; }

        public int KeepPublic {get; set;}

    }

    public class CreateKeepModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string MediaUrl { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

    }

    public class KeepsOnBoards
    {
        public string Id { get; set; }
        public string BoardId { get; set; }
        public string KeepId { get; set; }
        public string UserId { get; set; }
    }

    public class ReturnKeep
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string MediaUrl { get; set; }
        public string Description { get; set; }
        public int AddedToBoard { get; set; }
        public int KeepViews { get; set; }

    }


}
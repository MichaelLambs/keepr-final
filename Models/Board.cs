using System.ComponentModel.DataAnnotations;

namespace keepr_final.Models
{
    public class Board
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }
    }

    public class CreateBoardModel
    {
        [Required]
        public string Name { get; set; }
        public string UserId { get; set; }
    }

    
}
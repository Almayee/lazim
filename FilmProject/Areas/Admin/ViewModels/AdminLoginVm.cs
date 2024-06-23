using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilmProject.Areas.Admin.ViewModels
{
    public class AdminLoginVm
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MaxLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }

    }
}

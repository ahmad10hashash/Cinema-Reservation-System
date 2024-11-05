using Microsoft.Build.Framework;

namespace CMSpro1.Models.ViewModels
{
    public class EditViewModel
    {
        public EditViewModel()
        {
            Users = new List<string>();
        }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string Roleid { get; set; }
        public List<string> Users { get; set; }
    }
}

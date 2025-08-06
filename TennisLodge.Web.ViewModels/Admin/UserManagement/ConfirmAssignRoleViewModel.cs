using System.ComponentModel.DataAnnotations;

namespace TennisLodge.Web.ViewModels.Admin.UserManagement
{
    public class ConfirmAssignRoleViewModel
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }
} 
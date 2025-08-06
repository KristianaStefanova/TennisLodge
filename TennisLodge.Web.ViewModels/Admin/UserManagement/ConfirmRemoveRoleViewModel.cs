namespace TennisLodge.Web.ViewModels.Admin.UserManagement
{
    public class ConfirmRemoveRoleViewModel
    {
        public string UserId { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
} 
using TennisLodge.Web.ViewModels.Categories;

namespace TennisLodge.Web.ViewModels.Tournaments
{
    public class TournamentListViewModel
    {
        public int? SelectedCategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        public IEnumerable<AllTournamentsIndexViewModel> Tournaments { get; set; } = new List<AllTournamentsIndexViewModel>();
    }
}

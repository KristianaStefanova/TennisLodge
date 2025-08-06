using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TennisLodge.Data;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Accommodation;
using TennisLodge.Web.ViewModels.Admin.AccommodationManagement;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Services.Core
{
    public class AccommodationService : IAccommodationService
    {
        //private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccommodationRepository accommodationRepository;

        public AccommodationService(TennisLodgeDbContext dbContext, UserManager<ApplicationUser> userManager,
            IAccommodationRepository accommodationRepository)
        {
            //this.userManager = userManager;
            this.accommodationRepository = accommodationRepository;
        }

        public async Task<bool> AddAccommodationAsync(string userId, AccommodationCreateInputModel model)
        {
            bool operationResult = false;

            // ApplicationUser? user = await this.userManager
            //     .FindByIdAsync(userId);

            Accommodation accommodation = new Accommodation
            {
                City = model.City,
                Address = model.Address,
                MaxGuests = model.MaxGuests,
                AvailableFrom = model.AvailableFrom,
                AvailableTo = model.AvailableTo,
                Notes = model.Notes,
                HostUserId = userId,
                IsAvailable = true,
                CreatedOn = DateTime.UtcNow,
            };

            await this.accommodationRepository.AddAsync(accommodation);

            operationResult = true;

            return operationResult;
        }



        public async Task<IEnumerable<AccommodationViewModel>> GetAllAccommodationsAsync()
        {
            IEnumerable<AccommodationViewModel> accommodations = await this.accommodationRepository
                .GetAllAttached()
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .Where(a => a.IsAvailable)
                .Select(a => new AccommodationViewModel
                {
                    Id = a.Id,
                    City = a.City,
                    AvailableFrom = a.AvailableFrom,
                    AvailableTo = a.AvailableTo,
                    HostFullName = a.HostUser.FirstName + " " + a.HostUser.LastName,
                    HostUserId = a.HostUserId,
                })
                .ToListAsync();

            return accommodations;
        }

        public async Task<bool> IsAccommodationAddedFromUserAsync(string? accommodationId, string? userId)
        {
            if (string.IsNullOrEmpty(accommodationId) || string.IsNullOrEmpty(userId))
            {
                return false;
            }

            bool isValidId = int.TryParse(accommodationId, out int accommodationInt);
            if (!isValidId)
            {
                return false;
            }

            Accommodation? accommodation = await this.accommodationRepository
                    .SingleOrDefaultAsync(a => a.Id == accommodationInt &&
                                          a.HostUserId.ToLower() == userId.ToLower());

            return accommodation != null;
        }


        public AccommodationCreateInputModel GetCreateModel()
        {
            return new AccommodationCreateInputModel();
        }

        public async Task<bool> EditAccommodationAsync(AccommodationCreateInputModel inputModel)
        {
            bool result = false;

            if (string.IsNullOrEmpty(inputModel.Id) ||
                !int.TryParse(inputModel.Id, out int accommodationId))
            {
                return false;
            }

            Accommodation? editableAccommodation = await this.FindAccommodationByStringId(inputModel.Id);
            if (editableAccommodation == null)
            {
                return false;
            }


            editableAccommodation.City = inputModel.City;
            editableAccommodation.Address = inputModel.Address;
            editableAccommodation.MaxGuests = inputModel.MaxGuests;
            editableAccommodation.AvailableFrom = inputModel.AvailableFrom ?? editableAccommodation.AvailableFrom;
            editableAccommodation.AvailableTo = inputModel.AvailableTo ?? editableAccommodation.AvailableTo;
            editableAccommodation.Notes = inputModel.Notes;


            result = await this.accommodationRepository.UpdateAsync(editableAccommodation);

            return result;
        }


        public async Task<AccommodationCreateInputModel?> GetEditableAccommodationByIdAsync(string? id)
        {
            AccommodationCreateInputModel? editableAccommodation = null;

            bool isIntValid = int.TryParse(id, out int accommodationId);
            if (isIntValid)
            {
                editableAccommodation = await this.accommodationRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(a => a.Id == accommodationId)
                    .Select(a => new AccommodationCreateInputModel
                    {
                        Id = a.Id.ToString(),
                        City = a.City,
                        Address = a.Address,
                        MaxGuests = a.MaxGuests,
                        AvailableFrom = a.AvailableFrom,
                        AvailableTo = a.AvailableTo,
                        Notes = a.Notes
                    })
                    .SingleOrDefaultAsync();
            }

            return editableAccommodation;
        }

        public async Task<bool> SoftDeleteAccommodationAsync(string? id)
        {
            bool result = false;
            Accommodation? movieToDelete = await this
                .FindAccommodationByStringId(id);
            if (movieToDelete == null)
            {
                return false;
            }

            result = await this.accommodationRepository
                .DeleteAsync(movieToDelete);

            return result;
        }

        public async Task<AccommodationViewModel?> GetAccomodationDeleteDetailsByIdAsync(string? id)
        {
            AccommodationViewModel? deleteAccommodationVM = null;

            Accommodation? accommodationToBeDeleted = await this.FindAccommodationByStringId(id);
            if (accommodationToBeDeleted != null)
            {
                deleteAccommodationVM = new AccommodationViewModel()
                {
                    Id = accommodationToBeDeleted.Id,
                    City = accommodationToBeDeleted.City,
                    AvailableFrom = accommodationToBeDeleted.AvailableFrom,
                    AvailableTo = accommodationToBeDeleted.AvailableTo,
                    HostFullName = accommodationToBeDeleted.HostUser?.UserName ?? "Unknown",
                    HostUserId = accommodationToBeDeleted.HostUserId,
                    Address = accommodationToBeDeleted.Address,
                    IsOwner = false
                };
            }

            return deleteAccommodationVM;
        }

        private async Task<Accommodation?> FindAccommodationByStringId(string? id)
        {
            Accommodation? accommodation = null;

            if (!string.IsNullOrWhiteSpace(id))
            {
                bool isIntValid = int.TryParse(id, out int accommodationInt);
                if (isIntValid)
                {
                    accommodation = await this.accommodationRepository
                        .GetByIdAsync(accommodationInt);
                }
            }

            return accommodation;
        }

    }
}


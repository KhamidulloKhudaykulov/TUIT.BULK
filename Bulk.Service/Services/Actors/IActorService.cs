using Bulk.Domain.Entities;
using Bulk.Model.Actors;

namespace Bulk.Service.Services.Actors;

public interface IActorService
{
    ValueTask<ActorViewModel> CreateAsync(ActorCreateModel model);
    ValueTask<ActorViewModel> UpdateAsync(long id, ActorUpdateModel model);
    ValueTask<ActorViewModel> DeleteAsync(long id);
    ValueTask<ActorViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<ActorViewModel>> GetAllAsync();
    ValueTask<ActorViewModel> LoginAsync(string email, string password);
    ValueTask<bool> ResetPasswordAsync(string email, string newPassword);
    ValueTask<bool> SendCodeAsync(string email);
    ValueTask<bool> ConfirmCodeAsync(string email, string code);
    ValueTask<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword);
}

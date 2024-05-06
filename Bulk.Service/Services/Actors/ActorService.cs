using AutoMapper;
using Bulk.DataAccess.UnitOfWorks;
using Bulk.Domain.Entities;
using Bulk.Model.Actors;
using Bulk.Service.Exceptions;
using Bulk.Service.Heplers;

namespace Bulk.Service.Services.Actors;

public class ActorService(IUnitOfWork unitOfWork, IMapper mapper) : IActorService
{
    public async ValueTask<ActorViewModel> CreateAsync(ActorCreateModel model)
    {
        var email = await unitOfWork.Actors.SelectAsync(a => a.Email == model.Email);
        if (email is not null)
            throw new AlreadyExistException($"This admin with email={model.Email} is already exists");

        var phone = await unitOfWork.Actors.SelectAsync(a => a.Phone == model.Phone && !string.IsNullOrEmpty(a.Phone));
        if (phone is not null)
            throw new AlreadyExistException($"This admin with phone={model.Phone} is already exists");

        var createdActor = mapper.Map<Actor>(model);
        createdActor.Password = Hasher.Hash(model.Password);

        await unitOfWork.Actors.InsertAsync(mapper.Map<Actor>(createdActor));
        await unitOfWork.SaveAsync();

        return await Task.FromResult(mapper.Map<ActorViewModel>(createdActor));
    }

    public async ValueTask<ActorViewModel> UpdateAsync(long id, ActorUpdateModel model)
    {
        var existActor = await unitOfWork.Actors.SelectAsync(a => a.Id == id && !a.IsDeleted);
        if (existActor is null)
            throw new NotFoundException($"This admin with ID={id} is not found");

        existActor.Name = model.Name;
        existActor.Surname = model.Surname;
        existActor.Email = model.Email;
        existActor.Phone = model.Phone;

        await unitOfWork.Actors.UpdateAsync(existActor);
        await unitOfWork.SaveAsync();

        return mapper.Map<ActorViewModel>(existActor);
    }

    public async ValueTask<ActorViewModel> DeleteAsync(long id)
    {
        var existActor = await unitOfWork.Actors.SelectAsync(a => a.Id == id);
        if (existActor is null)
            throw new NotFoundException($"This admin with ID={id} is not found");

        await unitOfWork.Actors.UpdateAsync(existActor);
        await unitOfWork.SaveAsync();

        return mapper.Map<ActorViewModel>(existActor);
    }

    public async ValueTask<ActorViewModel> GetByIdAsync(long id)
    {
        var existActor = await unitOfWork.Actors.SelectAsync(a => a.Id == id);
        if (existActor is null)
            throw new NotFoundException($"This admin with ID={id} is not found");

        return mapper.Map<ActorViewModel>(existActor);
    }

    public async ValueTask<IEnumerable<ActorViewModel>> GetAllAsync()
    {
        var actors = unitOfWork.Actors.SelectAllAsQueryable(
            expression: actor => !actor.IsDeleted);

        return await Task.FromResult(mapper.Map<IEnumerable<ActorViewModel>>(actors.AsEnumerable()));
    }

    public async ValueTask<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
    {
        var existActor = await unitOfWork.Actors.SelectAsync(
            a => a.Email == email && Hasher.Verify(currentPassword, a.Password) && !a.IsDeleted);

        if (existActor is null)
            throw new ArgumentIsNotValidException("Email or Password incorrect");

        existActor.Password = Hasher.Hash(newPassword);
        await unitOfWork.Actors.UpdateAsync(existActor);
        await unitOfWork.SaveAsync();

        return true;
    }

    public ValueTask<bool> ResetPasswordAsync(string email, string newPassword)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<bool> SendCodeAsync(string email)
    {
        var actor = await unitOfWork.Actors.SelectAsync(a => a.Email == email);
        if (actor is null)
            throw new NotFoundException($"Admin with email={email} is not found");

        var random = new Random();
        var code = random.Next(10000, 99999);
        await EmailHelper.SendMessageAsync(email, "Confirmation code", code.ToString());

        return true;
    }

    public ValueTask<bool> ConfirmCodeAsync(string email, string code)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ActorViewModel> LoginAsync(string email, string password)
    {
        var actor = await unitOfWork.Actors.SelectAsync(a => a.Email == email);
        if (actor is null)
            throw new NotFoundException($"This user is not exists");

        var checkPassword = Hasher.Verify(hash: actor.Password, content: password);
        if (!checkPassword)
            throw new ArgumentIsNotValidException($"Invalid Email or Password");

        return mapper.Map<ActorViewModel>(actor);
    }
}

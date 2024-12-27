using Testify.Core.Interfaces;
using Testify.Core.Models;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Services;

public class SubmissionAnswerService : ISubmissionAnswerService
{
    private readonly IUnitOfWork _unitOfWork;

    public SubmissionAnswerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SubmissionAnswer>> GetAll()
    {
        var SubmissionAnswers = await _unitOfWork.SubmissionAnswerRepo.GetAllAsync();
        return SubmissionAnswers;
    }

    public async Task<SubmissionAnswer> GetById(int id)
    {
        return await _unitOfWork.SubmissionAnswerRepo.FindByIdAsync(id);
    }

    public async Task<SubmissionAnswer> AddSubmissionAnswer(SubmissionAnswer SubmissionAnswer)
    {
        try
        {
            await _unitOfWork.SubmissionAnswerRepo.AddAsync(SubmissionAnswer);
            await _unitOfWork.CommitAsync();
            return SubmissionAnswer;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while adding a SubmissionAnswer", ex);
        }
    }

    public async Task<SubmissionAnswer> UpdateSubmissionAnswer(SubmissionAnswer SubmissionAnswer)
    {
        try
        {
            await _unitOfWork.SubmissionAnswerRepo.UpdateAsync(SubmissionAnswer);
            await _unitOfWork.CommitAsync();
            return SubmissionAnswer;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while updating a SubmissionAnswer", ex);
        }
    }

    public async Task DeleteSubmissionAnswer(int id)
    {
        try
        {
            await _unitOfWork.SubmissionAnswerRepo.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while Deleting a SubmissionAnswer", ex);
        }
    }

}

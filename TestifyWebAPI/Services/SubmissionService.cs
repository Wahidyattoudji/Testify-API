using Testify.Core.Interfaces;
using Testify.Core.Models;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Services;

public class SubmissionService : ISubmissionService
{
    private readonly IUnitOfWork _unitOfWork;

    public SubmissionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Submission>> GetAll()
    {
        var Submissions = await _unitOfWork.SubmissionRepo.GetAllAsync();
        return Submissions;
    }

    public async Task<Submission> GetById(int id)
    {
        return await _unitOfWork.SubmissionRepo.FindByIdAsync(id);
    }

    public async Task<Submission> AddSubmission(Submission Submission)
    {
        try
        {
            await _unitOfWork.SubmissionRepo.AddAsync(Submission);
            await _unitOfWork.CommitAsync();
            return Submission;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while adding a Submission", ex);
        }
    }

    public async Task<Submission> UpdateSubmission(Submission Submission)
    {
        try
        {
            await _unitOfWork.SubmissionRepo.UpdateAsync(Submission);
            await _unitOfWork.CommitAsync();
            return Submission;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while updating a Submission", ex);
        }
    }

    public async Task DeleteSubmission(int id)
    {
        try
        {
            await _unitOfWork.SubmissionRepo.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while Deleting a Submission", ex);
        }
    }

}

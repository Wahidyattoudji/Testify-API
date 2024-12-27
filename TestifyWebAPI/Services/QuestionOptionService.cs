using Testify.Core.Interfaces;
using Testify.Core.Models;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Services;

public class QuestionOptionService : IQuestionOptionService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionOptionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<QuestionOption>> GetAll()
    {
        var QuestionOptions = await _unitOfWork.QuestionOptionRepo.GetAllAsync();
        return QuestionOptions;
    }

    public async Task<QuestionOption> GetById(int id)
    {
        return await _unitOfWork.QuestionOptionRepo.FindByIdAsync(id);
    }

    public async Task<QuestionOption> AddQuestionOption(QuestionOption QuestionOption)
    {
        try
        {
            await _unitOfWork.QuestionOptionRepo.AddAsync(QuestionOption);
            await _unitOfWork.CommitAsync();
            return QuestionOption;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while adding a QuestionOption", ex);
        }
    }

    public async Task<QuestionOption> UpdateQuestionOption(QuestionOption QuestionOption)
    {
        try
        {
            await _unitOfWork.QuestionOptionRepo.UpdateAsync(QuestionOption);
            await _unitOfWork.CommitAsync();
            return QuestionOption;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while updating a QuestionOption", ex);
        }
    }

    public async Task DeleteQuestionOption(int id)
    {
        try
        {
            await _unitOfWork.QuestionOptionRepo.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while Deleting a QuestionOption", ex);
        }
    }

}
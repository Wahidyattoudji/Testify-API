using Testify.Core.Interfaces;
using Testify.Core.Models;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Services;

public class QuestionService : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Question>> GetAll()
    {
        var Questions = await _unitOfWork.QuestionRepo.GetAllAsync();
        return Questions;
    }

    public async Task<Question> GetById(int id)
    {
        return await _unitOfWork.QuestionRepo.FindByIdAsync(id);
    }

    public async Task<Question> AddQuestion(Question Question)
    {
        try
        {
            await _unitOfWork.QuestionRepo.AddAsync(Question);
            await _unitOfWork.CommitAsync();
            return Question;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while adding a Question", ex);
        }
    }

    public async Task<Question> UpdateQuestion(Question Question)
    {
        try
        {
            await _unitOfWork.QuestionRepo.UpdateAsync(Question);
            await _unitOfWork.CommitAsync();
            return Question;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while updating a Question", ex);
        }
    }

    public async Task DeleteQuestion(int id)
    {
        try
        {
            await _unitOfWork.QuestionRepo.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while Deleting a Question", ex);
        }
    }

}

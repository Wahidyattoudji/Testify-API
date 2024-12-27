using Testify.Core.Interfaces;
using Testify.Core.Models;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Services;

public class EvaluationService : IEvaluationService
{
    private readonly IUnitOfWork _unitOfWork;

    public EvaluationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Evaluation>> GetAll()
    {
        var Evaluations = await _unitOfWork.EvaluationRepo.GetAllAsync();
        return Evaluations;
    }

    public async Task<Evaluation> GetById(int id)
    {
        return await _unitOfWork.EvaluationRepo.FindByIdAsync(id);
    }

    public async Task<Evaluation> AddEvaluation(Evaluation Evaluation)
    {
        try
        {
            await _unitOfWork.EvaluationRepo.AddAsync(Evaluation);
            await _unitOfWork.CommitAsync();
            return Evaluation;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while adding a Evaluation", ex);
        }
    }

    public async Task<Evaluation> UpdateEvaluation(Evaluation Evaluation)
    {
        try
        {
            await _unitOfWork.EvaluationRepo.UpdateAsync(Evaluation);
            await _unitOfWork.CommitAsync();
            return Evaluation;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while updating a Evaluation", ex);
        }
    }

    public async Task DeleteEvaluation(int id)
    {
        try
        {
            await _unitOfWork.EvaluationRepo.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while Deleting a Evaluation", ex);
        }
    }

}

using Testify.Core.Interfaces;
using Testify.Core.Models;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Services;

public class TestService : ITestService
{
    private readonly IUnitOfWork _unitOfWork;

    public TestService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Test>> GetAll()
    {
        var Tests = await _unitOfWork.TestRepo.GetAllAsync();
        return Tests.OrderBy(t => t.TestName);
    }

    public async Task<Test> GetById(int id)
    {
        return await _unitOfWork.TestRepo.FindByIdAsync(id);
    }

    public async Task<Test> AddTest(Test Test)
    {
        try
        {
            await _unitOfWork.TestRepo.AddAsync(Test);
            await _unitOfWork.CommitAsync();
            return Test;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while adding a Test", ex);
        }
    }

    public async Task<Test> UpdateTest(Test Test)
    {
        try
        {
            await _unitOfWork.TestRepo.UpdateAsync(Test);
            await _unitOfWork.CommitAsync();
            return Test;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while updating a Test", ex);
        }
    }

    public async Task DeleteTest(int id)
    {
        try
        {
            await _unitOfWork.TestRepo.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while Deleting a Test", ex);
        }
    }

}

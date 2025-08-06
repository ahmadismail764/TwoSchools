using TwoSchools.Domain.Entities;
using TwoSchools.Domain.Repositories;
namespace TwoSchools.App.Services;

// Fixed constructor and inheritance issues
public class TermService
{
    private readonly ITermRepository _termRepo;

    public TermService(ITermRepository termRepo)
    {
        _termRepo = termRepo;
    }

    public async Task<Term> GetCurrentTermAsync()
    {
        return await _termRepo.GetCurrentTermAsync();
    }
}
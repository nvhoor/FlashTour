using AspNetCoreSpa.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class EvaluationRepository:Repository<Evaluation>, IEvaluationRepository
    {
        public EvaluationRepository(DbContext context) : base(context)
        { }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}
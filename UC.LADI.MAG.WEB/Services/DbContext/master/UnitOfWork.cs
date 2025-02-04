using UC.Core.Abstracts;

namespace UC.LADI.MAG.WEB.Services.DbContext.master
{
    public class UnitOfWork : AbsUnitOfWork<DbSession>
    {
        public readonly string dbtype;

        public UnitOfWork(DbSession session) : base(session)
        {
            dbtype = session.dbtype;
        }
    }
}

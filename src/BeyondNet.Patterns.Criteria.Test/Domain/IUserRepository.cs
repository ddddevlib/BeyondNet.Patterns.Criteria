namespace BeyondNet.Patterns.Criteria.Test.Domain
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Search(Models.Criteria criteria);
    }
}

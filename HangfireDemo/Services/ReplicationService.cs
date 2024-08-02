using Hangfire;

namespace HangfireDemo.Services
{
    public interface IReplicationService
    {
        Task ReplicateAll();
        Task ReplicateOne();
        Task ReplicateTwo();
        Task ReplicateThree();
    }

    public class ReplicationService : IReplicationService
    {
        // private readonly IUserRepository _userRepository
        public ReplicationService()
        {

        }

        public Task ReplicateAll()
        {
            var repOneId = BackgroundJob.Enqueue(() => ReplicateOne());
            var repTwoId = BackgroundJob.ContinueJobWith(repOneId, () => ReplicateTwo());
            BackgroundJob.ContinueJobWith(repTwoId, () => ReplicateThree());

            return Task.WhenAll();
        }

        public Task ReplicateOne()
        {
            Console.WriteLine("Replicating one...");
            Thread.Sleep(3000);

            return Task.CompletedTask;
        }

        public Task ReplicateThree()
        {
            Console.WriteLine("Replicating three...");
            Thread.Sleep(1000);

            return Task.CompletedTask;
        }

        public Task ReplicateTwo()
        {
            Console.WriteLine("Replicating two...");
            Thread.Sleep(1000);

            return Task.CompletedTask;
        }
    }
}

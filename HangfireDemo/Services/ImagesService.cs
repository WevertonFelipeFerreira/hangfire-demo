using Hangfire;

namespace HangfireDemo.Services
{
    public interface IImagesService
    {
        Task ProcessImages();
        Task UploadImages();
        Task Resize();
        Task ApplyFilters();
        Task UploadToStorage();
    }

    public class ImagesService : IImagesService
    {
        private readonly IBackgroundJobClient _jobclient;
        public ImagesService(IBackgroundJobClient jobclient)
        {
            _jobclient = jobclient;
        }

        public Task ProcessImages()
        {
            var upId = _jobclient.Enqueue(() => UploadImages());
            var reszId = _jobclient.ContinueJobWith(upId, () => Resize());
            var appFlts = _jobclient.ContinueJobWith(reszId, () => ApplyFilters());
            _jobclient.ContinueJobWith(appFlts, () => UploadToStorage());

            return Task.WhenAll();
        }

        public Task UploadImages()
        {
            Console.WriteLine("Uploading images to local storage...");
            Thread.Sleep(4000);

            return Task.CompletedTask;
        }

        public Task Resize()
        {
            Console.WriteLine("Resizing images...");
            Thread.Sleep(4000);

            return Task.CompletedTask;
        }

        public Task ApplyFilters()
        {
            Console.WriteLine("Applying filters...");
            Thread.Sleep(4000);

            return Task.CompletedTask;
        }

        public Task UploadToStorage()
        {
            Console.WriteLine("Uploading images to new storage...");
            Thread.Sleep(4000);
            Console.WriteLine("Finalized! \n");

            return Task.CompletedTask;
        }
    }
}

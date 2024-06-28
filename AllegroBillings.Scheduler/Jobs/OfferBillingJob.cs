using AllegroBillingEntries.BussinesLogic.Commands;
using Microsoft.Extensions.Logging;
using Quartz;

namespace AllegroBillings.Scheduler.Jobs
{
    public class OfferBillingJob : IJob
    {
        private readonly OfferBillingServiceCommand _offerBillingServiceCommand;
        private readonly ILogger<OfferBillingJob> _logger;

        public OfferBillingJob(OfferBillingServiceCommand offerBillingServiceCommand, ILogger<OfferBillingJob> logger)
        {
            _offerBillingServiceCommand = offerBillingServiceCommand;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("OfferBillingJob started at: {time}", DateTime.UtcNow);

                await _offerBillingServiceCommand.ExecuteAsync();

                _logger.LogInformation("OfferBillingJob completed successfully at: {time}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during OfferBillingJob execution");
                throw;
            }
        }
    }
}

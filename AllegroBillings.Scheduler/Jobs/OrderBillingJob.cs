using AllegroBillingEntries.BussinesLogic.Commands;
using Microsoft.Extensions.Logging;
using Quartz;

namespace AllegroBillings.Scheduler.Jobs
{
    public class OrderBillingJob : IJob
    {
        private readonly OrderBillingServiceCommand _orderBillingServiceCommand;
        private readonly ILogger<OrderBillingJob> _logger;

        public OrderBillingJob(OrderBillingServiceCommand orderBillingServiceCommand, ILogger<OrderBillingJob> logger)
        {
            _orderBillingServiceCommand = orderBillingServiceCommand;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("OrderBillingJob started at: {time}", DateTime.UtcNow);

                await _orderBillingServiceCommand.ExecuteAsync();

                _logger.LogInformation("OrderBillingJob completed successfully at: {time}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during OrderBillingJob execution");
                throw;
            }
        }
    }
}

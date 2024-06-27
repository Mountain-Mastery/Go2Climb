using Shared;

namespace Customer.API.Domain.Services.Communication;

public class CustomerResponse : BaseResponse<model.Customer>
{
    public CustomerResponse(model.Customer customer) : base(customer) {}

    public CustomerResponse(string message) : base(message) {}
}
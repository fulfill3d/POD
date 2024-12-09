using AutoMapper;
using POD.Integrations.BrainTreeClient.Model.Transaction;

namespace POD.Integrations.BrainTreeClient.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Braintree.Transaction, TransactionResult>();
        }
    }
}
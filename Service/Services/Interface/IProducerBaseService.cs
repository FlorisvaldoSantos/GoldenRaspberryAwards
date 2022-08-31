using Domain;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface IProducerBaseService<TEntity> where TEntity : Base
    {
        Task<TOutputModel> CreateProducer<TInputModel, TOutputModel, TValidator>(TInputModel inputModel) where TValidator : AbstractValidator<TEntity> where TInputModel : class where TOutputModel : class;
        Task<IEnumerable<TOutputModel>> GetAllProducers<TOutputModel>() where TOutputModel : class;
        Task<TOutputModel> GetProducersById<TOutputModel>(int Id) where TOutputModel : class;
        Task<TOutputModel> UpdateProducer<TInputModel, TOutputModel, TValidator>(TInputModel inputModel) where TValidator : AbstractValidator<TEntity> where TInputModel : class where TOutputModel : class;
        Task<ProducerDTO> DeleteProducer(int Id);
        Task<bool> DeleteProducer(string name);
        Task<bool> HasProducer();
        Task<bool> HasProducer(ProducerDTO producerDTO);
        Task<AwardProducer> GetAwardProducer();
    }
}

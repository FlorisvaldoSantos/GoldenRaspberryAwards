using AutoMapper;
using Domain;
using FluentValidation;
using Infrasctruture.Repository;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProducerBaseService<TEntity> : IProducerBaseService<TEntity> where TEntity : Base
    {
        private readonly IProducerRepository producerRepository;
        private readonly IMapper mapper;
        public ProducerBaseService(IProducerRepository _producerRepository, IMapper _mapper)
        {
            this.producerRepository = _producerRepository;
            this.mapper = _mapper;
        }

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Por gentileza verifique o arquivo de importação");

            validator.ValidateAndThrow(obj);
        }


        public async Task<TOutputModel> CreateProducer<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)  where TValidator : AbstractValidator<TEntity>  where TInputModel : class  where TOutputModel : class
        {
            ProducerDTO producerDTO = new ProducerDTO();

            TEntity entity = mapper.Map<TEntity>(inputModel);

            Validate(entity, Activator.CreateInstance<TValidator>());

            producerDTO = mapper.Map<ProducerDTO>(entity);
            var result = await producerRepository.CreateProducer(producerDTO);

            TOutputModel outputModel = mapper.Map<TOutputModel>(result);

            return outputModel;
        }

        public async Task<IEnumerable<TOutputModel>> GetAllProducers<TOutputModel>() where TOutputModel : class
        {
            var entities = await producerRepository.ListAllProducer();

            var outputModels = entities.Select(s => mapper.Map<TOutputModel>(s));

            return outputModels;
        }

        public async Task<TOutputModel> GetProducersById<TOutputModel>(int Id) where TOutputModel : class
        {
            var entities = await producerRepository.ListProducer(Id);

            var outputModels = mapper.Map<TOutputModel>(entities);

            return outputModels;
        }

        public async Task<TOutputModel> UpdateProducer<TInputModel, TOutputModel, TValidator>(TInputModel inputModel) where TValidator : AbstractValidator<TEntity> where TInputModel : class where TOutputModel : class
        {
            ProducerDTO producerDTO = new ProducerDTO();

            TEntity entity = mapper.Map<TEntity>(inputModel);

            Validate(entity, Activator.CreateInstance<TValidator>());

            producerDTO = mapper.Map<ProducerDTO>(entity);
            var result = await producerRepository.UpdateProducer(producerDTO);

            TOutputModel outputModel = mapper.Map<TOutputModel>(result);

            return outputModel;
        }

        public async Task<ProducerDTO> DeleteProducer(int Id) => await producerRepository.DeleteProducer(Id);
        public async Task<bool> DeleteProducer(string name) => await producerRepository.DeleteProducer(name);
        public async Task<bool> HasProducer(ProducerDTO producerDTO) => await producerRepository.HasProducer(producerDTO);
        public async Task<bool> HasProducer() => await producerRepository.HasProducer();
        public async Task<AwardProducer> GetAwardProducer() => await producerRepository.GetAwardProducer();

    }
}

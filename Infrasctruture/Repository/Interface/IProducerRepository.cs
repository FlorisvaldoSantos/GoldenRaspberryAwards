using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasctruture.Repository
{
    public interface IProducerRepository
    {
        Task<ProducerDTO> CreateProducer(ProducerDTO producerDTO);

        Task<IEnumerable<ProducerDTO>> ListAllProducer();

        Task<ProducerDTO> ListProducer(int Id);

        Task<ProducerDTO> DeleteProducer(int Id);

        Task<bool> DeleteProducer(string name);

        Task<bool> HasProducer(ProducerDTO producerDTO);

        Task<bool> HasProducer();

        Task<AwardProducer> GetAwardProducer();

        Task<ProducerDTO> UpdateProducer(ProducerDTO producerDTO);
    }
}

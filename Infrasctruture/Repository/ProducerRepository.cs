using AutoMapper;
using Domain;
using Domain.Exceptions;
using Infrasctruture.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasctruture.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly DataBaseContext dataBaseContext;
        private readonly IMapper mapper;

        public ProducerRepository(DataBaseContext _dataBaseContext, IMapper _mapper)
        {
            this.dataBaseContext = _dataBaseContext;
            this.mapper = _mapper;
        }

        public async Task<ProducerDTO> CreateProducer(ProducerDTO producerDTO)
        {
            try
            {

                var result1 = await dataBaseContext.Producer.AddAsync(producerDTO);
                
                await dataBaseContext.SaveChangesAsync();

                var result = await dataBaseContext.Producer
                    .Where(e => e.Producer.ToUpper() == producerDTO.Producer.ToUpper())
                    .OrderByDescending(e => e.Id)
                    .Take(1)
                    .FirstOrDefaultAsync();

                return result;
            }
            catch (SQLiteException e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<ProducerDTO>> ListAllProducer()
        {
            try
            {
                var lstAllProducer = await dataBaseContext.Producer
                    .OrderBy(e => e.Id)
                    .ToListAsync();

                return lstAllProducer;

            }
            catch (SQLiteException e)
            {
                throw e;
            }
        }

        public async Task<ProducerDTO> ListProducer(int Id)
        {
            try
            {
                return await dataBaseContext.Producer
                    .Where(e => e.Id == Id)
                    .FirstOrDefaultAsync();

            }
            catch (SQLiteException e)
            {
                throw e;
            }
        }

        public async Task<ProducerDTO> DeleteProducer(int Id)
        {
            try
            {
                var element = await dataBaseContext.Producer
                        .Where(e => e.Id == Id)
                        .FirstOrDefaultAsync();

                if (element != null)
                {
                    dataBaseContext.Producer.Remove(element);
                    await dataBaseContext.SaveChangesAsync();
                }

                return element;
            }
            catch (SQLiteException e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteProducer(string name)
        {
            try
            {
                var lstProducer = await dataBaseContext.Producer
                    .Where(e => name.ToUpper().Contains(e.Producer.ToUpper()))
                    .ToListAsync();

                if (lstProducer == null)
                    return false;

                dataBaseContext.Producer.RemoveRange(lstProducer);
                await dataBaseContext.SaveChangesAsync();
                return true;

            }
            catch (SQLiteException e)
            {
                throw e;
            }
        }

        public async Task<bool> HasProducer(ProducerDTO producerDTO)
        {
            try
            {
                if (producerDTO == null)
                    return false;

                return await dataBaseContext.Producer
                    .Where(e => e.Producer.ToUpper() == producerDTO.Producer.ToUpper())
                    .AnyAsync();
            }
            catch (SQLiteException e)
            {

                throw e;
            }

        }

        public async Task<bool> HasProducer()
        {
            try
            {
                return await dataBaseContext.Producer.AnyAsync();
            }
            catch (SQLiteException e)
            {

                throw e;
            }

        }

        public async Task<AwardProducer> GetAwardProducer()
        {
            try
            {
                AwardProducer awardProducer = new AwardProducer();

                var producerMin = await dataBaseContext.Producer
                    .OrderBy(e => e.Interval)
                    .Take(2)
                    .ToListAsync();

                var producerMax = await dataBaseContext.Producer
                    .OrderByDescending(e => e.Interval)
                    .Take(2)
                    .ToListAsync();

                awardProducer.Min.AddRange(producerMin);
                awardProducer.Max.AddRange(producerMax);

                return awardProducer;
            }
            catch (SQLiteException e)
            {
                throw e;
            }
        }

        public async Task<ProducerDTO> UpdateProducer(ProducerDTO producerDTO)
        {
            try
            {
             
                var element = await dataBaseContext.Producer
                    .Where(e => e.Id == producerDTO.Id)
                    .FirstOrDefaultAsync();

                if (element == null)
                    return producerDTO;

                var elementUpdate = mapper.Map<ProducerDTO, ProducerDTO>(producerDTO);

                dataBaseContext.Entry(element).CurrentValues.SetValues(elementUpdate);
               
                await dataBaseContext.SaveChangesAsync();

                var newElement = await dataBaseContext.Producer
                    .Where(e => e.Id == producerDTO.Id)
                    .FirstOrDefaultAsync();

                return newElement;
            }
            catch (SQLiteException e)
            {
                //dataBaseContext.Database.RollbackTransaction();
                throw e;
            }


        }
    }
}

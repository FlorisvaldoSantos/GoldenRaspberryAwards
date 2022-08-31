using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Infrasctruture.Context;
using Infrasctruture.Repository;
using Infrasctruture.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrasctruture.Unit.Tests
{
    [TestClass]
    public class ProducerRepositoryTest
    {
        private IConfiguration configuration;
        private ILoadInformation iLoadInformation;
        private DataBaseContext dataBaseContext;
        private readonly IProducerRepository producerRepository;
        private readonly IMapper mapper;
        public ProducerRepositoryTest()
        {
            var path = Directory.GetParent(AppContext.BaseDirectory).FullName.Replace(@"\Infrasctruture.Unit.Tests\bin\Debug\net5.0", @"\GoldenRaspberryAwards");
            this.configuration = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json")
                    .Build();

            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase("InMemory")
                .Options;

            this.dataBaseContext = new DataBaseContext(options);

            this.dataBaseContext.Database.EnsureCreated();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DTOMapperProfile());
            });

            this.mapper = config.CreateMapper();

            this.producerRepository = new ProducerRepository(dataBaseContext, mapper);
        }
                

        [TestMethod]
        public async Task CreateProducer()
        {
            // Inicializo banco a cada interação 
            this.iLoadInformation = new LoadInformation(dataBaseContext, configuration);
            this.iLoadInformation.Initialize();

            var newProducer = new ProducerDTO(producer: "ProducerTest", interval: 10, previousWin: 2010, followingWin: 2020);

            var result = await producerRepository.CreateProducer(newProducer);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Id == 5);
        }

        [TestMethod]
        public async Task ListAllProducer()
        {
            // Inicializo banco a cada interação 
            this.iLoadInformation = new LoadInformation(dataBaseContext, configuration);
            this.iLoadInformation.Initialize();

            var result = await producerRepository.ListAllProducer();

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.ToList().Count == 4);
        }

        [TestMethod]
        public async Task ListProducerById()
        {
            // Inicializo banco a cada interação 
            this.iLoadInformation = new LoadInformation(dataBaseContext, configuration);
            this.iLoadInformation.Initialize();

            var result = await producerRepository.ListProducer(1);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Id == 1);
        }

        [TestMethod]
        public async Task DeleteProducerById()
        {
            // Inicializo banco a cada interação 
            this.iLoadInformation = new LoadInformation(dataBaseContext, configuration);
            this.iLoadInformation.Initialize();

            var result = await producerRepository.DeleteProducer(1);

            Assert.IsTrue(result != null);
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public async Task DeleteProducerByName()
        {
            // Inicializo banco a cada interação 
            this.iLoadInformation = new LoadInformation(dataBaseContext, configuration);
            this.iLoadInformation.Initialize();

            
            var result = await producerRepository.DeleteProducer("Producer 1");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task HasProducerObj()
        {
            // Inicializo banco a cada interação 
            this.iLoadInformation = new LoadInformation(dataBaseContext, configuration);
            this.iLoadInformation.Initialize();

            var newProducer = new ProducerDTO(producer: "Producer 1", interval: 10, previousWin: 2010, followingWin: 2020);

            var result = await producerRepository.HasProducer(newProducer);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task HasProducer()
        {
            // Inicializo banco a cada interação 
            this.iLoadInformation = new LoadInformation(dataBaseContext, configuration);
            this.iLoadInformation.Initialize();

            var result = await producerRepository.HasProducer();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetAwardProducer()
        {
            // Inicializo banco a cada interação 
            this.iLoadInformation = new LoadInformation(dataBaseContext, configuration);
            this.iLoadInformation.Initialize();

            var result = await producerRepository.GetAwardProducer();

            Assert.IsTrue(result.Min.Count == 2);
            Assert.IsTrue(result.Max.Count == 2);
        }

        [TestMethod]
        public async Task UpdateProducer()
        {
            // Inicializo banco a cada interação 
            this.iLoadInformation = new LoadInformation(dataBaseContext, configuration);
            this.iLoadInformation.Initialize();

            var UpdateProducer = new ProducerDTO(producer: "Producer Teste 1", interval: 10, previousWin: 2010, followingWin: 2020);

            UpdateProducer.Id = 1;

            var result = await producerRepository.UpdateProducer(UpdateProducer);

            Assert.IsTrue(result != null);
            Assert.AreEqual(result.Producer, UpdateProducer.Producer);
        }
    }
}

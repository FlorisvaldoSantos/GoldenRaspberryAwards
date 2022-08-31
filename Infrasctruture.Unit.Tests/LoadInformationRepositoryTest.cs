using Domain;
using Domain.Enums;
using Infrasctruture.Context;
using Infrasctruture.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infrasctruture.Unit.Tests
{
    [TestClass]
    public class LoadInformationRepositoryTest
    {
        private IConfiguration configuration;
        private ILoadInformation iLoadInformation;
        private DataBaseContext dataBaseContext;
         
        public LoadInformationRepositoryTest()
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
        }
                

        [TestMethod]
        [Description("Verifica os dados carregados na memoria")]
        public void VerifyLoadDatabaseFileToDatabase()
        {
            bool quit = true;
            this.iLoadInformation = new LoadInformation(dataBaseContext, configuration);

            this.iLoadInformation.Initialize();

            if (dataBaseContext.Database.EnsureCreated())
                Assert.Fail("Banco de dados não inicializado");

            var lstProducer = dataBaseContext.Producer.ToList();
            var lstProducerFile = LoadCSVfileToList();

            foreach (var item in lstProducer)
            {
                if(!lstProducerFile.Where(e=>e.Producer.ToUpper() == item.Producer.ToUpper()).Any())
                    quit = false;
            }

            Assert.IsTrue(quit);

        }

        private List<ProducerDTO> LoadCSVfileToList()
        {

            try
            {
                string path = configuration.GetSection("PathCSVFile").Value;
                StreamReader reader = null;
                List<ProducerDTO> lstProducers = new List<ProducerDTO>();

                if (File.Exists(path))
                {
                    reader = new StreamReader(File.OpenRead(path));

                    while (!reader.EndOfStream)
                    {

                        var lineValue = reader.ReadLine();

                        if (lineValue.Equals("producer;interval;previousWin;followingWin"))
                            lineValue = reader.ReadLine();

                        var values = lineValue.Split(';');

                        lstProducers.Add(new ProducerDTO(
                                producer: values[(int)ColumnsCSVFile.producer].ToString(),
                                interval: int.Parse(values[(int)ColumnsCSVFile.interval]),
                                previousWin: int.Parse(values[(int)ColumnsCSVFile.previousWin]),
                                followingWin: int.Parse(values[(int)ColumnsCSVFile.followingWin])
                            ));
                    }

                    return lstProducers;
                }
            }
            catch (Exception e)
            {

                throw e;
            }

            return new List<ProducerDTO>();
        }
    }
}

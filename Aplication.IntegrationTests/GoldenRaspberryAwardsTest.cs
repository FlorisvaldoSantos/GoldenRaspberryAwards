using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Aplication.IntegrationTests
{
    [TestClass]
    public class GoldenRaspberryAwardsTest
    {
        [TestMethod]
        public async Task TestGetAllProducer()
        {
            var url = "api/GoldenRaspberryAwards/GetAllProducer";
            var application = new CatalogApi();

            var client = application.CreateClient();

            var result = await client.GetAsync(url);

            var allProducers = await client.GetFromJsonAsync<List<ProducerDTO>>(url);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(allProducers);
            Assert.IsTrue(allProducers.Count == 4);
        }

        [TestMethod]
        public async Task TestCreateProducer()
        {
            var url = "api/GoldenRaspberryAwards";
            var application = new CatalogApi();

            var client = application.CreateClient();

            var producer = new ProducerDTO(producer: "ProducerTest", interval: 10, previousWin: 2010, followingWin: 2020);

            string json = JsonConvert.SerializeObject(producer);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var result = await client.PostAsync(url, httpContent);
         
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [TestMethod]
        public async Task TestGetProducerById()
        {
            var url = "api/GoldenRaspberryAwards/GetProducerById/1";
            var application = new CatalogApi();

            var client = application.CreateClient();

            var resultStatus = await client.GetAsync(url);
                      
            var result = await client.GetFromJsonAsync<ProducerDTO>(url);
            
            Assert.AreEqual(HttpStatusCode.OK, resultStatus.StatusCode);
            Assert.IsTrue(result.Id == 1);
        }


        [TestMethod]
        public async Task TestUpdateProducer()
        {
            var url = "api/GoldenRaspberryAwards/UpdateProducer";
            var application = new CatalogApi();

            var client = application.CreateClient();

            var producer = new ProducerDTO(producer: "ProducerTest", interval: 10, previousWin: 2010, followingWin: 2020);
            producer.Id = 1;

            string json = JsonConvert.SerializeObject(producer);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var result = await client.PutAsync(url, httpContent);

            var urlGetProducerById = "api/GoldenRaspberryAwards/GetProducerById/1";

            var producerValidation = await client.GetFromJsonAsync<ProducerDTO>(urlGetProducerById);


            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(producerValidation.Producer == producer.Producer); 
        }


        [TestMethod]
        public async Task TestGetAwardProducer()
        {
            var url = "api/GoldenRaspberryAwards/GetAwardProducer";
            var application = new CatalogApi();

            var client = application.CreateClient();

            var resultStatus = await client.GetAsync(url);

            var awardProducer = await client.GetFromJsonAsync<AwardProducer>(url);

            Assert.AreEqual(HttpStatusCode.OK, resultStatus.StatusCode);
            Assert.IsTrue(awardProducer.Min.Count == 2);
            Assert.IsTrue(awardProducer.Max.Count == 2);
        }

        [TestMethod]
        public async Task TestDeleteProducerById()
        {
            var url = "api/GoldenRaspberryAwards/DeleteProducerById/1";
            var application = new CatalogApi();

            var client = application.CreateClient();

            var result = await client.DeleteAsync(url);

            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }


        [TestMethod]
        public async Task TestDeleteProducerByName()
        {
            var url = "api/GoldenRaspberryAwards/DeleteProducerByName/Producer 1";
            var application = new CatalogApi();

            var client = application.CreateClient();

            var result = await client.DeleteAsync(url);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}

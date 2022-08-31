using Domain;
using Domain.Enums;
using Infrasctruture.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasctruture.Repository.Interface
{
    public class LoadInformation : ILoadInformation
    {
        private readonly DataBaseContext dataBaseContext;
        private readonly IConfiguration configuration; 
        public LoadInformation(DataBaseContext _dataBaseContext, IConfiguration _configuration)
        {
            dataBaseContext = _dataBaseContext;
            configuration = _configuration;
        }

        public void Initialize()
        {
            ClearProducer();

            LoadCSVfile();
        }

        private void ClearProducer()
        {
            try
            {
                var lstProducer = dataBaseContext.Producer.Select(e => e).ToList();

                foreach (var item in lstProducer)
                {
                    dataBaseContext.Producer.Remove(item);
                }

                dataBaseContext.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private void LoadCSVfile()
        {

            try
            {
                string path = configuration.GetSection("PathCSVFile").Value;
                StreamReader reader = null;
                

                if (File.Exists(path))
                {
                    reader = new StreamReader(File.OpenRead(path));


                    while (!reader.EndOfStream)
                    {

                        var lineValue = reader.ReadLine();

                        if (lineValue.Equals("producer;interval;previousWin;followingWin"))
                            lineValue = reader.ReadLine();

                        var values = lineValue.Split(';');

                        dataBaseContext.Producer.Add(new ProducerDTO(
                                producer: values[(int)ColumnsCSVFile.producer].ToString(),
                                interval: int.Parse(values[(int)ColumnsCSVFile.interval]),
                                previousWin: int.Parse(values[(int)ColumnsCSVFile.previousWin]),
                                followingWin: int.Parse(values[(int)ColumnsCSVFile.followingWin])
                            ));

                        dataBaseContext.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}

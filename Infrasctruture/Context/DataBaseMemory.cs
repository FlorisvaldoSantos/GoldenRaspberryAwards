
using Domain;
using Domain.Enums;
using Infrasctruture.Mapping;
using Infrasctruture.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Infrasctruture.Context
{
    public class DataBaseMemory : DbContext
    {
    
        private readonly IConfiguration configuration;
        private DataBaseContext dataContext;
        public DataBaseMemory(IConfiguration _configuration)
        {
            this.configuration = _configuration;

            //var connection = new SqliteConnection("DataSource=:memory:");
            //connection.Open();

            //var options = new DbContextOptionsBuilder<DataBaseContext>()
            //        .UseSqlite(connection)
            //        .EnableSensitiveDataLogging()
            //        .Options;

            //dataContext = new DataBaseContext(options);

            //if(dataContext.Database.EnsureCreated())
            //    LoadCSVfile();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProducerDTO>(new ProducerMap().Configure);
        }

        public DbSet<ProducerDTO> Producer { get; set; }

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

                        dataContext.Producer.Add(new ProducerDTO(
                                producer: values[(int)ColumnsCSVFile.producer].ToString(),
                                interval: int.Parse(values[(int)ColumnsCSVFile.interval]),
                                previousWin: int.Parse(values[(int)ColumnsCSVFile.previousWin]),
                                followingWin: int.Parse(values[(int)ColumnsCSVFile.followingWin])
                            ));

                        dataContext.SaveChanges();
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

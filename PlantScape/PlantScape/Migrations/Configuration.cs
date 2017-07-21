namespace PlantScape.Migrations
{
    using CsvHelper;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<PlantScape.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PlantScape.Models.ApplicationDbContext context)
        {
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //string CsvFileName = "C:\\Users\\Andrew Jordan\\Desktop\\Project-GreenLight\\PlantScape\\PlantScape\\Content\\usda2012ND.csv";
            //using (Stream stream = assembly.GetManifestResourceStream(CsvFileName))
            //{
            //    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            //    {
            //        CsvReader csvReader = new CsvReader(reader);
            //        csvReader.Configuration.WillThrowOnMissingField = false;
            //        var zipcode = csvReader.GetRecords<Zipcode>().ToArray();
            //        context.HardinessZone.AddOrUpdate(c => c.zone, zipcode);
            //    }
            //}
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}

namespace WebApplication_salesf.Models
{
    public class Candidature__c
    {

        public string id { get; set; }

        public string First_Name__c { get; set; }
        public string Last_Name__c { get; set; }

        public double Year_Of_Experience__c { get; set; }
        
        public object Year__c { get; set; }



        public Candidature__c(string id,string First_Name__c,string Last_Name__c, object Year__c, double Year_Of_Experience__c)
        {
            this.id = id;
            this.First_Name__c = First_Name__c;
            this.Last_Name__c= Last_Name__c;
            this.Year_Of_Experience__c= Year_Of_Experience__c;
            this.Year__c = Year__c;
            

        }

        public Candidature__c() { }

    }
}


using System;

namespace TesteLocalize.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; } 

        public string Name { get; private set; }             
        public string FantasyName { get; private set; }      
        public string CNPJ { get; private set; }               
        public string Situation { get; private set; }          
        public DateTime OpeningDate { get; private set; }     
        public string Type { get; private set; }                
        public string LegalNature { get; private set; }      
        public string MainActivity { get; private set; }       

        public string Street { get; private set; }             
        public string Number { get; private set; }           
        public string Complement { get; private set; }        
        public string Neighborhood { get; private set; }       
        public string City { get; private set; }              
        public string State { get; private set; }            
        public string ZipCode { get; private set; }             

        public Company(Guid userId, string name, string fantasyName, string cnpj, string situation,
            DateTime openingDate, string type, string legalNature, string mainActivity, string street,
            string number, string complement, string neighborhood, string city, string state, string zipCode, Guid id)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                throw new ArgumentException("CNPJ is required", nameof(cnpj));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required", nameof(name));
            if (string.IsNullOrWhiteSpace(situation))
                throw new ArgumentException("Situation is required", nameof(situation));

            Id = Guid.NewGuid();
            UserId = userId;

            Name = name;
            FantasyName = fantasyName;
            CNPJ = cnpj;
            Situation = situation;
            OpeningDate = openingDate;
            Type = type;
            LegalNature = legalNature;
            MainActivity = mainActivity;
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
            Id = id;
        }
    }
}

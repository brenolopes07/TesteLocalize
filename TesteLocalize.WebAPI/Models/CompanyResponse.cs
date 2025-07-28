namespace TesteLocalize.WebAPI.Models
{
    public class CompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FantasyName { get; set; }
        public string Cnpj { get; set; }
        public string Situation { get; set; }
        public DateTime? OpeningDate { get; set; }
        public string Type { get; set; }
        public string LegalNature { get; set; }
        public string MainActivity { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}

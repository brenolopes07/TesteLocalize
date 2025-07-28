using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TesteLocalize.Application.DTOs.External
{
    public class ReceitaWSResponseDTO
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("ultima_atualizacao")]
        public DateTime? LastUpdate { get; set; }

        [JsonPropertyName("cnpj")]
        public string CNPJ { get; set; }

        [JsonPropertyName("tipo")]
        public string Type { get; set; }

        [JsonPropertyName("porte")]
        public string Porte { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("fantasia")]
        public string FantasyName { get; set; }

        [JsonPropertyName("abertura")]
        public string Opening { get; set; } // string, parse to DateTime later

        [JsonPropertyName("atividade_principal")]
        public List<ActivityDto> MainActivities { get; set; }

        [JsonPropertyName("atividades_secundarias")]
        public List<ActivityDto> SecondaryActivities { get; set; }

        [JsonPropertyName("natureza_juridica")]
        public string LegalNature { get; set; }

        [JsonPropertyName("logradouro")]
        public string Street { get; set; }

        [JsonPropertyName("numero")]
        public string Number { get; set; }

        [JsonPropertyName("complemento")]
        public string Complement { get; set; }

        [JsonPropertyName("cep")]
        public string ZipCode { get; set; }

        [JsonPropertyName("bairro")]
        public string Neighborhood { get; set; }

        [JsonPropertyName("municipio")]
        public string City { get; set; }

        [JsonPropertyName("uf")]
        public string State { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("telefone")]
        public string Phone { get; set; }

        [JsonPropertyName("efr")]
        public string EFR { get; set; }

        [JsonPropertyName("situacao")]
        public string Situation { get; set; }

        [JsonPropertyName("data_situacao")]
        public string SituationDate { get; set; }

        [JsonPropertyName("motivo_situacao")]
        public string SituationReason { get; set; }

        [JsonPropertyName("situacao_especial")]
        public string SpecialSituation { get; set; }

        [JsonPropertyName("data_situacao_especial")]
        public string SpecialSituationDate { get; set; }

        [JsonPropertyName("capital_social")]
        public string CapitalSocial { get; set; }

        [JsonPropertyName("qsa")]
        public List<QsaDto> Qsa { get; set; }

        [JsonPropertyName("simples")]
        public SimplesDto Simples { get; set; }

        [JsonPropertyName("simei")]
        public SimeiDto Simei { get; set; }

        [JsonPropertyName("billing")]
        public BillingDto Billing { get; set; }

        public class ActivityDto
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonPropertyName("text")]
            public string Text { get; set; }
        }

        public class QsaDto
        {
            [JsonPropertyName("nome")]
            public string Name { get; set; }

            [JsonPropertyName("qual")]
            public string Qual { get; set; }

            [JsonPropertyName("pais_origem")]
            public string CountryOrigin { get; set; }

            [JsonPropertyName("nome_rep_legal")]
            public string LegalRepresentativeName { get; set; }

            [JsonPropertyName("qual_rep_legal")]
            public string LegalRepresentativeQual { get; set; }
        }

        public class SimplesDto
        {
            [JsonPropertyName("optante")]
            public bool Optante { get; set; }

            [JsonPropertyName("data_opcao")]
            public string DataOpcao { get; set; }

            [JsonPropertyName("data_exclusao")]
            public string DataExclusao { get; set; }

            [JsonPropertyName("ultima_atualizacao")]
            public string LastUpdate { get; set; }
        }

        public class SimeiDto
        {
            [JsonPropertyName("optante")]
            public bool Optante { get; set; }

            [JsonPropertyName("data_opcao")]
            public string DataOpcao { get; set; }

            [JsonPropertyName("data_exclusao")]
            public string DataExclusao { get; set; }

            [JsonPropertyName("ultima_atualizacao")]
            public string LastUpdate { get; set; }
        }

        public class BillingDto
        {
            [JsonPropertyName("free")]
            public bool Free { get; set; }

            [JsonPropertyName("database")]
            public bool Database { get; set; }
        }
    }
}

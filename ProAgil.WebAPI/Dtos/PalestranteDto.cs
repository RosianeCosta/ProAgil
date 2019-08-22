using System.Collections.Generic;
using System;

namespace ProAgil.WebAPI.Dtos
{
    public class PalestranteDto
    {
         public int Id { get; set; }

        public string Nome { get; set; }

        public string Minicurriculo { get; set; }
        
        public string ImagemURL { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public List<RedeSocialDto> RedesSociais { get; set; }

        public List<EventoDto> Eventos { get; set; }
    }
}
﻿using AutoMapper;
using Biblia.App.DTO;
using Biblia.Domain.Entidades;

namespace Biblia.App
{
    public class ServiceBase
    {
        public IMapper Mapper { get; private set; }

        public ServiceBase()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Capitulo, CapituloViewModel>();
                cfg.CreateMap<Livro, LivroViewModel>();
                cfg.CreateMap<Versao, VersaoViewModel>();
                cfg.CreateMap<Versiculo, VersiculoViewModel>();
                cfg.CreateMap<Resumo, ResumoViewModel>();
            });

            Mapper = mapperConfig.CreateMapper();
        }
    }
}

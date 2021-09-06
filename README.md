# Bíblia Sagrada

	A API disponibliza seis versões em português das sagradas escrutiras

## 1. Recursos
* Caixinha de promessas
* Busca de versículos
* Busca versículos por parte do texto
* Montagem de plano de estudo (em desenvolvimento)
* Acompanhamento do estudo (em desenvolvimento)

## 2. Tecnologias
* Plataforma .Net Core 3.1
* Banco de dados relacional MySql
* Dapper
* Domain Driven Design - DDD

## 3. Disponibilização dos recursos

###	3.1. API: http://biblia.marciocosta.eti.br
* curl --location --request GET 'http://biblia.marciocosta.eti.br/v1/CaixinhaPromessas'
* curl --location --request GET 'http://biblia.marciocosta.eti.br/v1/livros'
* curl --location --request GET 'http://biblia.marciocosta.eti.br/v1/versoes'
* curl --location --request GET 'http://biblia.marciocosta.eti.br/v1/versao/4/livro/59/capitulo/3/numero/3'
* curl --location --request GET 'http://biblia.marciocosta.eti.br/v1/Resumo/versao/5'
* curl --location --request GET 'http://biblia.marciocosta.eti.br/v1/Versao/5/Livro/47/Capitulo/2/quantidade-versiculos'
* curl --location --request GET 'http://biblia.marciocosta.eti.br/v1/versao/4/livro/59/capitulo/3/versiculos'
* curl --location --request GET 'http://biblia.marciocosta.eti.br/v1/caixinha-de-promessas/listar?livroId=20&capituloId=2&versiculoId=6'
* curl --location --request GET 'http://biblia.marciocosta.eti.br/v1/buscar/deus de israel'

###	3.2. UI: http://marciocosta.eti.br/biblia-app
# Bíblia Sagrada

	A API disponibliza seis versões em português das sagradas escrutiras 

## 1. Recursos
* Caixinha de promessas	
* Busca de versículos (em desenvolvimento)	
* Montagem de plano de estudo (em desenvolvimento)	
* Acompanhamento do estudo (em desenvolvimento)
	
## 2. Tecnologias	
* Plataforma .Net Core	
* Banco de dados relacional MySql	
* Dapper	
* Domain Driven Design - DDD	
* Angular
	
## 3. Disponibilização dos recursos	
###	3.1. API: http://mminfotech.com.br/biblia/
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/CaixinhaPromessas'	
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/livros'
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/versoes'
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/versao/4/livro/59/capitulo/3/numero/3'
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/Resumo/versao/5'
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/Versao/5/Livro/47/Capitulo/2/quantidade-versiculos'
###	3.2. UI: http://mminfotech.com.br/biblia-app/
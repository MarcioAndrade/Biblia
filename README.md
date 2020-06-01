# Bíblia Sagrada

	A API disponibliza seis versões em português das sagradas escrutiras 

## 1. Recursos
###	1.1. Caixinha de promessas	
###	1.2. Busca de versículos (em desenvolvimento)	
###	1.3. Montagem de plano de estudo (em desenvolvimento)	
###	1.4. Acompanhamento do estudo (em desenvolvimento)
	
## 2. Tecnologias	
###	2.1. Plataforma .Net Core	
###	2.2. Banco de dados relacional MySql	
###	2.3. Dapper	
###	2.4. Domain Driven Design - DDD	
###	2.5. Angular
	
## 3. Disponibilização dos recursos	
###	3.1. API: http://mminfotech.com.br/biblia/
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/CaixinhaPromessas'	
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/livros'
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/versoes'
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/versao/4/livro/59/capitulo/3/numero/3'
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/Resumo/versao/5'
* curl --location --request GET 'http://mminfotech.com.br/biblia/v1/Versao/5/Livro/47/Capitulo/2/quantidade-versiculos'
###	3.2. UI: http://mminfotech.com.br/biblia-app/
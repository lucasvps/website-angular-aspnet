Simples website desenvolvido com Angular no Front-End e Asp Net Web Api para o Back-End, para teste
de vaga de emprego na Grafis.

Funcionalidades:
Cadastro de clientes, com os campos:

Nome (obrigatório)
E-mail (obrigatório e único, pois não pode existir mais de um cadastro com o mesmo e-mail)

Cadastro de produtos, com os campos:

Descrição (obrigatório)
Valor (obrigatório)

Cadastro do pedidos, com os campos:

Número (obrigatório e sequêncial)
Data (obrigatório)
Produtos (obrigatório)
Cliente (obrigatório)
Valor (obrigatório)
Desconto
ValorTotal (obrigatório).

Rotas :

GET E POST CLIENTES = /api/clients

GET E POST PRODUTOS = /api/products

POST PARA PEDIDOS = /api/pedidos

POST PARA ADICIONAR PRODUTOS AO PEDIDO = /api/produtopedido

GET PARA OS PEDIDOS DE UM CLIENTE = /api/clients/{clienteId}/orders

GET PARA PRODUTOS EM UM PEDIDO = /api/pedidos/{pedidoId}/produtos
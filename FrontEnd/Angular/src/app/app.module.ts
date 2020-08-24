import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialImports } from './MaterialImports';

import { AppComponent } from './app.component';
import { CadastroContatoComponent } from './cadastro-contato/cadastro-contato.component';
import { ContatosComponent } from './contatos/contatos.component';
import { MensagensComponent } from './mensagens/mensagens.component';
import { CadastroMensagemComponent } from './cadastro-mensagem/cadastro-mensagem.component';
import { ClientsComponent } from './clients/clients.component';
import { CadastroClienteComponent } from './cadastro-cliente/cadastro-cliente.component';
import { ClientsService } from './clients.service';
import { PedidosComponent } from './pedidos/pedidos.component';
import { CadastroPedidoComponent } from './cadastro-pedido/cadastro-pedido.component';
import { ProdutosComponent } from './produtos/produtos.component';
import { CadastroProdutoComponent } from './cadastro-produto/cadastro-produto.component';
import { ProdutoService } from './produto.service';
import { ProdutosPedidosComponent } from './produtos-pedidos/produtos-pedidos.component';






const appRoutes: Routes = [
	{ path: 'contatos', component: ContatosComponent },
	{ path: 'contatos/cadastro', component: CadastroContatoComponent },
	{ path: 'contatos/:id/mensagens', component: MensagensComponent },
	{ path: 'cliente/:id/pedidos', component: PedidosComponent },
	{ path: 'contatos/:id/mensagens/cadastro', component: CadastroMensagemComponent },
	{ path: 'clientes', component: ClientsComponent },
	{ path: 'clientes/cadastro', component: CadastroClienteComponent },
	{ path: 'produtos', component: ProdutosComponent },
	{ path: 'produtos/cadastro', component: CadastroProdutoComponent },
	{ path: 'pedido/novo/:id', component: CadastroPedidoComponent },
	{ path: 'pedido/produtos/:id/:serial', component: ProdutosPedidosComponent },
	

	{ path: '', redirectTo: '/contatos', pathMatch: 'full' }
];

@NgModule({
	declarations: [
		AppComponent,
		CadastroContatoComponent,
		ContatosComponent,
		MensagensComponent,
		CadastroMensagemComponent,
		ClientsComponent,
		CadastroClienteComponent,
		PedidosComponent,
		CadastroPedidoComponent,
		ProdutosComponent,
		CadastroProdutoComponent,
		ProdutosPedidosComponent,
	],
	imports: [
		BrowserModule,
		RouterModule.forRoot(appRoutes),
		HttpClientModule,
		BrowserAnimationsModule,
		MaterialImports,
		FormsModule,
		ReactiveFormsModule
	],
	providers: [ClientsService, ProdutoService],
	bootstrap: [AppComponent]
})
export class AppModule { }

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialImports } from './MaterialImports';

import { AppComponent } from './app.component';
import { CadastroContatoComponent } from './cadastro-contato/cadastro-contato.component';
import { HomeComponent } from './home/home.component';
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
	{ path: 'home', component: HomeComponent},
	{ path: 'cliente/:id/pedidos', component: PedidosComponent },
	{ path: 'clientes', component: ClientsComponent },
	{ path: 'clientes/cadastro', component: CadastroClienteComponent },
	{ path: 'produtos', component: ProdutosComponent },
	{ path: 'produtos/cadastro', component: CadastroProdutoComponent },
	{path : 'clientes/:id/pedido/cadastro', component: CadastroPedidoComponent},
	{ path: 'pedido/:id/produtos', component: ProdutosPedidosComponent },
	{ path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
	declarations: [
		AppComponent,
		CadastroContatoComponent,
		MensagensComponent,
		CadastroMensagemComponent,
		ClientsComponent,
		CadastroClienteComponent,
		PedidosComponent,
		CadastroPedidoComponent,
		ProdutosComponent,
		CadastroProdutoComponent,
		ProdutosPedidosComponent,
		HomeComponent
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

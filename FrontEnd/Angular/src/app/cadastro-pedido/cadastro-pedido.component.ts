import { Component, OnInit } from '@angular/core';
import { ProdutoModel } from '../produtos/produto.model';
import { Router, ActivatedRoute } from '@angular/router';
import { ClientModel } from '../clients/client.model';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ProdutoService } from 'src/produto.service';

class NovoPedido {
  public idClient: number;
  public cost: number;
  public discount: number;
}

class ProdutoPedido {
  public ProdutoId: number;
  public PedidoId: number;
}

@Component({
  selector: 'app-cadastro-pedido',
  templateUrl: './cadastro-pedido.component.html',
  styleUrls: ['./cadastro-pedido.component.css']
})
export class CadastroPedidoComponent implements OnInit {

  displayedColumns: string[] = ['description', 'value', 'checkbox'];

  controleDesconto: FormGroup;

  produtos: Array<ProdutoModel> = new Array();

  carrinhoIds: Array<number> = new Array();

  valorTotal: number = 0;

  cliente: ClientModel = new ClientModel();

  IdClient: number;

  Desconto: number = 0;

  pedidoId: number;

  error: string;

  public noWhitespaceValidator(control: FormGroup) {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { 'whitespace': true };
  }

  constructor(private formBuilder: FormBuilder, private http: HttpClient, private produtoService: ProdutoService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.IdClient = this.activatedRoute.snapshot.params['id'];
  }


  listarProdutos() {
    this.produtoService.listarProdutos().subscribe(produtos => {
      this.produtos = produtos;
    });
  }

  getProduto(produto: ProdutoModel) {
    if (this.carrinhoIds.indexOf(produto.Id) != -1) {
      var index = this.carrinhoIds.indexOf(produto.Id);
      this.carrinhoIds.splice(index, 1);
      this.valorTotal -= (produto.Value);
    } else {
      this.carrinhoIds.push(produto.Id);
      this.valorTotal += (produto.Value);
    }
  }

  finalizarPedido() {
    console.log(this.carrinhoIds.length);
    if (this.carrinhoIds.length > 0) {
      var pedido = new NovoPedido();
      pedido.idClient = this.IdClient;
      pedido.cost = this.valorTotal;
      pedido.discount = this.Desconto;

      var headers = new HttpHeaders().append("Content-Type", "application/json")

      var params  = new HttpParams().set("client", (this.IdClient).toString()).set("discount", this.Desconto.toString());

      var produtos = JSON.stringify(this.carrinhoIds);
      console.log(produtos);

      this.http.post("http://localhost:49493/api/orders", produtos, {params, headers}).subscribe(result => {
        this.router.navigate(['cliente/' + this.IdClient + "/pedidos"]);
      });
    }

  }

  aplicarDesconto(desconto: number) {
    if (desconto > this.valorTotal) {
      this.error = "Desconto não pode ser maior que o valor total!"
      this.Desconto = 0;
    } else if (desconto < 0) {
      this.error = "Desconto não pode ser um valor negativo!"
      this.Desconto = 0;
    } else if (desconto == null) {
      this.error = "Valor inválido!"
      this.Desconto = 0;
    } else {
      this.error = "";
      console.log(desconto);
      this.Desconto = desconto;
      console.log(this.Desconto);
    }


  }


  ngOnInit() {
    this.http.get<ClientModel>('http://localhost:49493/api/clients/' + this.IdClient)
      .subscribe(x => this.cliente = x);

    this.listarProdutos()
  }

  home(){
		this.router.navigate(['home']);
	}

}

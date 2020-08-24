import { Component, OnInit } from '@angular/core';
import { ProdutoModel } from '../produtos/produto.model';
import { ProdutoService } from '../produto.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ClientModel } from '../clients/client.model';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

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

  rateControl: FormGroup;

  produtos: Array<ProdutoModel> = new Array();

  carrinho: Array<ProdutoModel> = new Array();

  carrinhoIds: Array<number> = new Array();

  valorTotal: number = 0;

  cliente: ClientModel = new ClientModel();

  IdClient: number;

  Desconto: number = 0;

  pedidoId: number;

  error: string;

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
      this.carrinho.splice(index, 1)
      this.carrinhoIds.splice(index, 1);
      this.valorTotal -= (produto.Value);
    } else {
      this.carrinhoIds.push(produto.Id);
      this.carrinho.push(produto);
      this.valorTotal += (produto.Value);
    }
    console.log(this.valorTotal);
    console.log(this.carrinho);
    console.log(this.carrinhoIds);
  }

  finalizarPedido() {
    if (this.carrinho.length > 0) {
      var pedido = new NovoPedido();
      pedido.idClient = this.IdClient;
      pedido.cost = this.valorTotal;
      pedido.discount = this.Desconto;

      this.http.post("http://localhost:49493/api/pedidos", pedido).subscribe((result: number) => {
        this.pedidoId = result;
        this.carrinho.forEach(produto => {
          var prod = new ProdutoPedido();
          prod.PedidoId = this.pedidoId;
          prod.ProdutoId = produto.Id;
          console.log(prod);
          this.http.post("http://localhost:49493/api/produtopedido", prod).subscribe((result: ProdutoPedido) => {
            this.router.navigate(['cliente/' + this.IdClient + "/pedidos"]);
          });
        });
      });
    }

  }

  aplicarDesconto(desconto: number) {
    if (desconto > this.valorTotal) {
      this.error = "Desconto não pode ser maior que o valor total!"
      console.log('Maior que valor total');
      this.Desconto = 0;
    } else {
      this.error = "";
      console.log(desconto);
      this.Desconto = desconto;
      console.log(this.Desconto);
    }


  }


  ngOnInit() {
    // this.rateControl = this.formBuilder.group({
    // 	desconto: ['', Validators.max(this.valorTotal)]
    // });
    this.http.get<ClientModel>('http://localhost:49493/api/clients/' + this.IdClient)
      .subscribe(x => this.cliente = x);

    this.listarProdutos()
  }

}

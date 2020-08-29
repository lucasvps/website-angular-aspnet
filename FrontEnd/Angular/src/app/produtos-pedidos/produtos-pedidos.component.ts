import { Component, OnInit } from '@angular/core';
import { ProdutoModel } from '../produtos/produto.model';
import { ProdutoService } from '../../produto.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-produtos-pedidos',
  templateUrl: './produtos-pedidos.component.html',
  styleUrls: ['./produtos-pedidos.component.css']
})
export class ProdutosPedidosComponent implements OnInit {

  displayedColumns: string[] = ['description', 'value'];

  produtos: Array<ProdutoModel> = new Array();

  pedidoId : number;

  serialNumber : number;

  constructor(private produtoService: ProdutoService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.pedidoId = this.activatedRoute.snapshot.params['id'];
   }

  listarProdutosPedido() {
    this.produtoService.listarProdutosPedido(this.pedidoId).subscribe(produtos => {
      this.produtos = produtos;
    });
  }


  ngOnInit() {
    this.listarProdutosPedido()
  }

  home(){
		this.router.navigate(['home']);
	}

}

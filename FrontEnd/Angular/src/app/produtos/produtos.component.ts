import { Component, OnInit } from '@angular/core';
import { ProdutoModel } from './produto.model';
import { Router } from '@angular/router';
import { ProdutoService } from '../../produto.service';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.css']
})
export class ProdutosComponent implements OnInit {

  displayedColumns: string[] = ['description', 'value', 'image'];

  produto: ProdutoModel = new ProdutoModel();

  produtos: Array<ProdutoModel> = new Array();

  constructor(private produtoService: ProdutoService, private router: Router) { }

  listarProdutos(){
    this.produtoService.listarProdutos().subscribe(produtos => {
      this.produtos = produtos;
    });
  }

  ngOnInit() {
    this.listarProdutos()
  }

  novo(){
    this.router.navigate(['produtos/cadastro']);
  }

  home(){
    console.log('oi');
		this.router.navigate(['/home']);
	}
  

}

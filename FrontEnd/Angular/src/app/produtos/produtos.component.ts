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

  colunas: string[] = ['descricao', 'valor', 'imagem'];

  produto: ProdutoModel = new ProdutoModel();

  produtos: Array<ProdutoModel> = new Array();

  constructor(private produtoService: ProdutoService, private router: Router) { }

  listarProdutos(){
    this.produtoService.listarProdutos().subscribe(produtos => {
      this.produtos = produtos;
      produtos.forEach(produto => {
        if (produto.Image == null){
          produto.Image = 'https://upload.wikimedia.org/wikipedia/commons/0/0a/No-image-available.png';
        }
      });
    });
  }

  ngOnInit() {
    this.listarProdutos()
  }

  novo(){
    this.router.navigate(['produtos/cadastro']);
  }

  home(){
		this.router.navigate(['/home']);
	}
  

}

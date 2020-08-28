import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProdutoModel } from './produtos/produto.model';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

  constructor(private http: HttpClient) { }


  listarProdutos() : Observable<any>{
    return this.http.get("http://localhost:49493/api/products/");
  }

  listarProdutosPedido(pedidoId:number) : Observable<any>{
    return this.http.get("http://localhost:49493/api/pedidos/" + pedidoId + "/produtos");
  }

  novoProduto(produto: ProdutoModel) : Observable<any> {
    //const fd = new FormData();
    //fd.append('imageFile', produto.ImageFile, produto.ImageFile.name)
    return this.http.post("http://localhost:49493/api/products/", produto);
  }
}

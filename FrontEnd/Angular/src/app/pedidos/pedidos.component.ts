import { Component, OnInit } from '@angular/core';
import { ClientModel } from '../clients/client.model';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { PedidoModel } from './pedido.model';

@Component({
  selector: 'app-pedidos',
  templateUrl: './pedidos.component.html',
  styleUrls: ['./pedidos.component.css']
})
export class PedidosComponent implements OnInit {

  pedidos: Array<PedidoModel> = new Array();

  cliente: ClientModel = new ClientModel();

  IdClient: number;

  constructor(private http: HttpClient, private router: Router, private activatedRoute: ActivatedRoute) {
    this.IdClient = this.activatedRoute.snapshot.params['id'];
  }

  ngOnInit() {
    this.http.get<ClientModel>('http://localhost:49493/api/clients/' + this.IdClient)
      .subscribe(x => this.cliente = x);

    this.http.get<PedidoModel[]>('http://localhost:49493/api/clients/' + this.IdClient + '/orders')
      .subscribe(x => this.pedidos = x);
  }

  novoPedido(){
    this.router.navigate(['clientes/' + this.IdClient + '/pedido/cadastro']);
  }

  produtoPedido(pedidoId:number, serial:number){
    this.router.navigate(['pedido/' + pedidoId + '/produtos']);
    
  }

}

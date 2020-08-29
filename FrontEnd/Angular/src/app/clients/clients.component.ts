import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { ClientsService } from '../../clients.service';
import { ClientModel } from './client.model';




@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {

  colunas: string[] = ['nome', 'email', 'pedidos'];

  cliente: ClientModel = new ClientModel();

  clientes: Array<ClientModel> = new Array();

  httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json'
		})
	};

  constructor(private clientsService: ClientsService,private router: Router) { }

  listarClientes(){
    this.clientsService.listarClientes().subscribe(clients => {
      this.clientes = clients;
    });
  }

  ngOnInit() {
    this.listarClientes();
  }

  novo() {
		this.router.navigate(['clientes/cadastro']);
  }
  
  pedidos(cliente: ClientModel) {
		this.router.navigate(['cliente/' + cliente.Id + '/pedidos']);
  }

  home(){
		this.router.navigate(['home']);
  }
  
  produtos() {
		this.router.navigate(['produtos']);
	}
  
  

}

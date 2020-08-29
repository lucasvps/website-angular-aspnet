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

  displayedColumns: string[] = ['name', 'email', 'orders'];

  client: ClientModel = new ClientModel();

  clients: Array<ClientModel> = new Array();

  httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json'
		})
	};

  constructor(private clientsService: ClientsService,private router: Router) { }

  listarClientes(){
    this.clientsService.listarClientes().subscribe(clients => {
      this.clients = clients;
    });
  }

  ngOnInit() {
    this.listarClientes();
  }

  novo() {
		this.router.navigate(['clientes/cadastro']);
  }
  
  pedidos(client: ClientModel) {
		this.router.navigate(['cliente/' + client.Id + '/pedidos']);
  }

  home(){
		this.router.navigate(['home']);
	}
  
  

}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';

class Contato {
	public Id: number;
	public Nome: string;
}

@Component({
	selector: 'app-contatos',
	templateUrl: './contatos.component.html',
	styleUrls: ['./contatos.component.css']
})
export class ContatosComponent implements OnInit {

	contatos: Contato[] = [];

	httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json'
		})
	};

	constructor(private http: HttpClient, private router: Router) { }

	ngOnInit() {
		this.http.get<Contato[]>('http://localhost:49493/api/contatos/')
			.subscribe(x => {
				this.contatos = x;
			});
	}

	clientes() {
		this.router.navigate(['clientes']);
	}

	produtos() {
		this.router.navigate(['produtos']);
	}

	mensagens(contato: Contato) {
		this.router.navigate(['contatos/' + contato.Id + '/mensagens']);
	}

}



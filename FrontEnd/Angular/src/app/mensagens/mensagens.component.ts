import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';

class Mensagem {
	public Descricao: string;
	public DataHora: Date;
}

class Contato {
	public Nome: string;
}

@Component({
	selector: 'app-mensagens',
	templateUrl: './mensagens.component.html',
	styleUrls: ['./mensagens.component.css']
})
export class MensagensComponent implements OnInit {

	mensagens: Mensagem[] = [];

	contato: Contato = new Contato();

	idContato: number;

	constructor(private http: HttpClient, private router: Router, private activatedRoute: ActivatedRoute) {
		this.idContato = this.activatedRoute.snapshot.params['id'];
	}

	ngOnInit() {

		this.http.get<Contato>('http://localhost:49493/api/contatos/' + this.idContato)
			.subscribe(x => this.contato = x);

		this.http.get<Mensagem[]>('http://localhost:49493/api/contatos/' + this.idContato + '/mensagens')
			.subscribe(x => this.mensagens = x);
	}

	novaMensagem() {
		this.router.navigate(['contatos/' + this.idContato + '/mensagens/cadastro']);
	}

	contatos() {
		this.router.navigate(['contatos']);
	}
}


